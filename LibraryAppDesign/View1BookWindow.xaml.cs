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
    /// Логика взаимодействия для View1BookWindow.xaml
    /// </summary>
    public partial class View1BookWindow : Window
    {
        public View1BookWindow(string login, string tag)
        {
            chosenbook = tag;
            userlogin = login;
            InitializeComponent();
        }
        string chosenbook;
        string userlogin;


        private void Account(object sender, RoutedEventArgs e)
        {
            //для кнопки внесения изменений в данные пользователя
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для кнопки показа уведомлений пользователя
        }

        private void GoOut(object sender, RoutedEventArgs e)
        {
            new TakeBookWindow(userlogin, new List<string> { null, null, null, null}).Show();
            Close();
        }

        private void BookBook(object sender, RoutedEventArgs e)
        {
            //для бронирования данной книги
        }
        
    }
}
