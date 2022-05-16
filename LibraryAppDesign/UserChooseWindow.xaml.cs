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
    /// Логика взаимодействия для UserChooseWindow.xaml
    /// </summary>
    public partial class UserChooseWindow : Window
    {
        public UserChooseWindow()
        {
            InitializeComponent();
        }

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<string> userLogins = new List<string> { };

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            //Для перехода к окну принятия книги
            if (UserList.Text.ToString() != "")
            {
                new AcceptBookWindow(UserList.Text.ToString()).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Такого пользователя нет.");
            }
        }

        private void UserList_Initialized(object sender, EventArgs e)
        {

            ComboBox UserList = sender as ComboBox;
            foreach (var item in users)
            {
                if (!userLogins.Contains(item.GetLogin()))
                {
                    userLogins.Add(item.GetLogin());
                }
            }

            foreach (var item in userLogins)
            {
                UserList.Items.Add(item);
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin().Show();
            Close();
        }
    }
}
