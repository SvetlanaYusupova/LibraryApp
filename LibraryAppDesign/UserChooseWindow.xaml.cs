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
    /// Логика взаимодействия для UserChooseWindow.xaml
    /// </summary>
    public partial class UserChooseWindow : Window
    {
        public UserChooseWindow(string action)
        {
            InitializeComponent();
            
            chosenaction = action;
        }

        string chosenaction;

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<string> userLogins = new List<string> { };
        List<OrderBook> orderbooks = _storage.OrderBooks;

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            //ComboBox UserList = sender as ComboBox;
            Button buttonNext = sender as Button;

            string login = UserList.Text.ToString();

            if (!userLogins.Contains(login))
            {
                MessageBox.Show("Такого пользователя нет.");
                new UserChooseWindow(chosenaction).Show();
                Close();
            }

            // для перехода в окно выдачи книги
            if (userLogins.Contains(login) && chosenaction == "Выдать книгу")
            {
                if (CheckUserOrderBooks())
                {
                    new GiveBookWindow(login, chosenaction, new List<string> { "", "", "", "" }).Show();
                    Close();
                }
                
            }

            // для перехода в окно получения книги
            if (userLogins.Contains(login) && chosenaction == "Принять книгу")
            {
                new AcceptBookWindow(login, new List<string> { "", "", "", "" }).Show();
                Close();
            }

            /*else
            {
                if (chosenaction == "Выдать книгу")
                {
                    new GiveBookWindow(login, chosenaction, new List<string> { "", "", "", "" }).Show();
                    Close();
                }

                //Для перехода к окну принятия книги
                *//*if (chosenaction == "Принять книгу")
                {

                }*//*
            }*/

            //Для перехода к окну принятия книги

            /*if (UserList.Text.ToString() != "")
            //Для перехода к окну принятия книги
            bool doing = true;
            foreach (var us in _storage.Users)
            {
                if (us.GetLogin() == UserList.Text)
                {
                    //us.UsersBook.Add(new TakenBook("testbook2", new List<string> { "testauthor"}, "19", "testdescription2", "test2", DateTime.Now, DateTime.Parse("30.05.2022")));
                    if (us.GetUsersBook().Count != 0)
                    {
                        new AcceptBookWindow(UserList.Text, new List<string> { "", "", "", "" }).Show();
                        Close();
                        doing = false;
                    }
                    else
                    {
                        MessageBox.Show("У пользователя нет взятых книг.");
                        doing = false;
                    }
                }
            }
            if (doing)
            {
                MessageBox.Show("Такого пользователя нет.");
            }*/
        }

        private void UserList_Initialized(object sender, EventArgs e)
        {

            ComboBox UserList = sender as ComboBox;
            foreach (var item in users)
            {
                if (!userLogins.Contains(item.GetLogin()))
                {
                    userLogins.Add(item.GetLogin());
                }
            }

            foreach (var item in userLogins)
            {
                UserList.Items.Add(item);
            }
        }

        private bool CheckUserOrderBooks()
        {
            bool booksinorder = true;
            if (orderbooks == null)
            {
                booksinorder = false;
                MessageBox.Show("На данный момент у пользователя нет забронированных книг. Для получения книг пользователь должен забронировать их онлайн!");
                new UserChooseWindow(chosenaction).Show();
                Close();

            }

            return booksinorder;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin().Show();
            Close();
        }
    }
}
