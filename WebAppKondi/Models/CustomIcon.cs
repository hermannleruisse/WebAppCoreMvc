using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class CustomIcon
    {
        [Required]
        public string Libelle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Icone { get; set; }
    }
}