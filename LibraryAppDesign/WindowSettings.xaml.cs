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
        public WindowSettings()
        {
            InitializeComponent();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            new WindowUser().Show();
            Close();
        }

        private void ChangeInfo(object sender, RoutedEventArgs e)
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
        }
    }
}
