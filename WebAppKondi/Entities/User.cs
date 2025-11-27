using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppCoreMVC.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Le prénom est obligatoire")]
        //[Display(Name = "Rôle actif ?")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
        public string Username { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Le role est obligatoire")]
        public Role Role { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
    }
}
