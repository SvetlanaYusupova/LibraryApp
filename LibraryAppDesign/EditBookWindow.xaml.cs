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
        public EditBookWindow()
        {
            InitializeComponent();
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            // для удаления книги
        }
        private void AddBook(object sender, RoutedEventArgs e)
        {
            // для добавления новой книги
        }
        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin().Show();
            Close();
        }

    }
}
