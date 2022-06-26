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
    /// Логика взаимодействия для DeletingBookWindow.xaml
    /// </summary>
    public partial class DeletingBookWindow : Window
    {
        public DeletingBookWindow(List<string> filters, string login)
        {
            _storage = new Storage();
            books = _storage.Books;

            InitializeComponent();
            admin = login;
            filters4Book = filters;

            ChooseBooks();
            BooksListBox.ItemsSource = filtersBooks;
        }

        string admin;
        static Storage _storage;
        List<BookInLibrary> books;

        List<string> filters4Book;
        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };
        List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            List<string> fil = new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString() };
            void AddFill(object obj)
            {
                if (obj == null)
                {
                    fil.Add("");
                }
                else
                {
                    fil.Add(obj.ToString());
                }
            }
            new DeletingBookWindow(fil, admin).Show();
            Close();
        }

        private void TitleName_Initialized(object sender, EventArgs e)
        {
            ComboBox TitleName = sender as ComboBox;
            foreach (var item in books)
            {
                if (!titlenames.Contains(item.GetName()))
                {
                    titlenames.Add(item.GetName());
                }
            }

            foreach (var item in titlenames)
            {
                TitleName.Items.Add(item);
            }
        }

        private void AuthorName_Initialized(object sender, EventArgs e)
        {
            ComboBox AuthorName = sender as ComboBox;
            foreach (var item in books)
            {
                foreach (var author in item.GetAuthor())
                {
                    if (!authornames.Contains(author))
                    {
                        authornames.Add(author);
                    }
                }
            }

            foreach (var item in authornames)
            {
                AuthorName.Items.Add(item);
            }
        }


        private void BookName_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;

            BookInLibrary book = BookName.DataContext as BookInLibrary;

            BookName.Text = book.GetName();
        }

        private void BookAuthor_Initialized(object sender, EventArgs e)
        {
            TextBlock BookAuthor = sender as TextBlock;

            BookInLibrary book = BookAuthor.DataContext as BookInLibrary;
            string authors = String.Join(", ", book.GetAuthor());

            BookAuthor.Text = authors;
        }

        private void ChooseBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;

            BookInLibrary book = ChooseBook.DataContext as BookInLibrary;

            ChooseBook.Tag = book.GetName();
        }

        private void ChooseBook_Click(object sender, RoutedEventArgs e)
        {
            Button ChooseBook = sender as Button;
            new ViewDeletingBookWindow(ChooseBook.Tag.ToString(), admin).Show();
            Close();
        }

        private void ChooseBooks()
        {
            foreach (var item in books)
            {
                if ((item.GetName().ToLower().Contains(filters4Book[0].ToLower()) || filters4Book[0] == "") &&
                    CheckAuthor(item.GetAuthor(), filters4Book[1]))
                {
                    filtersBooks.Add(item);
                }

                bool CheckAuthor(List<string> authors, string author)
                {
                    bool answer = false;
                    if (author == "")
                    {
                        answer = true;
                    }
                    else
                    {
                        foreach (var a in authors)
                        {
                            if (a.ToLower().Contains(author.ToLower()))
                            {
                                answer = true;
                            }
                        }
                    }

                    return answer;
                }
            }
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            new DeletingBookWindow(new List<string> { "", "" }, admin).Show();
            Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new EditBookWindow(admin).Show();
            Close();
        }
    }
}
