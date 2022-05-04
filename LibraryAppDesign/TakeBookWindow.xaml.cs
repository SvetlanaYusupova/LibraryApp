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
    /// Логика взаимодействия для TakeBookWindow.xaml
    /// </summary>
    public partial class TakeBookWindow : Window
    {
        public TakeBookWindow()
        {
            InitializeComponent();
        }

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<BookInLibrary> books = _storage.Books;
        List<Admin> admins = _storage.Admins;
        List<Notification> notifications = _storage.Notifications;

        List<string> genres = new List<string> { };
        List<string> ageRatings = new List<string> { };

        private void Account(object sender, RoutedEventArgs e)
        {
            //для кнопки внесения изменений в данные пользователя
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для кнопки показа уведомлений пользователя
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            //для кнопки применения фильтров
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowUser().Show();
            Close();
        }

        private void NameGenre_Initialized(object sender, EventArgs e)
        {

            ComboBox GenreName = sender as ComboBox;
            foreach (var item in books)
            {
                if (!genres.Contains(item.GetGenre()))
                {
                    genres.Add(item.GetGenre());
                }
            }

            foreach (var item in genres)
            {
                GenreName.Items.Add(item);
            }
        }


        private void AgeName_Initialized(object sender, EventArgs e)
        {

            ComboBox AgeName = sender as ComboBox;
            foreach (var item in books)
            {
                if (!ageRatings.Contains(item.GetAgeRating()))
                {
                    ageRatings.Add(item.GetAgeRating());
                }
            }

            foreach (var item in ageRatings)
            {
                AgeName.Items.Add(item);
            }
        }


        private void FillFilters()
        {
            foreach (var item in books)
            {
                if (!genres.Contains(item.GetGenre()))
                {
                    genres.Add(item.GetGenre());
                }

                if (!ageRatings.Contains(item.GetAgeRating()))
                {
                    genres.Add(item.GetAgeRating());
                }
            }
        }
    }
}
