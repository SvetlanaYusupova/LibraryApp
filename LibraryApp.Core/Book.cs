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
        [JsonProperty]
        private int PublishedYear { get; set; }
        [JsonProperty]
        private double Rating { get; set; }
        [JsonProperty]
        private int NumPages { get; set; }

        public Book(string bookname, List<string> author, string age, string description, string genre, int publishedyear, double rating, int numpages)
        {
            BookName = bookname;
            Author = author;
            AgeRating = age;
            Description = description;
            Genre = genre;
            PublishedYear = publishedyear;
            Rating = rating;
            NumPages = numpages;
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

        public int GetPublishedYear()
        { return PublishedYear; }

        public double GetRating()
        { return Rating; }

        public int GetNumPages()
        { return NumPages; }

        public virtual DateTime GetEndDate()
        { return default; }

        public virtual void Prolong()
        {  }
    }

    public class BookInLibrary : Book
    {
        [JsonProperty]
        private int AllNumber { get; set; }
        [JsonProperty]
        private int AvailableNumber { get; set; }

        public BookInLibrary(string bookname, List<string> author, string age, string description, string genre, int publishedyear, double rating, int numpages, int allnumber, int availablenumber)
        : base(bookname, author, age, description, genre, publishedyear, rating, numpages)
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

        public TakenBook(string bookname, List<string> author, string age, string description, string genre, int publishedyear, double rating, int numpages, DateTime start, DateTime end)
        : base(bookname, author, age, description, genre, publishedyear, rating, numpages)
        {
            StartDate = start;
            EndDate = end;
        }

        public DateTime GetStartDate()
        {
            return StartDate;
        }

        public override DateTime GetEndDate()
        {
            return EndDate;
        }

        public override void Prolong()
        {
            EndDate = EndDate.AddDays(30);
        }
    }

    public class OrderBook : Book
    {
        [JsonProperty]
        private DateTime EndDate { get; set; }

        public OrderBook(string bookname, List<string> author, string age, string description, string genre, int publishedyear, double rating, int numpages, DateTime end)
        : base(bookname, author, age, description, genre, publishedyear, rating, numpages)
        {
            EndDate = end;
        }

        public override DateTime GetEndDate()
        { return EndDate; }
        public override void Prolong()
        {
            EndDate = EndDate.AddDays(7);
        }
    }

}

