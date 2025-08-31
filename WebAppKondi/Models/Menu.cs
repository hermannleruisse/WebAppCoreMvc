using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCoreMVC.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public bool Visible { get; set; }
    }
}