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
    /// Логика взаимодействия для View2BookWindow.xaml
    /// </summary>
    public partial class View2BookWindow : Window
    {
        public View2BookWindow(string login, string action, string tag, string logAdmin)
        {
            _storage = Factory.GetInstance().Storage;

            chosenbook = tag;
            currentaction = action;
            userlogin = login;
            admin = logAdmin;

            books = _storage.GetBooks;
       
            // текущий пользователь
            currentuser = GetCurrentUser(userlogin);
            // выбранная пользователем книга
            currentbook = GetCurrentBook(chosenbook);

            InitializeComponent();
        }

        string currentaction;
        string chosenbook;
        string userlogin;
        string admin;

        User currentuser;
        OrderBook currentbook;

        static IStorage _storage;
        List<BookInLibrary> books;

        private void GoOut(object sender, RoutedEventArgs e)
        {
            _storage.Save();
            new GiveBookWindow(userlogin, currentaction, new List<string> { "", "", "", "" }, admin).Show();
            Close();
        }

        

        // инициализация названия книги
        private void NameBook_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = currentbook.GetBookName();
        }

        // инициализация автор(а/ов) книги
        private void AuthorName_Initialized(object sender, EventArgs e)
        {
            TextBlock AuthorName = sender as TextBlock;
            AuthorName.Text = String.Join(", ", currentbook.GetAuthor());
        }

        // инициализация жанра книги
        private void GenreName_Initialized(object sender, EventArgs e)
        {
            TextBlock GenreName = sender as TextBlock;
            GenreName.Text = currentbook.GetGenre();
        }

        // инициализация названия книги
        private void AgeName_Initialized(object sender, EventArgs e)
        {
            TextBlock AgeName = sender as TextBlock;
            AgeName.Text = currentbook.GetAgeRating();
        }

        // инициализация описания книги
        private void DescriptionName_Initialized(object sender, EventArgs e)
        {
            TextBlock DescriptionName = sender as TextBlock;
            DescriptionName.Text = currentbook.GetDescription();
        }

        // функция для получения экземпляра книги, выбранной пользователем, по названию
        private OrderBook GetCurrentBook(string chosenbook)
        {
            foreach (var book in currentuser.GetOrderBook())
            {
                if (book.GetBookName() == chosenbook)
                {
                    return book;
                }
            }

            return default;
        }

        // функция для получения определённого пользователя
        private User GetCurrentUser(string ulogin)
        {
            foreach (var user in _storage.GetUsers)
            {
                if (user.GetLogin() == ulogin)
                {
                    return user;
                }
            }

            return default;
        }

        private void GiveBook(object sender, RoutedEventArgs e)
        {
            Button Givebook = sender as Button;

            foreach (var user in _storage.GetUsers)
            {
                if (user == currentuser)
                {
                    user.AddTakenBook(new TakenBook(currentbook.GetBookName(), currentbook.GetAuthor(), currentbook.GetAgeRating(), currentbook.GetDescription(), currentbook.GetGenre(), currentbook.GetPublishedYear(), currentbook.GetRating(), currentbook.GetNumPages(), DateTime.Today, DateTime.Today.AddDays(30)));
                    user.DicreaseOrderBook(currentbook);
                }
            }

            _storage.Save();

            MessageBox.Show("Книга успешно выдана!");
            new WindowAdmin(admin).Show();
            Close();


        }

        private void FromOrderToTaken(OrderBook orderbook)
        {

        }
    }
}
