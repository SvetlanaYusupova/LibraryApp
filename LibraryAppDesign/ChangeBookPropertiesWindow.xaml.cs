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
    /// Interaction logic for ChangeBookPropertiesWindow.xaml
    /// </summary>
    public partial class ChangeBookPropertiesWindow : Window
    {
        public ChangeBookPropertiesWindow(string admlogin, string tag)
        {
            login = admlogin;
            _storage = new Storage();
            books = _storage.Books;

            chosenbook = tag;
            //filters4Book = filters;
            // PresentUserBook();
            // ChooseBooks();
            currentbook = GetCurrentBook(chosenbook);

            InitializeComponent();

            BookNametextBox.Text = currentbook.GetName();
            AgeRatingtextBox.Text = currentbook.GetAgeRating();
            DescriptiontextBox.Text = currentbook.GetDescription();
            GenretextBox.Text = currentbook.GetGenre();

            AuthorsListBox.ItemsSource = currentbook.GetAuthor();


            foreach (var it in AuthorsListBox.Items)
            {
                authorsList.Add(it.ToString());
            }
        }
        string chosenbook;
        string login;
        Storage _storage;
        List<BookInLibrary> books;
        BookInLibrary bLibrary;
        TakenBook tb;
        
        BookInLibrary currentbook;

        List<string> subjects = new List<string> { "0+", "6+", "12+", "16+", "18+" };


        List<string> authorsList;

        //List<User> users = _storage.Users;
        //List<List<string>> pastBook = new List<List<string>> { };



        BookInLibrary GetCurrentBook(string chosenbook)
        {
            foreach (var book in _storage.Books)
            {
                if (book.GetName() == chosenbook)
                {
                    return book;
                }
            }

            return default;
        }
        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            new BooksInLibrabyWindow(login, new List<string> { }).Show();
           // new WindowAdmin(login).Show();
            Close();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //bool doing = true;
            //TextBlock GenreName = sender as TextBlock;
            //List<BookInLibrary> books = new List<BookInLibrary>{ };

            //BookInLibrary book = GenreName.DataContext as BookInLibrary;
            //book.GetName();



            if (BookNametextBox.Text != "" & AgeRatingtextBox.Text != "" & DescriptiontextBox.Text != "" & GenretextBox.Text != ""
                & AuthorsListBox.ItemStringFormat != "")
            {

                foreach (var bk in _storage.Books)
                {



                    bk.SetName(BookNametextBox.Text);
                    // Name = BookNametextBox.Text;

                    bk.SetAgeRating(AgeRatingtextBox.Text);
                    bk.SetDescription(DescriptiontextBox.Text);
                    bk.SetGenre(GenretextBox.Text);
                  //  bk.SetAuthor(AuthorsListBox.ItemStringFormat);
                    bk.SetAuthor(authorsList);

                    _storage.SaveBooks();


                }
            }

            if (bLibrary.GetBookName().Contains(BookNametextBox.Text))
            {
                MessageBox.Show("Книга с таким названием уже существует");
            }
            if (tb.GetBookName().Contains(BookNametextBox.Text))
            {
                tb.SetAgeRating(AgeRatingtextBox.Text);
                tb.SetDescription(DescriptiontextBox.Text);
                tb.SetGenre(GenretextBox.Text);
                //bk.SetAuthor(AuthorsListBox.ItemStringFormat);
                tb.SetAuthor(authorsList);
            }
            if (tb.GetBookName().Contains(BookNametextBox.Text))
            {
                tb.SetAgeRating(AgeRatingtextBox.Text);
                tb.SetDescription(DescriptiontextBox.Text);
                tb.SetGenre(GenretextBox.Text);
                //bk.SetAuthor(AuthorsListBox.ItemStringFormat);
                tb.SetAuthor(authorsList);
            }
            else
            {
                MessageBox.Show("Поля незаполнены");
            }


        }
            
    

        private void AgeRatingtextBox_Initialized(object sender, EventArgs e)
        {
            ComboBox AgeRatingtextBox = sender as ComboBox;
            foreach (var item in subjects)
            {
                AgeRatingtextBox.Items.Add(item);
            }
        }
    }
}
