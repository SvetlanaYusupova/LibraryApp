using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            Password = GetHash(password);
        }

        [JsonConstructor]
        private Admin()
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

        public void SetLogin(string login)
        {
            Login = login;
        }

        public void SetPassword(string password)
        {
            Password = GetHash(password);
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
