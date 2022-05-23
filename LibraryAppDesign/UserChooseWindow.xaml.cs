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
            bool doing = true;
            foreach (var us in _storage.Users)
            {
                if (us.GetLogin() == UserList.Text)
                {
                    //us.UsersBook.Add(new TakenBook("testbook2", new List<string> { "testauthor"}, "19", "testdescription2", "test2", DateTime.Now, DateTime.Parse("30.05.2022")));
                    if (us.GetUsersBook().Count != 0)
                    {
                        new AcceptBookWindow(UserList.Text, new List<string> { "", "", "", "" }).Show();
                        Close();
                        doing = false;
                    }
                    else
                    {
                        MessageBox.Show("У пользователя нет взятых книг.");
                        doing = false;
                    }
                }
            }
            if (doing)
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
