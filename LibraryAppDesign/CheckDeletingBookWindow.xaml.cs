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
    /// Логика взаимодействия для CheckDeletingBookWindow.xaml
    /// </summary>
    public partial class CheckDeletingBookWindow : Window
    {
        public CheckDeletingBookWindow(string tag, string login)
        {
            _storage = Factory.GetInstance().Storage;
            bookname = tag;
            admin = login;
            InitializeComponent();
        }

        string bookname;
        string admin;

        static IStorage _storage;

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new ViewDeletingBookWindow(bookname, admin).Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckBook(bookname))
            {
                MessageBox.Show("Книга не может быть удалена, так как на данный момент она забронирована или находится на руках у пользователя!");
                new ViewDeletingBookWindow(bookname, admin).Show();
                Close();
            }
            else
            {
                _storage.GetBooks.Remove(GetCurrentBook(bookname));
                _storage.Save();

                MessageBox.Show("Книга удалена из библиотеки!");
                new DeletingBookWindow(new List<string> { "", "" }, admin).Show();
                Close();
            }

            
        }

        bool CheckBook(string bookName)
        {
            bool canbedeleted = false;

            foreach (var book in _storage.GetBooks)
            {
                if (book.GetName() == bookName)
                {
                    if (book.GetAvailableNumber() < book.GetAllNumber())
                    {
                        canbedeleted = false;
                    }
                    else
                    {
                        canbedeleted = true;
                    }
                }
                
            }

            return canbedeleted;

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
