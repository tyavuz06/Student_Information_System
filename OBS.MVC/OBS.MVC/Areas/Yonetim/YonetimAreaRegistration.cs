using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim
{
    public class YonetimAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Yonetim";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Yonetim_default",
                "Yonetim/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            //Bilgisayar Mühendisliği: 1, 2014 Güz: 5, BM101 kodlu ders: 12
            //Bilgisayar Mühendisliği bölümünün 2014 Güz dönemindeki dersleri:
            //  Yonetim/Dersler/DonemDersListesi/1/5
            //Bilgisayar Mühendisliği bölümünün 2014 Güz döneminde açılan BM101 kodlu dersinin detay sayfası:
            //  Yonetim/Dersler/DonemDersDetay/1/5/12
            context.MapRoute(
                 "Yonetim_DonemDers",
                 "Yonetim/Dersler/{action}/{donemid}/{bolumid}/{dersid}",
                 new { action = "Index", controller = "DonemDers", dersid = UrlParameter.Optional }
             );
        }
    }
}