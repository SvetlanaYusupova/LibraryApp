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
    /// Логика взаимодействия для AnaliticsWindow.xaml
    /// </summary>
    public partial class AnaliticsWindow : Window
    {
        public AnaliticsWindow(string admin, List<string> filters)
        {
            _storage = Factory.GetInstance().Storage;

            adminLogin = admin;
            analiticFilters = filters;

            FilterData();
            
            InitializeComponent();
            AnaliticsListBox.ItemsSource = data;
            TypeName.Text = filters[0];
            TopName.Text = filters[1];
        }

        string adminLogin;
        List<string> analiticFilters;
        List<string> subjects = new List<string> { "Читатели", "Книги" };
        List<int> top = new List<int> { 1, 3, 5, 10 };
        static IStorage _storage;
        List<List<string>> data = new List<List<string>> { };


        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            new WindowAdmin(adminLogin).Show();
            Close();
        }

        private void TypeName_Initialized(object sender, EventArgs e)
        {
            ComboBox TypeName = sender as ComboBox;
            foreach (var item in subjects)
            {
                TypeName.Items.Add(item);
            }
            
        }

        private void TopName_Initialized(object sender, EventArgs e)
        {
            ComboBox TopName = sender as ComboBox;
            foreach (var item in top)
            {
                TopName.Items.Add(item);
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            List<string> fil = new List<string> { TypeName.Text.ToString(), TopName.Text.ToString() };
            new AnaliticsWindow(adminLogin, fil).Show();
            Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new AnaliticsWindow(adminLogin, new List<string> { "", "" }).Show();
            Close();
        }

        private void BookORName_Initialized(object sender, EventArgs e)
        {
            TextBlock BookORName = sender as TextBlock;
            List<string> book = BookORName.DataContext as List<string>;
            BookORName.Text = book[0];
        }

        private void Number_Initialized(object sender, EventArgs e)
        {
            TextBlock Number = sender as TextBlock;
            List<string> book = Number.DataContext as List<string>;
            Number.Text = book[1];
        }

        void FilterData()
        {
            if (analiticFilters[0] == "Читатели")
            {
                List<User> users = _storage.GetUsers;
                for (int j = 0; j < users.Count; j++)
                {
                    for (int i = 0; i < users.Count -1; i++)
                    {
                        if (users[i + 1].CountHistory() > users[i].CountHistory())
                        {
                            var copy = users[i];
                            users[i] = users[i + 1];
                            users[i + 1] = copy;
                        }
                    }
                }

                for (int i = 0; i < users.Count; i++)
                {
                    data.Add(new List<string> { i + 1 + " - " + users[i].GetName() + " (" + users[i].GetLogin() + ")", "Количество книг в истории взятия - " + users[i].CountHistory() });

                }
                GetData();
            }
            else if (analiticFilters[0] == "Книги")
            {
                List<User> users = _storage.GetUsers;
                List<BookInLibrary> books = _storage.GetBooks;
                List<List<string>> preData = new List<List<string>> { };

                foreach (var book in books)
                {
                    var number = 0;
                    for (int i = 0; i < users.Count; i++)
                    {
                        List<List<string>> history = users[i].GetPastBook();

                        foreach (var his in history)
                        {
                            if (his[0] == book.GetName())
                            {
                                number += 1;
                            }
                        }
                    }
                    preData.Add(new List<string> { book.GetName(), number.ToString() });
                }

                for (int j = 0; j < preData.Count; j++)
                {
                    for (int i = 0; i < preData.Count - 1; i++)
                    {
                        if (int.Parse(preData[i + 1][1]) > int.Parse(preData[i][1]))
                        {
                            var copy = preData[i];
                            preData[i] = preData[i + 1];
                            preData[i + 1] = copy;
                        }
                    }
                }

                for (int i = 0; i < preData.Count; i++)
                {
                    data.Add(new List<string> { i + 1 + " - " + preData[i][0] , "Количество раз в истории взятия - " + preData[i][1] });

                }
                GetData();
            }
        }

        void GetData()
        {
            int indx = data.Count;
            var dataSP = new List<List<string>> { };
            int topStat = 0;
            if (!(analiticFilters[1] == ""))
            {
                topStat = int.Parse(analiticFilters[1]);
            }
            
            if (analiticFilters[1] == "")
            {
                for (int i = 0; i < indx; i++)
                {
                    dataSP.Add(data[i]);
                }
            }
            else if (indx >= topStat)
            {
                for (int i = 0; i < topStat; i++)
                {
                    dataSP.Add(data[i]);
                }
            }
            else
            {
                for (int i = 0; i < indx; i++)
                {
                    dataSP.Add(data[i]);
                }
                for (int i = 0; i < topStat - indx; i++)
                {
                    string name = indx + i + 1 + " -  ------------";
                    dataSP.Add(new List<string> { name, "" });
                }
            }
            data = dataSP;
        }
    }
}
