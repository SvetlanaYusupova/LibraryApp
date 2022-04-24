using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public class Storage
    {
        private const string filePathUsers = "../../Data/Users.json";
        private const string filePathAdmin = "../../Data/Admin.json";
        private const string filePathBooks = "../../Data/Books.json";
        private const string filePathNotifications = "../../Data/Notifications.json";

        public List<User> Users { get; private set; }
        public List<BookInLibrary> Books { get; private set; }
        public List<Admin> Admins { get; private set; }
        public List<Notification> Notifications { get; private set; }

        public Storage()
        {
            Users = new List<User> { };
            Books = new List<BookInLibrary> { };
            Admins = new List<Admin> { };
            Notifications = new List<Notification> { };
            /*ReadUsers();
            ReadAdmin();
            ReadBooks();*/
        }


        public void SaveNotifications()
        {
            using (var sw = new StreamWriter(filePathNotifications))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Notifications);
                }
            }
        }

        private void ReadNotifications()
        {
            using (var sr = new StreamReader(filePathNotifications))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Notifications = serializer.Deserialize<List<Notification>>(jsonReader);
                }
            }
        }

        public void SaveUsers()
        {
            using (var sw = new StreamWriter(filePathUsers))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Users);
                }
            }
        }

        private void ReadUsers()
        {
            using (var sr = new StreamReader(filePathUsers))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Users = serializer.Deserialize<List<User>>(jsonReader);
                }
            }
        }



        public void SaveBooks()
        {
            using (var sw = new StreamWriter(filePathBooks))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Books);
                }
            }
        }

        private void ReadBooks()
        {
            using (var sr = new StreamReader(filePathBooks))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Books = serializer.Deserialize<List<BookInLibrary>>(jsonReader);
                }
            }
        }

        public void SaveAdmin()
        {
            using (var sw = new StreamWriter(filePathAdmin))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    serializer.Serialize(jsonWriter, Admins);
                }
            }
        }

        private void ReadAdmin()
        {
            using (var sr = new StreamReader(filePathAdmin))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    Admins = serializer.Deserialize<List<Admin>>(jsonReader);
                }
            }
        }
    }
}
