using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public abstract class Book
    {
        [JsonProperty]
        private string BookName { get; set; }
        [JsonProperty]
        private List<string> Author { get; set; }
        [JsonProperty]
        private string AgeRating { get; set; }
        [JsonProperty]
        private string Description { get; set; }
        [JsonProperty]
        private string Genre { get; set; }

        public Book(string bookname, List<string> author, string age, string description, string genre)
        {
            BookName = bookname;
            Author = author;
            AgeRating = age;
            Description = description;
            Genre = genre;
        }
        public string GetGenre()
        {
            return Genre;
        }
        public List<string> GetAuthor()
        {
            return Author;
        }
        public string GetBookName()
        {
            return BookName;
        }
        public string GetAgeRating()
        { 
            return AgeRating; 
        }

        public string GetDescription()
        {
            return Description;
        }

        
        public int GetIntBookAge() // целочисленное значение возрастного рейтинга книги для сравнения с возрастом пользователя
        {
            var bookage = int.Parse(AgeRating.Substring(0, AgeRating.Length - 1));
            return bookage;
        }
        public string GetName()
        { return BookName; }
       

    }

    public class BookInLibrary : Book
    {
        [JsonProperty]
        private int AllNumber { get; set; }
        [JsonProperty]
        private int AvailableNumber { get; set; }

        public BookInLibrary(string bookname, List<string> author, string age, string description, string genre, int allnumber, int availablenumber)
        : base(bookname, author, age, description, genre)
        {
            AllNumber = allnumber;
            AvailableNumber = availablenumber;
        }

        public void AddOneBook()
        {
            AvailableNumber = AvailableNumber + 1;
        }

        public int GetAllNumber()
        { return AllNumber; }

        public int GetAvailableNumber()
        { return AvailableNumber; }

        public void Dicrease()
        {
            AvailableNumber--;
        }

    }


    public class TakenBook : Book
    {
        [JsonProperty]
        private DateTime StartDate { get; set; }
        [JsonProperty]
        private DateTime EndDate { get; set; }

        public TakenBook(string bookname, List<string> author, string age, string description, string genre, DateTime start, DateTime end)
        : base(bookname, author, age, description, genre)
        {
            StartDate = start;
            EndDate = end;
        }

        public DateTime GetStartDate()
        {
            return StartDate;
        }

        public DateTime GetEndDate()
        {
            return EndDate;
        }
    }

    public class OrderBook : Book
    {
        [JsonProperty]
        private DateTime EndDate { get; set; }

        public OrderBook(string bookname, List<string> author, string age, string description, string genre, DateTime end)
        : base(bookname, author, age, description, genre)
        {
            EndDate = end;
        }
    }

}

