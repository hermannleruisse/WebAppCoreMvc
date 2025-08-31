using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class Temoignage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom complet")]
        public string NomComplet { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Photo")]
        public string Url { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public Configuration config { get; set; }

        //public string getPhotoUrl() => $"~/UploadedFiles/{config.ObtenirNomReprtTestiImage()}/" + Url;
    }
}