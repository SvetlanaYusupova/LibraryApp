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
    /// Interaction logic for BookingBookWindow.xaml
    /// </summary>
    public partial class OrderedBookWindow : Window
    {
        public OrderedBookWindow(string login, List<string> filters)
        {


            userlogin = login;
            _storage = new Storage();
            filters4Book = filters;
            PresentUserBook();
            ChooseBooks();

            InitializeComponent();

            OrderedBooksListBox.ItemsSource = filtersBooks;
            //OrderedBooksListBox.ItemsSource = filtersBooks;
        }

        string userlogin;
        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<OrderBook> orderedBook = new List<OrderBook> { };
        List<string> filters4Book;
        List<string> titlenames = new List<string> { };
        List<string> authornames = new List<string> { };

        List<OrderBook> filtersBooks = new List<OrderBook> { };


        private void PresentUserBook()
        {
            foreach (var user in users)
            {
                if (user.GetLogin() == userlogin)
                {
                    orderedBook = user.GetOrderBook();
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
            Book book = BookName.DataContext as Book;
            BookName.Text = book.GetName();
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


        //private void StartDate_Initialized(object sender, EventArgs e)
        //{
        //    TextBlock StartDate = sender as TextBlock;
        //    OrderBook book = StartDate.DataContext as OrderBook;
        //    string start = book.GetStartDate().ToShortDateString().ToString();
        //    StartDate.Text = start;
        //}

        private void EndDate_Initialized(object sender, EventArgs e)
        {
            TextBlock EndDate = sender as TextBlock;
            OrderBook book = EndDate.DataContext as OrderBook;
            string end = book.GetEndDate().ToShortDateString().ToString();
            EndDate.Text = end;
        }

        private void ButtonBook_Initialized(object sender, EventArgs e)
        {
            Button ChooseBook = sender as Button;
            OrderBook book = ChooseBook.DataContext as OrderBook;
            ChooseBook.Tag = book.GetName();
        }

        private void ChooseBook(object sender, RoutedEventArgs e)
        {
            // для кнопки выбора книги
            Button ChooseBook = sender as Button;
            // new View1BookWindow(userlogin, ChooseBook.Tag.ToString()).Show();
            new ExtendingOrderedBookWindow(userlogin, ChooseBook.Tag.ToString(), "Booked").Show();
            //new TakeBookWindow(userlogin, new List<string> { TitleName.Text.ToString(), AuthorName.Text.ToString(), GenreName.SelectedItem.ToString(), AgeName.SelectedItem.ToString() }).Show();
            Close();
        }

        private void ChooseBooks()
        {
            foreach (var item in orderedBook)
            {
                if ((item.GetName().ToLower().Contains(filters4Book[0].ToLower()) || filters4Book[0] == "") &&
                    (CheckAuthor(item.GetAuthor(), filters4Book[1])))
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
            foreach (var item in orderedBook)
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
            foreach (var item in orderedBook)
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
            new OrderedBookWindow(userlogin, fil).Show();
            Close();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            //для кнопки сброса фильтров
            new OrderedBookWindow(userlogin, new List<string> { "", "" }).Show();
            Close();
        }

    }
}

