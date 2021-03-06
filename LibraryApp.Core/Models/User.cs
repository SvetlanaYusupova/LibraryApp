using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        [JsonProperty]
        private List<string> Messages { get; set; }

        public User(string name, string surname, int age, string login, string password, List<TakenBook> usersBook, List<OrderBook> usersorderBook,
            List<List<string>> pastBook, List<string> messages)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Login = login;
            Password = GetHash(password);
            UsersBook = usersBook;
            UsersOrderBook = usersorderBook;
            PastBooks = pastBook;
            Messages = messages;
        }

        [JsonConstructor]
        private User()
        {

        }

        public string GetLogin()
        {
            return Login;
        }
        public string GetPassword()
        {
            return Password;
        }
        
        public List<List<string>> GetPastBook()
        {
            return PastBooks;
        }
        public int GetAge()
        {
            return Age;
        }
        public List<TakenBook> GetTakenBooks()
        {
            return UsersBook;
        }
        /*public List<OrderBook> GetOrderedBooks() поменять на GetOrderBook()
        {
            return UsersOrderBook;
        }*/

        public void AddOrderBook(OrderBook orderedbook)
        {
            UsersOrderBook.Add(orderedbook);
        }
        public void SetLogin(string login)
        {
            Login = login;
        }
        public void SetPassword(string password)
        {
            Password = GetHash(password);
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetSurname(string surname)
        {
            Surname = surname;
        }
        public void SetAge(int age)
        {
            Age = age;
        }
        public List<TakenBook> GetUsersBook()
        {
            return UsersBook;
        }

        public void AddPastBook(List<string> pastBook)
        {
            PastBooks.Add(pastBook);
        }
        public List<OrderBook> GetOrderBook()
        {
            return UsersOrderBook;
        }
        public void AddTakenBook(TakenBook takenbook)
        {
            UsersBook.Add(takenbook);
        }
        public void DicreaseOrderBook(OrderBook orderbook)
        {
            UsersOrderBook.Remove(orderbook);
        }

        public void AddBookingMessage(OrderBook book)
        {
            Messages.Add("У вас истекает срок бронирования книги: " + book.GetBookName() + ". Осталось менее 3 дней.");
        }

        public void AddTakenMessage(TakenBook book)
        {
            Messages.Add("У вас истекает срок пользования книгой: " + book.GetBookName() + ". Осталось менее недели.");
        }

        public List<string> GetMessages()
        {
            return Messages;
        }

        public void DeleteMessages()
        {
            Messages = new List<string> { };
        }

        public int CountHistory()
        {
            return PastBooks.Count;
        }

        public string GetName()
        {
            return Name + " " + Surname;
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
