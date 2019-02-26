using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class Donem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen dönemin yılını seçiniz")]
        [Display(Name = "Yıl")]
        public int Yil { get; set; }

        [Required(ErrorMessage = "Lütfen dönemin tipi seçiniz")]
        [Display(Name = "Dönem Tipi")]
        public int DonemTip { get; set; }

        public virtual ICollection<DonemDers> DonemDersler { get; set; }
    }
}