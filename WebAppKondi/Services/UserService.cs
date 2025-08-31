using System.Linq;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User ObtenirElement(string username)
        {
            return _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
        }
    }
}
