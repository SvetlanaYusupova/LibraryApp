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

            private void LogOut(object sender, RoutedEventArgs e)
            {
                new WindowUser(userlogin).Show();
                Close();
            }

            //private void NameGenre_Initialized(object sender, EventArgs e)
            //{

            //    ComboBox GenreName = sender as ComboBox;
            //    foreach (var item in takenBook)
            //    {
            //        if (!genres.Contains(item.GetGenre()))
            //        {
            //            genres.Add(item.GetGenre());
            //        }
            //    }

            //    foreach (var item in genres)
            //    {
            //        GenreName.Items.Add(item);
            //    }
            //}
            private void NameBook_Initialized(object sender, EventArgs e)
            {
                TextBlock BookName = sender as TextBlock;
                List<string> book = BookName.DataContext as List<string>;
                BookName.Text = book[0];
            }


            private void AuthorBook_Initialized(object sender, EventArgs e)
            {
                TextBlock AuthorName = sender as TextBlock;
                List<string> book = AuthorName.DataContext as List<string>;
                AuthorName.Text = book[1];
            }
            private void GenreBook_Initialized(object sender, EventArgs e)
            {
                TextBlock GenreName = sender as TextBlock;
                List<string> book = GenreName.DataContext as List<string>;
                GenreName.Text = book[2];
            }

            private void StartDate_Initialized(object sender, EventArgs e)
            {
                TextBlock StartDate = sender as TextBlock;
                List<string> book = StartDate.DataContext as List<string>;
                StartDate.Text = book[3];
            }

            private void EndDate_Initialized(object sender, EventArgs e)
            {
                TextBlock EndDate = sender as TextBlock;
                List<string> book = EndDate.DataContext as List<string>;
                EndDate.Text = book[4];
            }

    }
}
