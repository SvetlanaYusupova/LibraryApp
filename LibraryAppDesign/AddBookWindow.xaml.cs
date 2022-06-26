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
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow(string adm)
        {
            InitializeComponent();
            admin = adm;
            _storage = new Storage();
        }

        string admin;
        readonly Storage _storage;

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            bool doing = true;
            foreach (var book in _storage.Books)
            {
                if (textBoxName.Text == book.GetName())
                {
                    MessageBox.Show("Книга с таким названием уже существует. Повторите попытку.");
                    doing = false;
                }
            }
            if (textBoxName.Text != "" & doing)
            {
                if (textBoxGenre.Text != "")
                {
                    if (ageBox.SelectedItem != null)
                    {
                        if (textBoxDescription.Text != "")
                        {
                            if (textBoxAuthors.Text != "")
                            {
                                try
                                {
                                    if (int.Parse(textBoxNumberBook.Text) > 0 & double.Parse(textBoxRaiting.Text) >= 0 & double.Parse(textBoxRaiting.Text) <= 5 & int.Parse(textBoxYear.Text) > 0 & int.Parse(textBoxYear.Text) <= DateTime.Now.Year & int.Parse(textBoxPage.Text) > 0)
                                    {
                                        _storage.Books.Add(new BookInLibrary(textBoxName.Text, textBoxAuthors.Text.Split(new string[] { ", " }, StringSplitOptions.None).ToList(), ageBox.SelectedItem.ToString(), textBoxDescription.Text, textBoxGenre.Text, int.Parse(textBoxYear.Text), double.Parse(textBoxRaiting.Text), int.Parse(textBoxPage.Text), int.Parse(textBoxNumberBook.Text), int.Parse(textBoxNumberBook.Text)));
                                        _storage.SaveBooks();
                                        MessageBox.Show("Книга создана!");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неккоректный ввод чисел.");
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Неверный ввод кол-ва книг");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите ФИО авторов книги");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите описание книги");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите возрастное ограничение книги");
                    }
                }
                else
                {
                    MessageBox.Show("Введите название жанра книги");
                }
            }
            else
            {
                if (doing)
                {
                    MessageBox.Show("Введите название книги, чтобы её добавить");
                }
            }
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            new EditBookWindow(admin).Show();
            Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ageBox_Initialized(object sender, EventArgs e)
        {
            ComboBox ageBox = sender as ComboBox;
            ageBox.Items.Add("0+");
            ageBox.Items.Add("6+");
            ageBox.Items.Add("12+");
            ageBox.Items.Add("16+");
            ageBox.Items.Add("18+");
        }
    }
}
