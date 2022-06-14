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
    /// Логика взаимодействия для WindowPassword.xaml
    /// </summary>
    public partial class WindowPassword : Window
    {
        public WindowPassword()
        {
            InitializeComponent();
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();
            bool doing = true;
            List<Admin> admins = storage.Admins;
            foreach (var adm in admins)
            {
                if (adm.GetLogin() == textBoxLogin.Text)
                {
                    if (adm.GetPassword() == textBoxPassword.Text)
                    {
                        MessageBox.Show("Авторизация пройдена.");
                        Hide();
                        new WindowAdmin().Show();
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
                MessageBox.Show("Такого администратора нет.");
            }
        }
    }
}
