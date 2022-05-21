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
    /// Логика взаимодействия для WindowSettings.xaml
    /// </summary>
    public partial class WindowSettings : Window
    {
        public WindowSettings(string login)
        {

            InitializeComponent();
            string userlogin = login;
            Storage storage = new Storage();
            foreach (var us in storage.Users)
            {
                if (us.GetLogin() == userlogin)
                {
                    user = us;
                }
            }
        }
        User user;

        private void Return(object sender, RoutedEventArgs e)
        {
            new WindowUser(user.GetLogin()).Show();
            Close();
        }

        private void ChangeInfo(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();
            //User userChoose; // закомментирована на время
            bool doing = true;
            foreach (var user in storage.Users)
            {
                if (textBoxLogin.Text == user.GetLogin())
                {
                    doing = false;
                    MessageBox.Show("Такой логин есть. Повторите попытку.");
                }
            }
            if (doing)
            {
                if (textBoxAge.Text != "")
                {
                    try
                    {
                        int newAge = int.Parse(textBoxAge.Text);
                        if (newAge > 0)
                        {
                            user.SetAge(newAge);
                        }
                        else
                        {
                            MessageBox.Show("Указан неверный возраст. Повторите попытку.");
                            doing = false;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Указан неверный возраст. Повторите попытку.");
                        doing = false;
                    }
                }
            }
            if (doing)
            {
                if (textBoxLogin.Text != "")
                {
                    user.SetLogin(textBoxLogin.Text);
                }
                if (textBoxPassword.Text != "")
                {
                    user.SetPassword(textBoxPassword.Text);
                }
                if (textBoxName.Text != "")
                {
                    user.SetName(textBoxName.Text);
                }
                if (textBoxSurname.Text != "")
                {
                    user.SetSurname(textBoxSurname.Text);
                }
            }
        }
    }
}
