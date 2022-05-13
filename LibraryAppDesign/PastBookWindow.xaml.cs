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
    /// Interaction logic for PastBookWindow.xaml
    /// </summary>
    public partial class PastBookWindow : Window
    {
            public PastBookWindow(string login)
            {
                PastUserBook();
                _storage = new Storage();
                InitializeComponent();
                userlogin = login;
                PastUserBook();
                PastBooksListBox.ItemsSource = pastBook;
            }

            string userlogin;

            static Storage _storage = new Storage();
            List<User> users = _storage.Users;
            List<List<string>> pastBook = new List<List<string>> { };


            //получение списка прошлых книг конкретного юзера через логин
            private void PastUserBook()
            {
                foreach (var us in users)
                {
                    if (us.GetLogin() == userlogin)
                    {
                        pastBook = us.GetPastBook();
                    }
                }
            }
            //выход в предыдущее окно
            private void LogOut(object sender, RoutedEventArgs e)
            {
                new WindowUser(userlogin).Show();
                Close();
            }


            //Инициализация названия книги в списке
            private void NameBook_Initialized(object sender, EventArgs e)
            {
                TextBlock BookName = sender as TextBlock;
                List<string> book = BookName.DataContext as List<string>;
                BookName.Text = book[0];
            }

            //Инициализация автора книги в списке
            private void AuthorBook_Initialized(object sender, EventArgs e)
            {
                TextBlock AuthorName = sender as TextBlock;
                List<string> book = AuthorName.DataContext as List<string>;
                AuthorName.Text = book[1];
            }

            //Инициализация жанра книги в списке
            private void GenreBook_Initialized(object sender, EventArgs e)
            {
                TextBlock GenreName = sender as TextBlock;
                List<string> book = GenreName.DataContext as List<string>;
                GenreName.Text = book[2];
            }


            //Инициализация даты начала (бронирования книги)
            private void StartDate_Initialized(object sender, EventArgs e)
            {
                TextBlock StartDate = sender as TextBlock;
                List<string> book = StartDate.DataContext as List<string>;
                StartDate.Text = book[3];
        }

            //Инициализация даты конца(бронирования книги)
            private void EndDate_Initialized(object sender, EventArgs e)
            {
                TextBlock EndDate = sender as TextBlock;
                List<string> book = EndDate.DataContext as List<string>;
                EndDate.Text = book[4];
            }

    }
}
