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
    /// Логика взаимодействия для GiveBookWindow.xaml
    /// </summary>
    public partial class GiveBookWindow : Window
    {
        public GiveBookWindow(string login, string action, List<string> filters, string admLogin)
        {
            _storage = new Storage();
            users = _storage.Users;

            currentuser = login;
            currentaction = action;
            admin = admLogin;

            filters4Book = filters;

            InitializeComponent();
            ChoseBooks();
            BooksListBox.ItemsSource = filtersBooks;
        }

        string currentuser;
        string currentaction;
        string admin;

        static Storage _storage;
        List<User> users;

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };
        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };

        List<string> filters4Book;

        List<OrderBook> filtersBooks = new List<OrderBook> { };
        

        private void Return(object sender, RoutedEventArgs e)
        {
            //для кнопки сброса фильтров
            new GiveBookWindow(currentuser, currentaction, new List<string> { "", "", "", "" }, admin).Show();
            Close();
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
            new GiveBookWindow(currentuser, currentaction, fil, admin).Show();
            Close();
        }

        private void ChooseBook(object sender, RoutedEventArgs e)
        {
            //для кнопки выбора книги
            Button ChooseBook = sender as Button;
            new View2BookWindow(currentuser, currentaction, ChooseBook.Tag.ToString(), admin).Show();
            Close();
        }

        private void TitleName_Initialized(object sender, EventArgs e)
        {

            ComboBox TitleName = sender as ComboBox;

            foreach (var item in GetUser(currentuser))
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
            foreach (var item in GetUser(currentuser))
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
            foreach (var item in GetUser(currentuser))
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
            foreach (var item in GetUser(currentuser))
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

            OrderBook book = BookName.DataContext as OrderBook;

            BookName.Text = book.GetBookName();
        }

        private void AuthorBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;

            OrderBook book = AuthorName.DataContext as OrderBook;
            string authors = String.Join(", ", book.GetAuthor());

            AuthorName.Text = authors;
            
        }

        private void GenreBook_Initialized(object sender, EventArgs e)
        {
            TextBlock GenreName = sender as TextBlock;

            OrderBook book = GenreName.DataContext as OrderBook;

            GenreName.Text = book.GetGenre();
        }

        private void AgeBook_Initialized(object sender, EventArgs e)
        {
            TextBlock AgeName = sender as TextBlock;

            OrderBook book = AgeName.DataContext as OrderBook;

            AgeName.Text = book.GetAgeRating();        
        }

        private void ButtonBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;

            OrderBook book = ChooseBook.DataContext as OrderBook;

            ChooseBook.Tag = book.GetBookName();
        }

        private void ChoseBooks()
        {
            foreach (var item in GetUser(currentuser))
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

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new UserChooseWindow(currentaction, admin).Show();
            Close();
        }

        public List<OrderBook> GetUser(string currentuser)
        {
            List<OrderBook> orderbooks = new List<OrderBook>();

            foreach (var user in users)
            {
                if (user.GetLogin() == currentuser)
                {
                    orderbooks = user.GetOrderBook();
                }
            }

            return orderbooks;
        }
    }
}
