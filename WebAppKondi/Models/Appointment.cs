using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Departement Departement { get; set; }
        [Required]
        public Docteur Docteur { get; set; }
        public string Message { get; set; }
    }
}