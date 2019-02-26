using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class Ders
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen dersin adını yazınız")]
        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen dersin kodunu yazınız")]
        [Display(Name = "Ders Kodu")]
        [MinLength(3, ErrorMessage = "Ders kodu 3 karakterden oluşmalıdır.")]
        [MaxLength(3, ErrorMessage = "Ders kodu 3 karakterden oluşmalıdır.")]
        public string Kod { get; set; }

        [Required]
        public int BolumId { get; set; }

        public virtual Bolum Bolum { get; set; }

        public virtual ICollection<DonemDers> DonemDersler { get; set; }
    }
}