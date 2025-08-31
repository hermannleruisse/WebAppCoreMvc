using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public interface IUserService
    {
        User ObtenirElement(string username);
    }
}
