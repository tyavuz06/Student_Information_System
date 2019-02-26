using OBS.MVC.ModelBinders;
using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using OBS.MVC.Models.ViewModels;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Controllers
{
    [Authorize(Roles = "OgretimGorevlisi")]
    public class OgretimGorevlisiIslemController : Controller
    {
        #region Değişkenler

        OgretimGorevlisiRepository _repository = new OgretimGorevlisiRepository();
        OgrenciDonemDersRepository _ogrenciDonemDersRepository = new OgrenciDonemDersRepository();
        UyelikRepository _uyelik = new UyelikRepository();

        #endregion

        #region Üyelik İşlemleri

        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult GirisYap(NUyeGiris model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var girisSonuc = _uyelik.GirisYap(model.KullaniciAd, model.Sifre, UyeTip.OgretimGorevlisi);
                if (girisSonuc.BasariliMi)
                {
                    Session["OgretimGorevlisi"] = girisSonuc.Veri;
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("DerslerSonDonem");
                }
                else
                {
                    ModelState.AddModelError("", girisSonuc.Mesaj);
                }
            }
            return View(model);
        }

        [Authorize(Roles="OgretimGorevlisi")]
        public ActionResult CikisYap()
        {
            Session["OgretimGorevlisi"] = null;
            _uyelik.CikisYap();
            return RedirectToAction("GirisYap");
        }

        #endregion

        #region Ders İşlemleri

        public ActionResult DerslerSonDonem()
        {
            var ogretimGorevlisi = (NSession)Session["OgretimGorevlisi"];

            ViewBag.Donemler = _repository.GetirDonem_SonDonem(ogretimGorevlisi.Id).Veri;
            var dersler = _repository.GetirDonemDersleri_SonDonem(ogretimGorevlisi.Id).Veri;
            return View("Dersler", dersler);
        }
        public ActionResult DerslerEskiDonemler()
        {
            var ogretimGorevlisi = (NSession)Session["OgretimGorevlisi"];

            ViewBag.Donemler = _repository.GetirDonem_TumDonemler(ogretimGorevlisi.Id).Veri;
            var dersler = _repository.GetirDonemDersleri_Tumu(ogretimGorevlisi.Id).Veri;
            return View("Dersler", dersler);
        }

        public ActionResult DersOgrenciListe(int id)
        {
            var ogretimGorevlisi = (NSession)Session["OgretimGorevlisi"];

            var ders = _ogrenciDonemDersRepository.GetirDers(id, ogretimGorevlisi.Id);
            if (ders.BasariliMi)
            {
                ViewBag.Ders = ders.Veri.Ad;
                var ogrenciler = _repository.GetirDonemDersi_OgrenciNotlari(id).Veri;
                return View("OgrenciNotlari", ogrenciler);
            }
            else
            {
                return DerslerSonDonem();
            }
        }

        [HttpPost]
        public JsonResult DersNotKaydet([ModelBinder(typeof(OgrenciDersNotuModelBinder))] NOgrenciDersNotuKayit not)
        {
            var sonuc = _ogrenciDonemDersRepository.GuncelleNot(not);
            if (sonuc.BasariliMi)
            {
                return Json("Basarili");
            }
            else
            {
                return Json(sonuc.Mesaj);
            }
        }

        #endregion
    }
}