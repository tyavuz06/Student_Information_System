using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OBS.MVC.Models
{
    public class OgretimGorevlisi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen öğretim görevlisinin adını yazınız")]
        [Display(Name = "Adı")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen öğretim görevlisinin soyadını yazınız")]
        [Display(Name = "Soyadı")]
        public string Soyad { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "T.C. kimlik numarası 11 karakterden oluşmalıdır.")]
        [MaxLength(11, ErrorMessage = "T.C. kimlik numarası 11 karakterden oluşmalıdır.")]
        [Display(Name = "T.C. Kimlik No")]
        public string KimlikNo { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Posta Adresi")]
        public string EPosta { get; set; }

        [Required]
        [Display(Name = "Doğum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DogumTarih { get; set; }

        [Required]
        [Display(Name = "İşe Giriş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime GirisTarih { get; set; }

        [Display(Name = "İşten Çıkış Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CikisTarih { get; set; }

        [Required]
        public int BolumId { get; set; }

        public virtual Bolum Bolum { get; set; }
        public virtual ICollection<DonemDers> DonemDersler { get; set; }
    }
}