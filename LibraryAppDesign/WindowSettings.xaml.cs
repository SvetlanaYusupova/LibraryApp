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
        string userLogin;
        public WindowSettings(string login)
        {
            InitializeComponent();
            userLogin = login;
        }

        private void Return(object sender, RoutedEventArgs e)
        {

            new WindowUser(userLogin).Show();
            Close();
        }

        private void ChangeInfo(object sender, RoutedEventArgs e)
        {
            IStorage _storage = Factory.GetInstance().Storage;
            //User userChoose; // закомментирована на время
            bool doing = true;
            foreach (var us in _storage.GetUsers)
            {
                if (textBoxLogin.Text == us.GetLogin())
                {
                    doing = false;
                    MessageBox.Show("Такой логин есть. Повторите попытку.");
                }
                if (doing & us.GetLogin() == userLogin)
                {
                    if (textBoxAge.Text != "")
                    {
                        try
                        {
                            int newAge = int.Parse(textBoxAge.Text);
                            if (newAge > 0 & newAge < 120)
                            {
                                us.SetAge(newAge);
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
                if (doing & us.GetLogin() == userLogin)
                {
                    if (textBoxLogin.Text != "")
                    {
                        us.SetLogin(textBoxLogin.Text);
                    }
                    if (textBoxPassword.Text != "")
                    {
                        us.SetPassword(textBoxPassword.Text);
                    }
                    if (textBoxName.Text != "")
                    {
                        us.SetName(textBoxName.Text);
                    }
                    if (textBoxSurname.Text != "")
                    {
                        us.SetSurname(textBoxSurname.Text);
                    }
                    _storage.Save();
                    new WindowUser(us.GetLogin()).Show();
                    Close();
                }
            }
        }
    }
}
