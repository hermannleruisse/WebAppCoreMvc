using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class Statistique
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Icone { get; set; }
        [Required]
        [Range(1, 500, ErrorMessage ="Le nombre à saisir doit être comprise entre 1 et 100")]
        public int Nombre { get; set; }
        [Required]
        public string Libelle { get; set; }
    }
}