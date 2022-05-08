using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public class User
    {
        [JsonProperty]
        private string Name { get; set; }
        [JsonProperty]
        private string Surname { get; set; }
        [JsonProperty]
        private int Age { get; set; }
        [JsonProperty]
        private string Login { get; set; }
        [JsonProperty]
        private string Password { get; set; }
        [JsonProperty]
        private List<TakenBook> UsersBook { get; set; }
        [JsonProperty]
        private List<OrderBook> UsersOrderBook { get; set; }
        [JsonProperty]
        private List<List<string>> PastBooks { get; set; }
        [JsonIgnore]
        private List<string> Messages { get; set; }

        public User(string name, string surname, int age, string login, string password, List<TakenBook> usersBook, List<OrderBook> usersorderBook,
            List<List<string>> pastBook, List<string> messages)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Login = login;
            Password = password;
            UsersBook = usersBook;
            UsersOrderBook = usersorderBook;
            PastBooks = pastBook;
            // Messages = messages;
        }

        public string GetLogin()
        {
            return Login;
        }
        public string GetPassword()
        {
            return Password;
        }
        public List<TakenBook> GetTakenBook()
        {
            return UsersBook;
        }
    }
}
