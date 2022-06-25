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
                    MessageBox.Show("Фильм с таким названием уже существует. Повторите попытку.");
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
                                _storage.Books.Add(new BookInLibrary(textBoxName.Text, , ageBox.SelectedItem.ToString(), textBoxDescription.Text, textBoxGenre.Text, ));
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
                MessageBox.Show("Введите название книги, чтобы её добавить");
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
