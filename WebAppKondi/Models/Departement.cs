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
    public class Departement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public string Description { get; set; }
        public string Url { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "Veuillez charger un fichier")]
        //[ValidateFile(ErrorMessage = "Veuillez charger un fichier .png, .jpg, .jpeg, .gif <= 5 MB")]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public Configuration config { get; set; }

        //public string getPhotoUrl() => $"~/UploadedFiles/{config.ObtenirNomReprtDepImage()}/" + Url;
    }
}