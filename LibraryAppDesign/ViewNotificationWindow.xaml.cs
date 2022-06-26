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
    /// Логика взаимодействия для ViewNotificationWindow.xaml
    /// </summary>
    public partial class ViewNotificationWindow : Window
    {
        public ViewNotificationWindow(string tag, string adminlog)
        {
            _storage = Factory.GetInstance().Storage;

            admin = adminlog;
            string[] information = tag.Split(';');

            login = information[0];
            bookname = information[1];
            typenot = information[2];

            notification = GetNotification(login, bookname, typenot);
            user = GetUser(login);

            InitializeComponent();
        }

        string admin;
        string login;
        string bookname;
        string typenot;

        Notification notification;
        User user;

        static IStorage _storage;

        private void UserLogin_Initialized(object sender, EventArgs e)
        {
            TextBlock UserLogin = sender as TextBlock;
            UserLogin.Text = notification.GetLogin();
        }
        private void BookName_Initialized(object sender, EventArgs e)
        {
            TextBlock BookName = sender as TextBlock;
            BookName.Text = notification.GetBookName();
        }
        private void NotificationType_Initialized(object sender, EventArgs e)
        {
            TextBlock NotificationType = sender as TextBlock;
            NotificationType.Text = notification.GetType();
        }

        private void NoProlong(object sender, RoutedEventArgs e)
        {
            _storage.GetNotifications.Remove(notification);
            _storage.Save();

            MessageBox.Show("Уведомление удалено!");
            string filter = "";
            new AdminNotificationsWindow(filter, admin).Show();
            Close();
        }

        private void Prolong(object sender, RoutedEventArgs e)
        {
            if (notification.GetType() == "Продление бронирования")
            {
                foreach (var user in _storage.GetUsers)
                {
                    if (user.GetLogin() == login)
                    {
                        foreach (var book in user.GetOrderBook())
                        {
                            if (book.GetBookName() == bookname)
                            {
                                book.Prolong();

                                _storage.Save();
                                _storage.GetNotifications.Remove(notification);
                                _storage.Save();

                                MessageBox.Show("Бронирование книги продлено на 7 дней!");
                                string filter = "";
                                new AdminNotificationsWindow(filter, admin).Show();
                                Close();

                            }
                        }
                    }
                }
                
            }

            if (notification.GetType() == "Продление пользования")
            {
                foreach (var book in user.GetTakenBooks())
                {
                    if (book.GetBookName() == bookname)
                    {
                        book.Prolong();

                        _storage.Save();
                        _storage.GetNotifications.Remove(notification);
                        _storage.Save();

                        MessageBox.Show("Книга продлена на 30 дней!");
                        string filter = "";
                        new AdminNotificationsWindow(filter, admin).Show();
                        Close();

                    }
                }
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            // надо что-то сохранить?
            string filter = "";
            new AdminNotificationsWindow(filter, admin).Show();
            Close();
        }
        private Notification GetNotification(string login, string book, string typenot)
        {
            foreach (var item in _storage.GetNotifications)
            {
                if (item.GetLogin() == login && item.GetBookName() == book && item.GetType() == typenot)
                {
                    return item;
                }
            }

            return default;
        }
        private User GetUser(string login)
        {
            foreach (var user in _storage.GetUsers)
            {
                if (user.GetLogin() == login)
                {
                    return user;
                }
            }

            return default;
        }
    }
}
