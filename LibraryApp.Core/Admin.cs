using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public class Admin
    {
        [JsonProperty]
        private string Login { get; set; }
        [JsonProperty]
        private string Password { get; set; }
        [JsonProperty]
        private List<Notification> Messages { get; set; }

        public Admin(string login, string password, List<Notification> messages)
        {
            Login = login;
            Password = password;
            Messages = messages;
        }


    }
}
