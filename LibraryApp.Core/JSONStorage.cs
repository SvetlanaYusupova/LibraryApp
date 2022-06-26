using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public class JSONStorage : IStorage
    {
        readonly Repository<User> _users;
        readonly Repository<Admin> _admins;
        readonly Repository<BookInLibrary> _books;
        readonly Repository<Notification> _notifications;


        public JSONStorage()
        {
            _users = new Repository<User>("../../Data/Users.json");
            _admins = new Repository<Admin>("../../Data/Admin.json");
            _books = new Repository<BookInLibrary>("../../Data/Books.json");
            _notifications = new Repository<Notification>("../../Data/Notifications.json");
        }

        public List<User> GetUsers => _users.GetCollection;

        public List<Admin> GetAdmins => _admins.GetCollection;

        public List<BookInLibrary> GetBooks => _books.GetCollection;

        public List<Notification> GetNotifications => _notifications.GetCollection;

        public void Save()
        {
            _users.Save();
            _admins.Save();
            _books.Save();
            _notifications.Save();
        }
    }
}
