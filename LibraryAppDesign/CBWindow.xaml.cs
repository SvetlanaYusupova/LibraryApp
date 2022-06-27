using LibraryApp.Core;
using System;
using System.Collections.Generic;
//using System.Diagnostics;
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
using System.IO;


namespace LibraryAppDesign
{
    /// <summary>
    /// Логика взаимодействия для CBWindow.xaml
    /// </summary>
    public partial class CBWindow : Window
    {
        public CBWindow(string login, List<string> filters)
        {
            
            _storage = Factory.GetInstance().Storage;
            userLogin = login;
            filtersCB = filters;
            GetBooks();
            InitializeComponent();
            TitleName.Text = filters[0];
            NumberBox.Text = filters[1];

            if (!booksName.Contains(filters[0]) || filters[0] =="")
            {
                BooksListBox.ItemsSource = rec;
            }
            else if (filters[1] == "" || !numbers.Contains(int.Parse(filters[1])) )
            {
                MessageBox.Show("Выберите количество книг в рекомендации");
                BooksListBox.ItemsSource = rec;
            }
            else
            {

                var args_r = new string[2] { filters[0], filters[1] };
                string result = null;
                result = RScriptRunner.RunFromCmd(@"../../Data" + @"\CBsourse.R",  @"C:\Program Files\R\R-4.2.1\bin\Rscript.exe", args_r);
                rec.Add(result);
                BooksListBox.ItemsSource = rec;

            }

            
        }

        string userLogin;
        IStorage _storage;
        List<string> filtersCB;
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<string> rec = new List<string> { };
        List<string> booksName = new List<string> { };


        public void GetBooks()
        {
            foreach (var item in _storage.GetBooks)
            {
                booksName.Add(item.GetBookName());
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new WindowUser(userLogin).Show();
            Close();
        }

        private void TitleName_Initialized(object sender, EventArgs e)
        {
            ComboBox BookName = sender as ComboBox;
            foreach (var item in _storage.GetBooks)
            {
                BookName.Items.Add(item.GetBookName());

            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            List<string> fil = new List<string> { TitleName.Text.ToString(), NumberBox.Text.ToString() };

            new CBWindow(userLogin, fil).Show();
            Close();
        }

        private void BookName_Initialized(object sender, EventArgs e)
        {
            

            TextBlock TitleName = sender as TextBlock;

            string book = TitleName.DataContext as string;

            TitleName.Text = book;
        }

        private void NumberBox_Initialized(object sender, EventArgs e)
        {
            ComboBox NumberBox = sender as ComboBox;
            foreach (var item in numbers)
            {
                NumberBox.Items.Add(item);   
            }
        }
    }
}
