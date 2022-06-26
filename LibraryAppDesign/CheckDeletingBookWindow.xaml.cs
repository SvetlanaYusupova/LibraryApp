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
    /// Логика взаимодействия для CheckDeletingBookWindow.xaml
    /// </summary>
    public partial class CheckDeletingBookWindow : Window
    {
        public CheckDeletingBookWindow(string tag, string login)
        {
            _storage = new Storage();
            bookname = tag;
            admin = login;
            InitializeComponent();
        }

        string bookname;
        string admin;

        static Storage _storage;

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new ViewDeletingBookWindow(bookname, admin).Show();
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var book in _storage.Books)
            {
                if (book.GetName() == bookname)
                {
                    if (book.GetAllNumber() < book.GetAvailableNumber())
                    {
                        MessageBox.Show("Книга не может быть удалена, так как на данный момент она забронирована или находится на руках у пользователя!");
                        new ViewDeletingBookWindow(bookname, admin).Show();
                        Close();
                    }
                    else
                    {
                        _storage.Books.Remove(book);
                        _storage.SaveBooks();

                        MessageBox.Show("Книга удалена из библиотеки!");
                        new DeletingBookWindow(new List<string> { "", "" }, admin).Show();
                        Close();

                    }
                }
            }
        }
    }
}
