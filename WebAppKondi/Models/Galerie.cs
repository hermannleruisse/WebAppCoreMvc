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
    public class Galerie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public Configuration config { get; set; }
        public string getPhotoUrl(string folderName) => $"~/UploadedFiles/{folderName}/" + Url;
        //public string getPhotoUrl() => $"~/UploadedFiles/{config.ObtenirNomReprtGalerieImage()}/" + Url;
    }
}