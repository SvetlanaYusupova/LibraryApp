﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryApp.Core;

namespace LibraryAppDesign
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static Storage _storage = new Storage();
        List<User> users = _storage.Users;
        List<BookInLibrary> books = _storage.Books;
        List<Admin> admins = _storage.Admins;

        private void Admin(object sender, RoutedEventArgs e)
        {
            //для кнопки входа в админа и дальнейшие действия (в новом окне)

        }

        private void Login(object sender, RoutedEventArgs e)
        {
            //для кнопки входа в пользователя и дальнейшие действия (в новом окне)

        }

        private void Autorisation(object sender, RoutedEventArgs e)
        {
            //для кнопки авторизации в пользователя
            new WindowRegistration().Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
