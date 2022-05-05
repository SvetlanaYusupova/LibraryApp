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
        public View1BookWindow(string login, string tag)
        {
            chosenbook = tag;
            userlogin = login;
            InitializeComponent();
        }
        string chosenbook;
        string userlogin;

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<BookInLibrary> books = _storage.Books;
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
            new TakeBookWindow(userlogin, new List<string> { null, null, null, null}).Show();
            Close();
        }

        private void BookBook(object sender, RoutedEventArgs e) //для бронирования данной книги
        {
            Button Book = sender as Button;

            User currentuser = GetCurrentUser(userlogin);
            BookInLibrary currentbook = GetCurrentBook(chosenbook);

            Check_Age();

            void Check_Age()
            {
                if (currentuser.GetAge() >= currentbook.GetIntBookAge())
                {
                    CheckUserBooks();
                }
                else
                {
                    MessageBox.Show("Контент не доступен!");
                    new TakeBookWindow(userlogin, new List<string> { "", "", "", "" });
                }
            }

            BookInLibrary GetCurrentBook(string chosenbook)
            {
                foreach (var book in books)
                {
                    if (book.GetName() == chosenbook)
                    {
                        return book;
                    }
                }

                return default;
            }

            User GetCurrentUser(string ulogin)
            {
                foreach (var user in users)
                {
                    if (user.GetLogin() == ulogin)
                    {
                        currentuser = user;
                    }
                }

                return default;
            }

            void CheckUserBooks()
            {
                foreach (var book in currentuser.GetTakenBooks())
                {
                    if(book.GetName() == chosenbook)
                    {
                        MessageBox.Show("Данная книга сейчас у вас на руках!");
                        new TakeBookWindow(userlogin, new List<string> { "", "", "", "" });
                        Close();
                    }
                }

                foreach (var book in currentuser.GetOrderedBooks())
                {
                    if (book.GetName() == chosenbook)
                    {
                        MessageBox.Show("Данная книга сейчас забронирована вами!");
                        new TakeBookWindow(userlogin, new List<string> { "", "", "", "" });
                        Close();
                    }
                }

                MessageBox.Show("Книга забронирована! Вы можете получить её в течении 7 дней!");

                currentuser.AddOrderBook(new OrderBook(currentbook.GetName(), currentbook.GetAuthor(), currentbook.GetAgeRating(), currentbook.GetDescription(), currentbook.GetGenre(), DateTime.Today.AddDays(7))); // добавили книгу в список забронированных книг пользователя

                foreach (var book in books)
                {
                    if (book == currentbook)
                    {
                        book.Dicrease();
                        break;
                    }
                }
            }
        }
    }
}
