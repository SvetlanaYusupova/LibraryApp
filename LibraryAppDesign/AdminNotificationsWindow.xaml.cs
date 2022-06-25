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
    /// Логика взаимодействия для AdminNotificationsWindow.xaml
    /// </summary>
    public partial class AdminNotificationsWindow : Window
    {
        public AdminNotificationsWindow(string filter)
        {
            InitializeComponent();
            _storage = new Storage();
            user = filter;

            ChooseNotifications();
            NotificationsListBox.ItemsSource = filterNotifications;
            
        }

        static Storage _storage = new Storage();
        List<Notification> notifications = _storage.Notifications;

        List<string> userslogins = new List<string> { };

        List<User> users = _storage.Users;
        string user;
        List<Notification> filterNotifications = new List<Notification> { };

        private void UserList_Initialized(object sender, EventArgs e)
        {
            ComboBox UserList = sender as ComboBox;
            foreach (var item in users)
            {
                if (!userslogins.Contains(item.GetLogin()))
                {
                    userslogins.Add(item.GetLogin());
                }
            }

            foreach (var item in userslogins)
            {
                UserList.Items.Add(item);
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            // кнопка для сброса фильтра
            string filter = "";
            new AdminNotificationsWindow(filter).Show();
            Close();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            string fil = "";
            AddFill(UserList.SelectedItem);
            void AddFill(object obj)
            {
                if (obj == null)
                {
                    fil = "";
                }
                else
                {
                    fil = obj.ToString();
                }
            }
            new AdminNotificationsWindow(fil).Show();
            Close();
        }
        private void UserLogin_Initialized(object sender, EventArgs e)
        {
            TextBlock UserLogin = sender as TextBlock;

            Notification user = UserLogin.DataContext as Notification;

            UserLogin.Text = user.GetLogin();
        }

        private void UserBook_Initialized(object sender, EventArgs e)
        {
            TextBlock UserBook = sender as TextBlock;

            Notification book = UserBook.DataContext as Notification;

            UserBook.Text = book.GetBookName();
        }

        private void NotificationType_Initialized(object sender, EventArgs e)
        {
            TextBlock NotificationType = sender as TextBlock;

            Notification type = NotificationType.DataContext as Notification;

            NotificationType.Text = type.GetType();

        }


        private void ChooseNotification_Initialized(object sender, EventArgs e)
        {
            Button ChooseNotification = sender as Button;
            Notification notification = ChooseNotification.DataContext as Notification;

            string chosennotification = "";

            foreach (var not in notifications)
            {
                if (not == notification)
                {
                    chosennotification = notification.GetLogin() + ";" + notification.GetBookName() + ";" + notification.GetType();
                }
            }

            ChooseNotification.Tag = chosennotification;
        }

        private void ChooseNotification(object sender, RoutedEventArgs e)
        {
            Button ChooseNotification = sender as Button;
            new ViewNotificationWindow(ChooseNotification.Tag.ToString()).Show();
            Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin().Show();
            Close();
        }

        private void ChooseNotifications()
        {
            if (user == "")
            {
                foreach (var notes in notifications)
                {
                    filterNotifications.Add(notes);
                }
            }
            else
            {
                foreach (var notes in notifications)
                {
                    if (notes.GetLogin() == user)
                    {
                        filterNotifications.Add(notes);
                    }
                }
            }
        }

    }
}
