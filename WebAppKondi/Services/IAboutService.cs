using System.Collections.Generic;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public interface IAboutService
    {
        List<About> ObtenirListeElement();
        About ObtenirElement(int? id);
        void EnregistrerElement(About about);
        void MiseAjoutElement(About about);
        void SupprimerElement(About about);
    }
}
