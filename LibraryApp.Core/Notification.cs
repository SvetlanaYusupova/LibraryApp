﻿using Newtonsoft.Json;
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
        private string Action { get; set; }

        public Notification(string login, string bookName, string action)
        {
            Login = login;
            BookName = bookName;
            Action = action;
        }
    }
}
