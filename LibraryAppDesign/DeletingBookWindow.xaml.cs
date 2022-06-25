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
    /// Логика взаимодействия для DeletingBookWindow.xaml
    /// </summary>
    public partial class DeletingBookWindow : Window
    {
        public DeletingBookWindow(List<string> filters, string login)
        {
            InitializeComponent();
            admin = login;
            filters4Book = filters;
            _storage = new Storage();

            
            BooksListBox.ItemsSource = filtersBooks;
        }

        string admin;
        static Storage _storage = new Storage();
        List<BookInLibrary> books = _storage.Books;

        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };
        string userlogin;
        List<string> filters4Book;
        List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };

        private void Apply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TitleName_Initialized(object sender, EventArgs e)
        {

        }

        private void AuthorName_Initialized(object sender, EventArgs e)
        {

        }

        private void ChooseBook_Initialized(object sender, EventArgs e)
        {

        }

        private void ChooseBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new EditBookWindow(admin).Show();
            Close();
        }
    }
}
