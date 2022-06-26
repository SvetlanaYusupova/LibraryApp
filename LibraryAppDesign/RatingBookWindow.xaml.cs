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
    /// Логика взаимодействия для RatingBookWindow.xaml
    /// </summary>
    public partial class RatingBookWindow : Window
    {
        public RatingBookWindow(string tag, string login)
        {
            _storage = new Storage();

            bookname = tag;
            userlogin = login;

            InitializeComponent();
        }

        string bookname;
        string userlogin;

        List<double> rates = new List<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0 };
        Storage _storage;

        private void RateValue_Initialized(object sender, EventArgs e)
        {
            ComboBox RateValue = sender as ComboBox;
            foreach (var rate in rates)
            {
                RateValue.Items.Add(rate);
            }
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            string idx = RateValue.Text.ToString();
            foreach (var user in _storage.Users)
            {
                if (user.GetLogin() == userlogin)
                {
                    foreach (var pastbook in user.GetPastBook())
                    {
                        if (pastbook[4] == bookname)
                        {
                            pastbook.Add(idx);
                        }
                    }
                }
            }

            _storage.SaveUsers();
            MessageBox.Show("Книга успешно оценена!");
            new PastBookWindow(userlogin).Show();
            Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new PastBookWindow(userlogin).Show();
            Close();
        }
    }
}
