using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class Fakulte
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen fakültenin adını yazınız")]
        [Display(Name = "Fakülte Adı")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen fakültenin adresini yazınız")]
        [Display(Name = "Fakülte Adresi")]
        public string Adres { get; set; }

        public virtual ICollection<Bolum> Bolumler { get; set; }
    }
}