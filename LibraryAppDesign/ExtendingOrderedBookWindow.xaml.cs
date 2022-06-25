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
    /// Interaction logic for ExtendingOrderedBookWindow.xaml
    /// </summary>
    public partial class ExtendingOrderedBookWindow : Window
    {
        public ExtendingOrderedBookWindow(string userLogin, string bookName, string whatDo)
        {
            userlogin = userLogin;
            bookN = bookName;

            _storage = new Storage();

            InitializeComponent();
            action = whatDo;

            //if (action == "requestTaken")
            //{
            //   // foreach (var adm in _storage.Admins)
            //   // {
            //   //     if (adm.GetLogin() == login)
            //   //     {
            //   //         admin = adm;
            //   //     }
            //   // }
            //   // textBoxLogin.Text = admin.GetLogin();
            //   // textBoxPassword.Text = admin.GetPassword();
            //   //// buttonCheck.Content = "Изменить";
            //}
            //if (action == "requestBooked")
            //{
            //   // buttonCheck.Content = "Создать";
            //}

        }
        string userlogin;
        static Storage _storage;
        string bookN;
        string action;

        // static Notification _notific = new Notification(userlogin, bookName, asking);

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowUser(userlogin).Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            bool a = false;


            foreach (var item in _storage.Notifications)
            {
                if (item.GetLogin() == userlogin & item.GetBookName() == bookN & item.GetType() == action)
                {
                    a = true;
                    break;
                }

            }
            if (a)
            {
                MessageBox.Show("Заявка на продление данной книги уже была отправлена.");
                new WindowUser(userlogin).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Заявка на продление отправлена.");

                List<Notification> noooooo = _storage.Notifications;

                noooooo.Add(new Notification(userlogin, bookN, action));
                //_storage.Notifications.Add(new Notification(userlogin, bookN, "taking"));
                _storage.SaveNotifications();


                new WindowUser(userlogin).Show();
                Close();
            }
        }
    }

}
