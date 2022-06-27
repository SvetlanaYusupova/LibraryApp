using LibraryApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public WindowPassword(string whatDo, string account)
        {
            InitializeComponent();
            action = whatDo;
            login = account;
            if (action == "edit")
            {
                foreach (var adm in _storage.GetAdmins)
                {
                    if (adm.GetLogin() == login)
                    {
                        admin = adm;
                    }
                }
                textBoxLogin.Text = admin.GetLogin();
                buttonCheck.Content = "Изменить";
            }
            if (action == "newadmin")
            {
                buttonCheck.Content = "Создать";
            }
        }

        static IStorage _storage = Factory.GetInstance().Storage;
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
            List<Admin> admins = _storage.GetAdmins;
            if (action == "authorization")
            {
                foreach (var adm in admins)
                {
                    if (adm.GetLogin() == textBoxLogin.Text)
                    {
                        if (adm.GetPassword() == GetHash(textBoxPassword.Password))
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
                if (textBoxLogin.Text != "" & textBoxPassword.Password != "")
                {
                    admin.SetLogin(textBoxLogin.Text);
                    login = textBoxLogin.Text;
                    admin.SetPassword(textBoxPassword.Password);
                    _storage.Save();
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
                    if (textBoxLogin.Text != "" & textBoxPassword.Password != "")
                    {
                        Admin newAdmin = new Admin(textBoxLogin.Text, textBoxPassword.Password);
                        
                        _storage.GetAdmins.Add(newAdmin);
                        _storage.Save();
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

        public static string GetHash(string password)
        {
            byte[] bytePass = new System.Text.UTF8Encoding().GetBytes(password);
            SHA256 sha = new SHA256Managed();
            byte[] bytesh = sha.ComputeHash(bytePass);
            string result = BitConverter.ToString(bytesh);
            return result;
        }
    }
}
