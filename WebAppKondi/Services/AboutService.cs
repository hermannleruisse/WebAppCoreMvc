using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Services
{
    public class AboutService : IAboutService
    {
        private readonly ApplicationDbContext _context;

        public AboutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void EnregistrerElement(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
        }

        public void MiseAjoutElement(About about)
        {
            _context.Entry(about).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public About ObtenirElement(int? id)
        {
            return _context.Abouts.Find(id);
        }

        public List<About> ObtenirListeElement()
        {
            return _context.Abouts.ToList();
        }

        public void SupprimerElement(About about)
        {
            _context.Abouts.Remove(about);
            _context.SaveChanges();
        }
    }
}
