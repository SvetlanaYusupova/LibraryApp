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
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public EditBookWindow(string login)
        {
            InitializeComponent();
            admin = login;
        }

        string admin;

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new WindowAdmin(admin).Show();
            Close();
        }

        private void Deletebook_Click(object sender, RoutedEventArgs e)
        {
            // для удаления книги
            new DeletingBookWindow(new List<string> { "", ""}, admin).Show();
            Close();
        }

        private void Addbook_Click(object sender, RoutedEventArgs e)
        {
            // для добавления новой книги
            new AddBookWindow(admin).Show();
            Close();
        }
    }
}
