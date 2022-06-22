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
        public AdminNotificationsWindow(List<string> filter)
        {
            _storage = new Storage();
            filterUsers = filter;
            NotificationsListBox.ItemsSource = filterNotifications;

            InitializeComponent();
        }

        static Storage _storage = new Storage();
        List<Notification> notifications = _storage.Notifications;

        List<string> userslogins = new List<string> { };

        List<User> users;
        List<string> filterUsers;
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
            new AdminNotificationsWindow(new List<string> { "" }).Show();
            Close();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            List<string> fil = new List<string> { };
            AddFill(UserList.SelectedItem);
            void AddFill(object obj)
            {
                if (obj == null)
                {
                    fil.Add("");
                }
                else
                {
                    fil.Add(obj.ToString());
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

            NotificationType.Text = type.GetNotType();

        }

        private void ChooseNotification_Click(object sender, RoutedEventArgs e)
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

        private void ChooseUser()
        {
            foreach (var item in users)
            {
                if (item.GetLogin().ToLower().Contains(filterUsers[0].ToLower()) || filterUsers[0] == "")
                {
                    filterNotifications.AddRange((IEnumerable<Notification>)item.GetMessages()); // добавлено преобразование типов, надо проверить, что работает
                }
            }
        }
    }
}
