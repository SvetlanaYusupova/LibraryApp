using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core
{
    public interface IStorage
    {
        void Save();

        List<User> GetUsers { get; }

        List<Admin> GetAdmins { get; }

        List<BookInLibrary> GetBooks { get; }

        List<Notification> GetNotifications { get; }
    }
}
