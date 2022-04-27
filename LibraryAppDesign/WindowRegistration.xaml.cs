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
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        public WindowRegistration()
        {
            InitializeComponent();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();
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
                try
                {
                    if (int.Parse(textBoxAge.Text) <= 0)
                    {
                        MessageBox.Show("Некорректный возраст.");
                        return;
                    }
                    User userNew = new User(textBoxName.Text, textBoxSurname.Text, int.Parse(textBoxAge.Text), textBoxLogin.Text, textBoxPassword.Text, new List<TakenBook>() { }, new List<OrderBook>() { }, new List<List<string>>() { }, new List<string>() { });
                    storage.Users.Add(userNew);
                    MessageBox.Show("Регистрация пройдена.");
                    storage.SaveUsers();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Некорректный возраст.");
                }
            }
        }
    }
}
