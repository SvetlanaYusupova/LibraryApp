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
        private List<List<string>> Messages { get; set; }

        public Admin(string name, string surname, int age, string login, string password, List<List<string>> messages)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Login = login;
            Password = password;
            Messages = messages;
        }


    }
}
