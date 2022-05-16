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
    /// Логика взаимодействия для AcceptBookWindow.xaml
    /// </summary>
    public partial class AcceptBookWindow : Window
    {
        public AcceptBookWindow(string userLogin)
        {
            InitializeComponent();
            filters4Book = new List<string> { "", "", "", "" };
            ChoseBooks();
            BooksListBox.ItemsSource = filtersBooks;
        }


        static Storage _storage = new Storage();
        //List<BookInLibrary> books = _storage.Books;

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };
        //List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };
        string userlogin;
        List<string> filters4Book;
        List<BookInLibrary> filtersBooks = new List<BookInLibrary> { };


        private void Return(object sender, RoutedEventArgs e)
        {
            //для кнопки сброса фильтров
            new AcceptBookWindow().Show();
            Close();
        }

        private void ChooseBook(object sender, RoutedEventArgs e)
        {
            //для кнопки выбора книги
            Button ChooseBook = sender as Button;
            new View1BookWindow(userlogin, ChooseBook.Tag.ToString()).Show();
            //new TakeBookWindow(userlogin, new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString(), GenreName.SelectedItem.ToString(), AgeName.SelectedItem.ToString() }).Show();
            Close();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            //для кнопки применения фильтров
            /*TextBox TitleName = sender as TextBox;
            TextBox AuthorName = sender as TextBox;*/

            /*ComboBox GenreName = sender as ComboBox;
            ComboBox AgeName = sender as ComboBox;*/

            List<string> fil = new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString() };

            AddFill(GenreName.SelectedItem);
            AddFill(AgeName.SelectedItem);
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
            new TakeBookWindow(userlogin, fil).Show();
            Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin().Show();
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

        private void GenreName_Initialized(object sender, EventArgs e)
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
        private void ButtonBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;

            BookInLibrary book = ChooseBook.DataContext as BookInLibrary;

            ChooseBook.Tag = book.GetName();
        }

        private void ChoseBooks()
        {
            foreach (var item in books)
            {
                if ((item.GetName().ToLower().Contains(filters4Book[0].ToLower()) || filters4Book[0] == "") &&
                    (CheckAuthor(item.GetAuthor(), filters4Book[1])) &&
                    new List<string> { "", item.GetGenre() }.Contains(filters4Book[2]) &&
                    new List<string> { "", item.GetAgeRating() }.Contains(filters4Book[3]))
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
    }
}
