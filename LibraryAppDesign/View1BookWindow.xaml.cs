using LibraryApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryAppDesign
{
    /// <summary>
    /// Логика взаимодействия для View1BookWindow.xaml
    /// </summary>
    public partial class View1BookWindow : Window
    {
        public View1BookWindow(string login, string tag) // передаём логи пользователя и название книги
        {
            chosenbook = tag;
            userlogin = login;

            currentuser = GetCurrentUser(userlogin); // выбранная пользователем  книга
            currentbook = GetCurrentBook(chosenbook); // пользователь


            InitializeComponent();
        }

        string chosenbook;
        string userlogin;

        User currentuser;
        BookInLibrary currentbook;

        static Storage _storage = new Storage();
        //List<User> users = _storage.Users;
        //List<BookInLibrary> books = _storage.Books;
        //List<Admin> admins = _storage.Admins;
        //List<Notification> notifications = _storage.Notifications;

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };
        //List<string> filters4Book;
        //List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };

        private void Account(object sender, RoutedEventArgs e)
        {
            //для кнопки внесения изменений в данные пользователя
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для кнопки показа уведомлений пользователя
        }

        private void GoOut(object sender, RoutedEventArgs e)
        {
            _storage.SaveBooks();
            _storage.SaveUsers();
            new TakeBookWindow(userlogin, new List<string> { null, null, null, null}).Show();
            Close();
        }

        private void NameBook_Initialized(object sender, EventArgs e) // инициализация названия книги
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = currentbook.GetName();
        }

        private void AuthorName_Initialized(object sender, EventArgs e) // инициализация автор(а/ов) книги
        {
            TextBlock AuthorName = sender as TextBlock;
            AuthorName.Text = String.Join(", ", currentbook.GetAuthor());
        }

        private void GenreName_Initialized(object sender, EventArgs e) // инициализация жанра книги
        {
            TextBlock GenreName = sender as TextBlock;
            GenreName.Text = currentbook.GetGenre();
        }

        private void AgeName_Initialized(object sender, EventArgs e) // инициализация названия книги
        {
            TextBlock AgeName = sender as TextBlock;
            AgeName.Text = currentbook.GetAgeRating();
        }

        private void DescriptionName_Initialized(object sender, EventArgs e) // инициализация описания книги
        {
            TextBlock DescriptionName = sender as TextBlock;
            DescriptionName.Text = currentbook.GetDescription();
        }

        BookInLibrary GetCurrentBook(string chosenbook) // функция для получения экземпляра книги, выбранной пользователем, по названию
        {
            foreach (var book in _storage.Books)
            {
                if (book.GetName() == chosenbook)
                {
                    return book;
                }
            }

            return default;
        }

        User GetCurrentUser(string ulogin) // функция для получения определённого пользователя
        {
            foreach (var user in _storage.Users)
            {
                if (user.GetLogin() == ulogin)
                {
                    return user;
                }
            }

            return default;
        }


        private void BookBook(object sender, RoutedEventArgs e) //для бронирования данной книги
        {
            Button Book = sender as Button;

            currentuser = GetCurrentUser(userlogin);
            currentbook = GetCurrentBook(chosenbook);

            Check_Age();

            void Check_Age() // функция для проверки соотношения возраста пользователя и возрастного рейтинга книги
            {
                if (currentuser.GetAge() >= currentbook.GetIntBookAge())
                {
                    CheckUserBooks();
                }
                else
                {
                    MessageBox.Show("Контент не доступен!");
                    new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                    Close();
                }
            }

            void CheckUserBooks() // функция для проверки наличия выбранной пользователем книги в списке забронированных пользователем книг и в списке книг, которые уже "на руках"
            {
                bool check = true;
                foreach (var book in currentuser.GetTakenBooks())
                {
                    if (book.GetName() == chosenbook)
                    {
                        MessageBox.Show("Данная книга сейчас у вас на руках!");
                        new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                        Close();
                        check = false;
                    }
                }

                if (check)
                {
                    foreach (var book in currentuser.GetOrderedBooks())
                    {
                        if (book.GetName() == chosenbook)
                        {
                            MessageBox.Show("Данная книга сейчас забронирована вами!");
                            new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                            Close();
                            check = false;
                        }
                    }
                    if (check)
                    {
                        //currentuser.AddOrderBook(new OrderBook(currentbook.GetName(), currentbook.GetAuthor(), currentbook.GetAgeRating(), currentbook.GetDescription(), currentbook.GetGenre(), DateTime.Today.AddDays(7))); // добавили книгу в список забронированных книг пользователя

                        foreach (var user in _storage.Users)
                        {
                            if (user == currentuser)
                            {
                                user.AddOrderBook(new OrderBook(currentbook.GetName(), currentbook.GetAuthor(), currentbook.GetAgeRating(), currentbook.GetDescription(), currentbook.GetGenre(), DateTime.Today.AddDays(7))); // добавили книгу в список забронированных книг пользователя
                                break;
                            }
                        }

                        foreach (var book in _storage.Books)
                        {
                            if (book == currentbook)
                            {
                                book.Dicrease();
                                break;
                            }
                        }

                        _storage.SaveBooks();
                        _storage.SaveUsers();

                        MessageBox.Show("Книга забронирована! Вы можете получить её в течении 7 дней!");
                        new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                        Close();
                    }
                    
                }
            }
        }
    }
}
