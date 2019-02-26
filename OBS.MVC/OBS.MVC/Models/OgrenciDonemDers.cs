using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class OgrenciDonemDers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DonemDersId { get; set; }

        [Required]
        public int OgrenciId { get; set; }

        public int Vize1 { get; set; }
        public int Vize2 { get; set; }
        public int Final { get; set; }
        public int Ortalama { get; set; }
        public int BasariDurumTip { get; set; }

        public virtual DonemDers DonemDers { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }

    }
}