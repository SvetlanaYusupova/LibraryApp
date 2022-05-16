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
    /// Interaction logic for TakenBookWindow.xaml
    /// </summary>
    public partial class TakenBookWindow : Window
    {
        public TakenBookWindow(string login)
        {
            PastUserBook();
            _storage = new Storage();
            InitializeComponent();
            userlogin = login;
            PastUserBook();
            TakenBooksListBox.ItemsSource = takenBook;
        }

        string userlogin;

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<TakenBook> takenBook = new List<TakenBook> { };
        
        

        private void PastUserBook()
        {
            foreach (var us in users)
            {
                if (us.GetLogin() == userlogin)
                {
                    takenBook = us.GetTakenBook();
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
            Book book = BookName.DataContext as Book;
            BookName.Text = book.GetBookName();
        }


        private void AuthorBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;
            TakenBook book = AuthorName.DataContext as TakenBook;
            string authors = String.Join(", ", book.GetAuthor());
            AuthorName.Text = authors;
        }
        private void GenreBook_Initialized(object sender, EventArgs e)
        {
            TextBlock GenreName = sender as TextBlock;
            TakenBook book = GenreName.DataContext as TakenBook;
            GenreName.Text = book.GetGenre();
        }

        private void StartDate_Initialized(object sender, EventArgs e)
        {
            TextBlock StartDate = sender as TextBlock;
            TakenBook book = StartDate.DataContext as TakenBook;
            DateTime startDate = book.GetStartDate();
            string start = startDate.ToString("dd/mm/yyyy");
            StartDate.Text = start;
        }

        private void EndDate_Initialized(object sender, EventArgs e)
        {
            TextBlock EndDate = sender as TextBlock;

            TakenBook book = EndDate.DataContext as TakenBook;
            DateTime endDate = book.GetEndDate();
            string end = endDate.ToString("dd/mm/yyyy");
            EndDate.Text = end;
        }

    }
}
