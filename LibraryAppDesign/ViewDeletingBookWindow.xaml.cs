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
    /// Логика взаимодействия для ViewDeletingBookWindow.xaml
    /// </summary>
    public partial class ViewDeletingBookWindow : Window
    {
        public ViewDeletingBookWindow(string tag, string login)
        {
            _storage = Factory.GetInstance().Storage;

            admin = login;
            bookname = tag;

            currentbook = GetCurrentBook(bookname);

            InitializeComponent();
        }

        string admin;
        string bookname;

        static IStorage _storage;

        BookInLibrary currentbook;

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            //_storage.SaveBooks();
            new DeletingBookWindow(new List<string> { "", "" }, admin).Show();
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            new CheckDeletingBookWindow(bookname, admin).Show();
            Close();
        }

        private void BookName_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = currentbook.GetName();
        }

        private void AuthorName_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;
            AuthorName.Text = String.Join(", ", currentbook.GetAuthor());
        }

        private void DescriptionName_Initialized(object sender, EventArgs e)
        {
            TextBlock DescriptionName = sender as TextBlock;
            DescriptionName.Text = currentbook.GetDescription();
        }

        BookInLibrary GetCurrentBook(string chosenbook)
        {
            foreach (var book in _storage.GetBooks)
            {
                if (book.GetName() == chosenbook)
                {
                    return book;
                }
            }

            return default;
        }
    }
}
