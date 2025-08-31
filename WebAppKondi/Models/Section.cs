using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCoreMVC.Models
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Menu Menu { get; set; }
        [Display(Name = "Titre de la section")]
        public string TitreSection { get; set; }

        [Display(Name = "Description de la section")]
        public string DescriptionSection { get; set; }

    }
}