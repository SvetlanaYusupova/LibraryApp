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
            _storage = Factory.GetInstance().Storage;
            users = _storage.GetUsers;
            PastUserBook();

            InitializeComponent();
            userlogin = login;
            
            PastBooksListBox.ItemsSource = pastBook;
        }

        string userlogin;

        static IStorage _storage;
        List<User> users;
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

        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            List<string> book = BookName.DataContext as List<string>;
            BookName.Text = book[0];
        }

        private void AgeRating_Initialized(object sender, EventArgs e)
        {
            TextBlock AgeRating = sender as TextBlock;
            List<string> book = AgeRating.DataContext as List<string>;
            AgeRating.Text = book[2];
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
            GenreName.Text = book[3];
        }

        private void BookRate_Initialized(object sender, EventArgs e)
        {
            Button BookRate = sender as Button;
            List<string> book = BookRate.DataContext as List<string>;

            BookRate.Tag = book[4];
        }

        private void BookRate_Click(object sender, RoutedEventArgs e)
        {
            Button RateBook = sender as Button;
            new RatingBookWindow(RateBook.Tag.ToString(), userlogin).Show();
            Close();
        }


    }
}
