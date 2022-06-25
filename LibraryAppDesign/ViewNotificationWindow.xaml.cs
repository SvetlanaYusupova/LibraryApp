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
    /// Логика взаимодействия для ViewNotificationWindow.xaml
    /// </summary>
    public partial class ViewNotificationWindow : Window
    {
        public ViewNotificationWindow(string tag)
        {
            string[] information = tag.Split(';');

            login = information[0];
            book = information[1];
            typenot = information[2];

            _storage = new Storage();

            notification = GetNotification(login, book, typenot);
            user = GetUser(login);

            InitializeComponent();
        }

        string login;
        string book;
        string typenot;

        Notification notification;
        User user;

        static Storage _storage = new Storage();

        private void UserLogin_Initialized(object sender, EventArgs e)
        {
            TextBlock UserLogin = sender as TextBlock;
            UserLogin.Text = notification.GetLogin();
        }
        private void BookName_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = notification.GetBookName();
        }
        private void NotificationType_Initialized(object sender, EventArgs e)
        {
            TextBlock NotificationType = sender as TextBlock;
            NotificationType.Text = notification.GetType();
        }

        private void NoProlong(object sender, RoutedEventArgs e)
        {
            _storage.Notifications.Remove(notification);
            _storage.SaveNotifications();

            MessageBox.Show("Уведомление удалено!");
            string filter = "";
            new AdminNotificationsWindow(filter).Show();
            Close();
        }

        private void Prolong(object sender, RoutedEventArgs e)
        {
            if (notification.GetType() == "Booked")
            {
                foreach (var book in user.GetOrderBook())
                {
                    if (book.GetBookName() == notification.GetBookName())
                    {
                        book.Prolong();

                        _storage.SaveUsers();
                        _storage.Notifications.Remove(notification);
                        _storage.SaveNotifications();

                        MessageBox.Show("Бронирование книги продлено на 30 дней!");
                        string filter = "";
                        new AdminNotificationsWindow(filter);
                        Close();

                    }
                }
            }

            if (notification.GetType() == "Taken")
            {
                foreach (var book in user.GetTakenBooks())
                {
                    if (book.GetBookName() == notification.GetBookName())
                    {
                        book.Prolong();

                        _storage.SaveUsers();
                        _storage.Notifications.Remove(notification);
                        _storage.SaveNotifications();

                        MessageBox.Show("Книга продлена на 30 дней!");
                        string filter = "";
                        new AdminNotificationsWindow(filter);
                        Close();

                    }
                }
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            // надо что-то сохранить?
            string filter = "";
            new AdminNotificationsWindow(filter).Show();
            Close();
        }
        private Notification GetNotification(string login, string book, string typenot)
        {
            foreach (var item in _storage.Notifications)
            {
                if (item.GetLogin() == login && item.GetBookName() == book && item.GetType() == typenot)
                {
                    return item;
                }
            }

            return default;
        }
        private User GetUser(string login)
        {
            foreach (var item in _storage.Users)
            {
                if (user.GetLogin() == login)
                {
                    return user;
                }
            }

            return default;
        }
    }
}
