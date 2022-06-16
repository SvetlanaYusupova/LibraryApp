using LibraryApp.Core;
using System.Collections.Generic;
using System.Windows;

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
    }
}
