﻿using LibraryApp.Core;
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
        public UserChooseWindow(string action, string logAdmin)
        {
            _storage = new Storage();
            users = _storage.Users;
            InitializeComponent();
            admin = logAdmin;
            chosenaction = action;
        }

        string chosenaction;
        string admin;

        static Storage _storage;
        List<User> users;
        List<string> userLogins = new List<string> { };

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            //ComboBox UserList = sender as ComboBox;
            Button buttonNext = sender as Button;

            string login = UserList.Text.ToString();

            if (!userLogins.Contains(login))
            {
                MessageBox.Show("Такого пользователя нет.");
                new UserChooseWindow(chosenaction, admin).Show();
                Close();
            }

            // для перехода в окно выдачи книги
            if (userLogins.Contains(login) && chosenaction == "Выдать книгу")
            {
                if (CheckUserOrderBooks(login))
                {
                    new GiveBookWindow(login, chosenaction, new List<string> { "", "", "", "" }, admin).Show();
                    Close();
                }
                
            }

            // для перехода в окно получения книги
            if (userLogins.Contains(login) && chosenaction == "Принять книгу")
            {
                if (CheckUserTakenBooks(login))
                {
                    new AcceptBookWindow(login, new List<string> { "", "", "", "" }, admin).Show();
                    Close();
                }
                    
            }

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

        private bool CheckUserOrderBooks(string login)
        {
            bool booksinorder = true;
            foreach (var user in _storage.Users)
            {
                if (user.GetLogin() == login)
                {
                    if (user.GetOrderBook().Count() == 0)
                    {
                        booksinorder = false;
                        MessageBox.Show("На данный момент у пользователя нет забронированных книг. Для получения книг пользователь должен забронировать их онлайн!");
                        new UserChooseWindow(chosenaction, admin).Show();
                        Close();
                    }
                }
            }

            return booksinorder;
        }

        private bool CheckUserTakenBooks(string login)
        {
            bool bookstaken = true;
            foreach (var user in _storage.Users)
            {
                if (user.GetLogin() == login)
                {
                    if (user.GetTakenBooks().Count() == 0)
                    {
                        bookstaken = false;
                        MessageBox.Show("На данный момент у пользователя нет взятых книг!");
                        new UserChooseWindow(chosenaction, admin).Show();
                        Close();
                    }
                }
            }
            return bookstaken;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowAdmin(admin).Show();
            Close();
        }
    }
}
