using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreMVC.Areas.ADMIN.Interfaces;
using WebAppCoreMVC.Areas.ADMIN.Services;
using WebAppCoreMVC.Entities;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public class UserService : IUserService
    {
        //private readonly ApplicationDbContext _context;
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task EnregistrerElement(User user)
        {
            await _repository.EnregistrerElement(user);
            await _repository.SaveAsync();
        }

        public async Task MiseAjourElement(User user)
        {
            _repository.MiseAjourElement(user);
            await _repository.SaveAsync();
        }

        public async Task<User> ObtenirElement(string username)
        {
            return await _repository.FindElementByAsync(x => x.Username.Equals(username));
        }

        public async Task<User> ObtenirElement(int? id)
        {
            return await _repository.ObtenirElement(id.Value);
        }

        public async Task<IEnumerable<User>> ObtenirListeElement()
        {
            return await _repository.ObtenirListeElement();
        }

        public async Task SupprimerElement(User user)
        {
            _repository.SupprimerElement(user);
            await _repository.SaveAsync();
        }
    }
}
