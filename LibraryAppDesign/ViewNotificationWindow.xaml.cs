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
            chosennotification = tag;

            _storage = new Storage();

            notification = GetNotification(tag);
            user = GetUser(notification);

            InitializeComponent();
        }

        string chosennotification;
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
            NotificationType.Text = notification.GetNotType();
        }

        private void NoProlong(object sender, RoutedEventArgs e)
        {
            foreach (var item in _storage.Notifications)
            {
                if (item.GetNotType() == chosennotification)
                {
                    _storage.Notifications.Remove(item);
                }
            }

            MessageBox.Show("Уведомление удалено!");
            _storage.SaveNotifications();
        }

        private void Prolong(object sender, RoutedEventArgs e)
        {
            if (notification.GetNotType() == "Продлить бронирование")
            {
                foreach (var book in user.GetOrderBook())
                {
                    if (book.GetBookName() == notification.GetBookName())
                    {
                        book.Prolong();
                        _storage.Notifications.Remove(notification);

                        _storage.SaveBooks();
                        _storage.SaveNotifications();

                        MessageBox.Show("Бронирование книги продлено на 30 дней!");
                        new AdminNotificationsWindow(new List<string> { "" });
                        Close();

                    }
                }
            }

            if (notification.GetNotType() == "Продлить пользование")
            {
                foreach (var book in user.GetTakenBooks())
                {
                    if (book.GetBookName() == notification.GetBookName())
                    {
                        book.Prolong();
                        _storage.Notifications.Remove(notification);

                        _storage.SaveBooks();
                        _storage.SaveNotifications();

                        MessageBox.Show("Книга продлена на 30 дней!");
                        new AdminNotificationsWindow(new List<string> { "" });
                        Close();

                    }
                }
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            // надо что-то сохранить?
            new AdminNotificationsWindow(new List<string> { "" }).Show();
            Close();
        }
        private Notification GetNotification(string chosennotification)
        {
            foreach (var item in _storage.Notifications)
            {
                if (item.GetNotType() == chosennotification)
                {
                    return item;
                }
            }

            return default;
        }
        private User GetUser(Notification notification)
        {
            foreach (var item in _storage.Users)
            {
                if (item.GetLogin() == notification.GetLogin())
                {
                    return item;
                }
            }

            return default;
        }

    }
}
