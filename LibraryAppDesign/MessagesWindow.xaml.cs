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
    /// Логика взаимодействия для MessagesWindow.xaml
    /// </summary>
    public partial class MessagesWindow : Window
    {
        public MessagesWindow(string user)
        {
            userLogin = user;
            _storage = new Storage();
            users = _storage.Users;
            InitializeComponent();
            foreach (var userl in users)
            {
                if (userl.GetLogin() == user)
                {
                    MessagesListBox.ItemsSource = userl.GetMessages();
                }
            }
        }

        Storage _storage;
        List<User> users;
        string userLogin;

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new WindowUser(userLogin).Show();
            Close();
        }

        private void Message_Initialized(object sender, EventArgs e)
        {
            TextBlock Message = sender as TextBlock;

            //string mess = Message.DataContext as string;

            Message.Text = Message.DataContext.ToString();
        }
    }
}
