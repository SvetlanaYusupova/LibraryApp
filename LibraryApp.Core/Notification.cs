using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public class Notification
    {
        [JsonProperty]
        private string Login { get; set; }
        [JsonProperty]
        private string BookName { get; set; }
        [JsonProperty]
        private string Type { get; set; }



        public Notification(string login, string bookName, string type)
        {
            Login = login;
            BookName = bookName;
            Type = type;
        }
    }
}
