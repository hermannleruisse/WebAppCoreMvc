using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAppCoreMVC.Models;

namespace WebAppCoreMVC.Helpers
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("WebAppDatabase");
            options.UseSqlServer(connectionString);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Docteur> Docteurs { get; set; }
        public DbSet<Temoignage> Temoignages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<WhyUs> WhyUs { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Statistique> Statistiques { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Galerie> Galeries { get; set; }
    }
}
