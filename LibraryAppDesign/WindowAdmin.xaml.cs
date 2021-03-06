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
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin(string login)
        {
            InitializeComponent();
            admin = login;
        }

        string admin;

        private void AddAdmin(object sender, RoutedEventArgs e)
        {
            //для создания новога админа при необходимости (права у них будут равные)
            new WindowPassword("newadmin", admin).Show();
            Close();
        }

        private void Account(object sender, RoutedEventArgs e)
        {
            //для редактирования логина и пароля админа
            new WindowPassword("edit", admin).Show();
            Close();
        }

        private void Notification(object sender, RoutedEventArgs e)
        {
            //для просмотра просроченный для возврата книг, запроса на продление срока пользования и запроса на продление срока брони
            string filter = "";
            new AdminNotificationsWindow(filter, admin).Show();
            Close();
        }

        // выдача книги пользователю
        private void GiveBook(object sender, RoutedEventArgs e)
        {
            new UserChooseWindow(Givebook.Content.ToString(), admin).Show(); // не работало через .ContentStringFormat - выдавало null
            Close();
        }

        private void GetBackBook(object sender, RoutedEventArgs e)
        {
            //принятие книги от пользователя
            new UserChooseWindow(GetBackBok.Content.ToString(), admin).Show();
            Close();
        }

        private void EditBook(object sender, RoutedEventArgs e)
        {
            //внести изменения в какую-то книгу
            new EditBookWindow(admin).Show();
            Close();
        }

        private void Analytics(object sender, RoutedEventArgs e)
        {
            //вывести аналитику по книгам
            new AnaliticsWindow(admin, new List<string> { "", "" }).Show();
            Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
