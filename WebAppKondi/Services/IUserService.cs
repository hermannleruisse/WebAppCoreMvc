using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppCoreMVC.Areas.ADMIN.Interfaces;
using WebAppCoreMVC.Entities;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public interface IUserService
    {
        Task<User> ObtenirElement(string username);
        Task<IEnumerable<User>> ObtenirListeElement();
        Task<User> ObtenirElement(int ?id);
        Task EnregistrerElement(User user);
        Task MiseAjourElement(User user);
        Task SupprimerElement(User user);
    }
}
