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
    /// Interaction logic for ExtendingOrderedBookWindow.xaml
    /// </summary>
    public partial class ExtendingOrderedBookWindow : Window
    {
        public ExtendingOrderedBookWindow(string userLogin, string bookName, string whatDo)
        {
            userlogin = userLogin;
            bookN = bookName;

            _storage = new Storage();

            InitializeComponent();
            action = whatDo;

            //if (action == "requestTaken")
            //{
            //   // foreach (var adm in _storage.Admins)
            //   // {
            //   //     if (adm.GetLogin() == login)
            //   //     {
            //   //         admin = adm;
            //   //     }
            //   // }
            //   // textBoxLogin.Text = admin.GetLogin();
            //   // textBoxPassword.Text = admin.GetPassword();
            //   //// buttonCheck.Content = "Изменить";
            //}
            //if (action == "requestBooked")
            //{
            //   // buttonCheck.Content = "Создать";
            //}

        }
        string userlogin;
        static Storage _storage;
        string bookN;
        string action;

        // static Notification _notific = new Notification(userlogin, bookName, asking);

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new WindowUser(userlogin).Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_storage.Notifications.Contains(new Notification(userlogin, bookN, action)))
            {
                MessageBox.Show("Заявка на продление данной книги уже была отправлена.");
                new WindowUser(userlogin).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Заявка на продление отправлена.");

                List<Notification> noooooo = _storage.Notifications;

                noooooo.Add(new Notification(userlogin, bookN, action));
                //_storage.Notifications.Add(new Notification(userlogin, bookN, "taking"));
                _storage.SaveNotifications();


                new WindowUser(userlogin).Show();
                Close();
            }
        }
    }

}











/*
        public WindowPassword(string whatDo, string account)
        {
            InitializeComponent();
            action = whatDo;
            login = account;
            if (action == "edit")
            {
                foreach (var adm in _storage.Admins)
                {
                    if (adm.GetLogin() == login)
                    {
                        admin = adm;
                    }
                }
                textBoxLogin.Text = admin.GetLogin();
                textBoxPassword.Text = admin.GetPassword();
                buttonCheck.Content = "Изменить";
            }
            if (action == "newadmin")
            {
                buttonCheck.Content = "Создать";
            }
        }

        Storage _storage = new Storage();
        string action;
        Admin admin;
        string login;

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            if (action == "authorization")
            {
                new MainWindow().Show();
                Close();
            }
            if (action == "edit" | action == "newadmin")
            {
                new WindowAdmin(login).Show();
                Close();
            }
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            bool doing = true;
            List<Admin> admins = _storage.Admins;
            if (action == "authorization")
            {
                foreach (var adm in admins)
                {
                    if (adm.GetLogin() == textBoxLogin.Text)
                    {
                        if (adm.GetPassword() == textBoxPassword.Text)
                        {
                            MessageBox.Show("Авторизация пройдена.");
                            Hide();
                            new WindowAdmin(adm.GetLogin()).Show();
                            Close();
                            doing = false;
                        }
                        else
                        {
                            MessageBox.Show("Пароль неверный.");
                            doing = false;
                        }
                    }
                }
                if (doing)
                {
                    MessageBox.Show("Такого администратора нет.");
                }
            }
            if (action == "edit")
            {
                if (textBoxLogin.Text != "" & textBoxPassword.Text != "")
                {
                    admin.SetLogin(textBoxLogin.Text);
                    login = textBoxLogin.Text;
                    admin.SetPassword(textBoxPassword.Text);
                    _storage.SaveAdmin();
                }
                else
                {
                    MessageBox.Show("Логин или пароль не могут быть пустыми. Повторите попытку.");
                }
            }
            if (action == "newadmin")
            {
                foreach (var adm in admins)
                {
                    if (adm.GetLogin() == textBoxLogin.Text)
                    {
                        doing = false;
                    }
                }
                if (doing)
                {
                    if (textBoxLogin.Text != "" & textBoxPassword.Text != "")
                    {
                        Admin newAdmin = new Admin(textBoxLogin.Text, textBoxPassword.Text);
                        _storage.Admins.Add(newAdmin);
                        _storage.SaveAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль не могут быть пустыми. Повторите попытку.");
                    }
                }
                else
                {
                    MessageBox.Show("Такой логин уже есть! Повторите попытку.");
                }
            }
        }


}*/