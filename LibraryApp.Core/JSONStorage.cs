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
            _books = new Repository<BookInLibrary>("../../Data/boooks.json");
            _notifications = new Repository<Notification>("../../Data/Notifications.json");
            foreach (var user in GetUsers)
            {
                user.DeleteMessages();
                foreach (var booking in user.GetOrderBook())
                {
                    if ((DateTime.Today - booking.GetEndDate()).Duration().Days < 3)
                    {
                        user.AddBookingMessage(booking);
                    }
                }

                foreach (var taken in user.GetTakenBooks())
                {
                    if ((DateTime.Today - taken.GetEndDate()).Duration().Days < 7)
                    {
                        user.AddTakenMessage(taken);
                    }
                }
            }
            Save();
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
