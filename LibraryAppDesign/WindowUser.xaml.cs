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
    /// Логика взаимодействия для WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        public WindowUser(string login)
        {
            InitializeComponent();
            userlogin = login;
        }
        string userlogin;
        private void Account(object sender, RoutedEventArgs e)
        {
            //для кнопки внесения изменений в данные пользователя
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для кнопки показа уведомлений пользователя
        }

        private void TakeNew(object sender, RoutedEventArgs e)
        {
            //для кнопки возможности поиска книг по жанрам и брони новой книги

            Hide();
            new TakeBookWindow(userlogin, new List<string> { "", "", "", "" }).Show();
            Close();
        }

        private void LookPresent(object sender, RoutedEventArgs e)
        {
            //для кнопки возможности просмотра текущих книг на руках пользователя
        }

        private void LookPast(object sender, RoutedEventArgs e)
        {
            //для кнопки возможности просмотра прошлых книг пользователя
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
