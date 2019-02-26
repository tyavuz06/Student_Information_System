using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class Bolum
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen bölümün adını yazınız")]
        [Display(Name = "Bölüm Adı")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen bölümün adresini yazınız")]
        [Display(Name = "Bölüm Adresi")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen bölümün kodunu yazınız")]
        [Display(Name = "Bölüm Kodu")]
        [MinLength(3, ErrorMessage = "Bölüm kodu 3 karakterden oluşmalıdır.")]
        [MaxLength(3, ErrorMessage = "Bölüm kodu 3 karakterden oluşmalıdır.")]
        public string Kod { get; set; }

        [Required(ErrorMessage = "Lütfen fakülte seçimini yapınız")]
        [Display(Name = "Fakülte")]
        public int FakulteId { get; set; }

        public virtual Fakulte Fakulte { get; set; }

        public virtual ICollection<Ogrenci> Ogrenciler { get; set; }
        public virtual ICollection<Ders> Dersler { get; set; }

    }
}