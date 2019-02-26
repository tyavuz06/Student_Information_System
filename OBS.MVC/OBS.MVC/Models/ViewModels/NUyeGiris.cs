using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models.ViewModels
{
    public class NUyeGiris
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
    }
}