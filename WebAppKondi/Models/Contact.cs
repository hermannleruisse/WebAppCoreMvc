using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Objet { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}