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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryApp.Core;

namespace LibraryAppDesign
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            _storage = Factory.GetInstance().Storage;
            InitializeComponent();

        }

        static IStorage _storage;
        List<User> users = _storage.GetUsers;
        List<BookInLibrary> books = _storage.GetBooks;
        List<Admin> admins = _storage.GetAdmins;
        List<Notification> notifications = _storage.GetNotifications;

        private void Admin(object sender, RoutedEventArgs e)
        {
            //для кнопки входа в админа и дальнейшие действия (в новом окне)
            new WindowPassword("authorization", null).Show();
            Close();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            //для кнопки входа в пользователя и дальнейшие действия (в новом окне)
            _storage = Factory.GetInstance().Storage;
            bool doing = true;
            List<User> users = _storage.GetUsers;
            foreach (var user in users)
            {
                if (user.GetLogin() == textBoxName.Text)
                {
                    if (user.GetPassword() == textBoxPassword.Text)
                    {
                        MessageBox.Show("Авторизация пройдена.");
                        
                        new WindowUser(user.GetLogin()).Show();
                        Close();
                        doing = false;
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный.");
                        doing = false;
                    }
                }
            }
            if (doing)
            {
                MessageBox.Show("Такого пользователя нет.");
            }
        }

        private void Autorisation(object sender, RoutedEventArgs e)
        {
            //для кнопки авторизации в пользователя
            new WindowRegistration().Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            /*_storage.SaveUsers();
            _storage.SaveBooks();
            _storage.SaveAdmin();
            _storage.SaveNotifications();*/
            Close();
        }
    }
}
