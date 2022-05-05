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
        // передаём логин пользователя и название книги
        public View1BookWindow(string login, string tag) 
        {
            chosenbook = tag;
            userlogin = login;

            // выбранная пользователем  книга
            currentuser = GetCurrentUser(userlogin);
            // пользователь
            currentbook = GetCurrentBook(chosenbook); 


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

        // инициализация названия книги
        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = currentbook.GetName();
        }

        // инициализация автор(а/ов) книги
        private void AuthorName_Initialized(object sender, EventArgs e) 
        {
            TextBlock AuthorName = sender as TextBlock;
            AuthorName.Text = String.Join(", ", currentbook.GetAuthor());
        }

        // инициализация жанра книги
        private void GenreName_Initialized(object sender, EventArgs e) 
        {
            TextBlock GenreName = sender as TextBlock;
            GenreName.Text = currentbook.GetGenre();
        }

        // инициализация названия книги
        private void AgeName_Initialized(object sender, EventArgs e) 
        {
            TextBlock AgeName = sender as TextBlock;
            AgeName.Text = currentbook.GetAgeRating();
        }

        // инициализация описания книги
        private void DescriptionName_Initialized(object sender, EventArgs e) 
        {
            TextBlock DescriptionName = sender as TextBlock;
            DescriptionName.Text = currentbook.GetDescription();
        }

        // функция для получения экземпляра книги, выбранной пользователем, по названию
        BookInLibrary GetCurrentBook(string chosenbook) 
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

        // функция для получения определённого пользователя
        User GetCurrentUser(string ulogin) 
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

        //для бронирования данной книги
        private void BookBook(object sender, RoutedEventArgs e) 
        {
            Button Book = sender as Button;

            currentuser = GetCurrentUser(userlogin);
            currentbook = GetCurrentBook(chosenbook);

            Check_Age();

            // функция для проверки соотношения возраста пользователя и возрастного рейтинга книги
            void Check_Age() 
            {
                if (currentuser.GetAge() >= currentbook.GetIntBookAge())
                {
                    CheckBookAvailable();
                }
                else
                {
                    MessageBox.Show("Контент не доступен!");
                    new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                    Close();
                }
            }
            
            void CheckBookAvailable()
            {
                if (currentbook.GetAvailableNumber() != 0)
                {
                    CheckUserBooks();
                }
                else
                {
                    MessageBox.Show("В настоящий момент в библиотеке нет доступных экземпляров данной книги!");
                    new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
                    Close();
                }
            }

            // функция для проверки наличия выбранной пользователем книги в списке забронированных пользователем книг и в списке книг, которые уже "на руках"
            void CheckUserBooks() 
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
