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
    /// Interaction logic for ExtendBookingWindow.xaml
    /// </summary>
    public partial class ExtendBookingWindow : Window
    {
        public ExtendBookingWindow(string userLogin, string bookName)
        {
            userlogin = userLogin;
            bookN = bookName;

            _storage = new Storage();

            InitializeComponent();
        }

        string userlogin;
        static Storage _storage;
        string bookN;

       // static Notification _notific = new Notification(userlogin, bookName, asking);

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowUser(userlogin).Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_storage.Notifications.Contains(new Notification(userlogin, bookN, "booking")))
            {
                MessageBox.Show("Заявка на продление данной книги уже была отправлена.");
                new WindowUser(userlogin).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Заявка на продление отправлена.");

                List<Notification> noooooo = _storage.Notifications;

                noooooo.Add(new Notification(userlogin, bookN, "booking"));
                //_storage.Notifications.Add(new Notification(userlogin, bookN, "booking"));
                _storage.SaveNotifications();


                new WindowUser(userlogin).Show();
                Close();
            }
        }
    }
}
