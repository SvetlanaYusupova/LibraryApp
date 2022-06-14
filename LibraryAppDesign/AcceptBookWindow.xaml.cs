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
        public AcceptBookWindow(string userLg, List<string> filt)
        {
            foreach (var us in _storage.Users)
            {
                if (us.GetLogin() == userLg)
                {
                    user = us;
                    books = us.GetUsersBook();
                }
            }
            allBooks = _storage.Books;
            InitializeComponent();
            filters4Book = filt;
            ChoseBooks();
            BooksListBox.ItemsSource = filtersBooks;
        }

        User user;
        static Storage _storage = new Storage();
        List<TakenBook> books;  //книги на руках у пользователя
        List<BookInLibrary> allBooks; //все книги в библиотеке

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };
        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };
        List<string> filters4Book;
        List<TakenBook> filtersBooks = new List<TakenBook> { };


        private void Return(object sender, RoutedEventArgs e)
        {
            //для кнопки сброса фильтров
            new AcceptBookWindow(user.GetLogin(), new List<string> { "", "", "", "" }).Show();
            Close();
        }

        private void ChooseBook(object sender, RoutedEventArgs e)
        {
            //для кнопки выбора книги
            Button ChooseBook = sender as Button;
            string nameBook = ChooseBook.Tag.ToString();
            foreach (TakenBook book in books)
            {
                if (book.GetBookName() == nameBook)
                {
                    user.AddPastBook(new List<string> { $"{nameBook}", $"{string.Join(", ", book.GetAuthor())}", $"{book.GetAgeRating()}", $"{book.GetGenre()}" });
                    foreach (BookInLibrary book2 in allBooks)
                    {
                        if (book2.GetBookName() == nameBook)
                        {
                            book2.AddOneBook();
                        }
                    }
                    books.Remove(book);
                    MessageBox.Show("Книга принята.");
                    if (books.Count != 0)
                    {
                        new AcceptBookWindow(user.GetLogin(), new List<string> { "", "", "", "" }).Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("У данного пользователя больше нет взятых книг.");
                        //
                        new UserChooseWindow().Show();
                        Close();
                    }
                    break;
                }
            }
            _storage.SaveUsers();
            //new View1BookWindow(userLogin, ChooseBook.Tag.ToString()).Show();

            //new TakeBookWindow(userlogin, new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString(), GenreName.SelectedItem.ToString(), AgeName.SelectedItem.ToString() }).Show();
            //Close();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            //для кнопки применения фильтров
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
            new AcceptBookWindow(user.GetLogin(), fil).Show();
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
                if (!titlenames.Contains(item.GetBookName()))
                {
                    titlenames.Add(item.GetBookName());
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

            TakenBook book = BookName.DataContext as TakenBook;

            BookName.Text = book.GetBookName();
        }

        private void AuthorBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;

            TakenBook book = AuthorName.DataContext as TakenBook;
            string authors = string.Join(", ", book.GetAuthor());

            AuthorName.Text = authors;
        }
        private void GenreBook_Initialized(object sender, EventArgs e)
        {
            TextBlock GenreName = sender as TextBlock;

            TakenBook book = GenreName.DataContext as TakenBook;

            GenreName.Text = book.GetGenre();
        }
        private void AgeBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AgeName = sender as TextBlock;

            TakenBook book = AgeName.DataContext as TakenBook;

            AgeName.Text = book.GetAgeRating();
        }
        private void ButtonBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;

            TakenBook book = ChooseBook.DataContext as TakenBook;

            ChooseBook.Tag = book.GetBookName();
        }

        private void ChoseBooks()
        {
            foreach (var item in books)
            {
                if ((item.GetBookName().ToLower().Contains(filters4Book[0].ToLower()) || filters4Book[0] == "") &&
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
