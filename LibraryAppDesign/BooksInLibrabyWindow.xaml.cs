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
    /// Interaction logic for BooksInLibrabyWindow.xaml
    /// </summary>
    public partial class BooksInLibrabyWindow : Window
    {
        public BooksInLibrabyWindow(string login, List<string> filters)
        {


            this.login = login;
            _storage = new Storage();
            filters4Book = filters;

            bookInLibrary = _storage.Books;


            ChooseBooks();
            InitializeComponent();

            BooksInLibraryListBox.ItemsSource = filtersBooks;
        }




        string login;
        static Storage _storage = new Storage();
        
        List<BookInLibrary> bookInLibrary = new List<BookInLibrary> { };
        List<string> filters4Book;
        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };

        List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };



   
        private void LogOut(object sender, RoutedEventArgs e)
        {
            new EditBookWindow(login).Show();
            Close();
        }

        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            Book book = BookName.DataContext as Book;
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
        private void ButtonBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;
            BookInLibrary book = ChooseBook.DataContext as BookInLibrary;
            ChooseBook.Tag = book.GetName();
        }


        private void ChooseBook(object sender, RoutedEventArgs e)
        {
            // для кнопки выбора книги


            Button ChooseBook = sender as Button;


            // new View1BookWindow(userlogin, ChooseBook.Tag.ToString()).Show();
            // new ExtendBookingWindow(userlogin, ChooseBook.Tag.ToString()).Show();
            //new ExtendingOrderedBookWindow(login, ChooseBook.Tag.ToString(), "Taken").Show();
            new ChangeBookPropertiesWindow(login, ChooseBook.Tag.ToString()).Show();

             Close();
        }        
        private void ChooseBooks()
        {
            foreach (var item in bookInLibrary)
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



        private void TitleName_Initialized(object sender, EventArgs e)
        {

            ComboBox TitleName = sender as ComboBox;
            foreach (var item in bookInLibrary)
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
            foreach (var item in bookInLibrary)
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

        private void Apply(object sender, RoutedEventArgs e)
        {
            List<string> fil = new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString() };
            new BooksInLibrabyWindow(login, fil).Show();
            Close();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            //для кнопки сброса фильтров
            new BooksInLibrabyWindow(login, new List<string> { "", "" }).Show();
            Close();
        }
    }
}

