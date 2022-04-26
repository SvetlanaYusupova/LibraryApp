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
        const string password = "qwerty";
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
            if (textBoxPassword.Text == password)
            {
                MessageBox.Show("Авторизация пройдена.");
                Hide();
                new WindowAdmin().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Пароль неверный.");
            }
        }
    }
}
