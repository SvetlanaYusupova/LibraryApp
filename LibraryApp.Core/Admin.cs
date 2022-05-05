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

        public Admin(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string GetLogin()
        {
            return Login;
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}
