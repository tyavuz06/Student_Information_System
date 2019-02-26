using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class DonemDers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dönem")]
        public int DonemId { get; set; }

        [Required(ErrorMessage = "Lütfen dersi seçiniz")]
        [Display(Name = "Ders")]
        public int DersId { get; set; }

        [Required(ErrorMessage = "Lütfen dersi verecek öğretim görevlisini seçiniz")]
        [Display(Name = "Öğretim Görevlisi")]
        public int OgretimGorevlisiId { get; set; }

        public virtual Donem Donem { get; set; }
        public virtual Ders Ders { get; set; }
        public virtual OgretimGorevlisi OgretimGorevlisi { get; set; }
        public virtual ICollection<OgrenciDonemDers> OgrenciDonemDersler { get; set; }
    }
}