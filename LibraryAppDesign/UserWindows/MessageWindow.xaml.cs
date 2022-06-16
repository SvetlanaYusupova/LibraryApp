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

namespace LibraryAppDesign.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string user)
        {
            userLogin = user;
            _storage = new Storage();

            InitializeComponent();
        }

        Storage _storage;
        List<User> usersh;
        string userLogin;

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new WindowUser(userLogin).Show();
            Close();
        }

        private void Message_Initialized(object sender, EventArgs e)
        {

        }
    }

}
