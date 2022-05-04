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
    /// Логика взаимодействия для TakeBookWindow.xaml
    /// </summary>
    public partial class TakeBookWindow : Window
    {
        public TakeBookWindow(string login, List<string> filters)
        {
            InitializeComponent();
            userlogin = login;
            filters4Book = filters;
            ChoseBooks();
            BooksListBox.ItemsSource = filtersBooks;
        }

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<BookInLibrary> books = _storage.Books;
        List<Admin> admins = _storage.Admins;
        List<Notification> notifications = _storage.Notifications;

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };
        string userlogin;
        List<string> filters4Book;
        List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };

        

        private void Account(object sender, RoutedEventArgs e)
        {
            //для кнопки внесения изменений в данные пользователя
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для кнопки показа уведомлений пользователя
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            //для кнопки применения фильтров
            /*TextBox TitleName = sender as TextBox;
            TextBox AuthorName = sender as TextBox;*/

            /*ComboBox GenreName = sender as ComboBox;
            ComboBox AgeName = sender as ComboBox;*/
            //Close();
            List<string> fil = new List<string> { };
            if (TitleName.Text == null)
            {
                fil.Add(null);
            }
            else
            {
                fil.Add(TitleName.Text);
            }
            if (AuthorName.Text == null)
            {
                fil.Add(null);
            }
            else
            {
                fil.Add(AuthorName.Text);
            }

            if (GenreName.SelectedItem == null)
            {
                fil.Add(null);
            }
            else
            {
                fil.Add(GenreName.SelectedItem.ToString());
            }
            if (AgeName.SelectedItem == null)
            {
                fil.Add(null);
            }
            else
            {
                fil.Add(AgeName.SelectedItem.ToString());
            }
            new TakeBookWindow(userlogin, fil).Show();
            //new TakeBookWindow(userlogin, new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString(), GenreName.SelectedItem.ToString(), AgeName.SelectedItem.ToString() }).Show();
            Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowUser(userlogin).Show();
            Close();
        }

        private void NameGenre_Initialized(object sender, EventArgs e)
        {

            ComboBox GenreName = sender as ComboBox;
            foreach (var item in books)
            {
                if (!genres.Contains(item.GetGenre()))
                {
                    genres.Add(item.GetGenre());
                }
            }

            foreach (var item in genres)
            {
                GenreName.Items.Add(item);
            }
        }


        private void AgeName_Initialized(object sender, EventArgs e)
        {

            ComboBox AgeName = sender as ComboBox;
            foreach (var item in books)
            {
                if (!ageRatings.Contains(item.GetAgeRating()))
                {
                    ageRatings.Add(item.GetAgeRating());
                }
            }

            foreach (var item in ageRatings)
            {
                AgeName.Items.Add(item);
            }
        }

        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;

            BookInLibrary book = BookName.DataContext as BookInLibrary;

            BookName.Text = book.GetName();
        }

        private void AuthorBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;

            BookInLibrary book = AuthorName.DataContext as BookInLibrary;
            string authors = String.Join(", ", book.GetAuthor());

            AuthorName.Text = authors;
        }
        private void GenreBook_Initialized(object sender, EventArgs e)
        {
            TextBlock GenreName = sender as TextBlock;

            BookInLibrary book = GenreName.DataContext as BookInLibrary;

            GenreName.Text = book.GetGenre();
        }
        private void AgeBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AgeName = sender as TextBlock;

            BookInLibrary book = AgeName.DataContext as BookInLibrary;

            AgeName.Text = book.GetAgeRating();
        }

        private void ChoseBooks()
        {
            foreach (var item in books)
            {
                if (new List<string> { null, "", item.GetName()}.Contains(filters4Book[0]) &&
                    (item.GetAuthor().Contains(filters4Book[1]) || filters4Book[1] == null || filters4Book[1] =="") &&
                    new List<string> { null, "",  item.GetGenre() }.Contains(filters4Book[2]) &&
                    new List<string> { null, "", item.GetAgeRating() }.Contains(filters4Book[3]))
                {
                    filtersBooks.Add(item);
                }
            }
        }
}
}
