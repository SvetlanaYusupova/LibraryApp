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
    /// Логика взаимодействия для View2BookWindow.xaml
    /// </summary>
    public partial class View2BookWindow : Window
    {
        public View2BookWindow(string login, string action, string tag, string logAdmin)
        {
            chosenbook = tag;
            currentaction = action;
            userlogin = login;
            admin = logAdmin;

            _storage = new Storage();
            books = _storage.Books;
       
            // текущий пользователь
            currentuser = GetCurrentUser(userlogin);
            // выбранная пользователем книга
            currentbook = GetCurrentBook(chosenbook);

            InitializeComponent();
        }

        string currentaction;
        string chosenbook;
        string userlogin;
        string admin;

        User currentuser;
        OrderBook currentbook;

        static Storage _storage;
        List<BookInLibrary> books;

        private void GoOut(object sender, RoutedEventArgs e)
        {
            _storage.SaveBooks();
            _storage.SaveUsers();
            new GiveBookWindow(userlogin, currentaction, new List<string> { "", "", "", "" }, admin).Show();
            Close();
        }

        

        // инициализация названия книги
        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = currentbook.GetBookName();
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
        private OrderBook GetCurrentBook(string chosenbook)
        {
            foreach (var book in currentuser.GetOrderBook())
            {
                if (book.GetBookName() == chosenbook)
                {
                    return book;
                }
            }

            return default;
        }

        // функция для получения определённого пользователя
        private User GetCurrentUser(string ulogin)
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

        private void GiveBook(object sender, RoutedEventArgs e)
        {
            Button Givebook = sender as Button;

            foreach (var user in _storage.Users)
            {
                if (user == currentuser)
                {
                    user.AddTakenBook(new TakenBook(currentbook.GetBookName(), currentbook.GetAuthor(), currentbook.GetAgeRating(), currentbook.GetDescription(), currentbook.GetGenre(), currentbook.GetPublishedYear(), currentbook.GetRating(), currentbook.GetNumPages(), DateTime.Today, DateTime.Today.AddDays(30)));
                    user.DicreaseOrderBook(currentbook);
                }
            }

            _storage.SaveBooks();
            _storage.SaveUsers();

            MessageBox.Show("Книга успешно выдана!");
            new WindowAdmin(admin).Show();
            Close();


        }

        private void FromOrderToTaken(OrderBook orderbook)
        {

        }

        //для бронирования данной книги
        /*private void BookBook(object sender, RoutedEventArgs e)
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
        }*/

    }
}
