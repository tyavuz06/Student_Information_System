
namespace OBS.MVC.Models.ViewModels
{
    public class NOgrenciDersNotu
    {
        public int Id { get; set; }
        public string KimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public int Vize1 { get; set; }
        public int Vize2 { get; set; }
        public int Final { get; set; }
        public int Ortalama { get; set; }
    }
    public class NOgrenciDersNotuKayit
    {
        public int Id { get; set; }
        public int Vize1 { get; set; }
        public int Vize2 { get; set; }
        public int Final { get; set; }
        public int Ortalama { get; set; }
    }
}