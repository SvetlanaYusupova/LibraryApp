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
        private string Author { get; set; }
        [JsonProperty]
        private string AgeRating { get; set; }
        [JsonProperty]
        private string Description { get; set; }
        [JsonProperty]
        private string Genre { get; set; }

        public Book(string bookname, string author, string age, string description, string genre)
        {
            BookName = bookname;
            Author = author;
            AgeRating = age;
            Description = description;
            Genre = genre;
        }

    }

    public class BookInLibrary : Book
    {
        [JsonProperty]
        private int AllNumber { get; set; }
        [JsonProperty]
        private int AvailableNumber { get; set; }

        public BookInLibrary(int allnumber, int availblenumber, string bookname, string author, string age, string description, string genre)
        : base(bookname, author, age, description, genre)
        {
            AllNumber = allnumber;
            AvailableNumber = availblenumber;
        }
    }


    public class TakenBook : Book
    {
        [JsonProperty]
        private DateTime StartDate { get; set; }
        [JsonProperty]
        private DateTime EndDate { get; set; }

        public TakenBook(DateTime start, DateTime end, string bookname, string author, string age, string description, string genre)
        : base(bookname, author, age, description, genre)
        {
            StartDate = start;
            EndDate = end;
        }
    }

    public class OrderBook : Book
    {
        [JsonProperty]
        private DateTime EndDate { get; set; }

        public OrderBook(DateTime end, string bookname, string author, string age, string description, string genre)
        : base(bookname, author, age, description, genre)
        {
            EndDate = end;
        }
    }

}

