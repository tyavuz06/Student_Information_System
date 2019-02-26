
namespace OBS.MVC.Models.HelperModels
{
    public class NIslemSonuc
    {
        public bool BasariliMi { get; set; }
        public string Mesaj { get; set; }
    }

    public class NIslemSonuc<T> : NIslemSonuc
    {
        public T Veri { get; set; }
    }
}