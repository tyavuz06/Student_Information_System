
using OBS.MVC.Models;
namespace OBS.MVC.Helpers
{
    public class EnumHelper
    {
        public static string GetirDonemAdi(int tip)
        {
            return GetirDonemAdi((DonemTipEnum)tip);
        }
        public static string GetirDonemAdi(DonemTipEnum tip)
        {
            switch (tip)
            {
                case DonemTipEnum.Bahar: return "Bahar";
                case DonemTipEnum.Guz: return "Güz";
                case DonemTipEnum.Yaz: return "Yaz";
                default: return "";
            }
        }
    }
}