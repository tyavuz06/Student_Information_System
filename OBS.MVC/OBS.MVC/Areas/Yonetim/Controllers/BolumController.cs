using OBS.MVC.Helpers;
using OBS.MVC.Models;
using OBS.MVC.Models.ViewModels;
using OBS.MVC.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class BolumController : Controller
    {
        BolumRepository _repository = new BolumRepository();

        #region Bölüm Listesi

        public ActionResult BolumListesi()
        {
            var bolumler = _repository.GetirTumu();
            if (bolumler.BasariliMi)
                return View(bolumler.Veri);
            return View();
        }

        public ActionResult FakulteninBolumleri(int id)
        {
            var bolumler = _repository.GetirFakulteninBolumleri(id);
            if (bolumler.BasariliMi)
                return View("BolumListesi", bolumler.Veri);
            return View();
        }

        [HttpPost]
        public JsonResult FakulteninBolumleriJson(int id)
        {
            var bolumler = _repository.GetirFakulteninBolumleri(id);
            if (bolumler.BasariliMi)
                return Json(bolumler.Veri.Select(b => new NIdAd { Id = b.Id.ToString(), Ad = b.Ad.ToString() }).ToList());
            return null;
        }

        #endregion

        #region Bölüm Detay

        public ActionResult BolumDetay(int id)
        {
            var bolum = _repository.Getir(id);
            if (bolum.BasariliMi)
                return View(bolum.Veri);
            return RedirectToAction("BolumListesi");
        }

        #endregion

        #region Bölüm Ekle

        public ActionResult BolumEkle()
        {
            ViewBag.Fakulteler = SelectListHelper.GetirFakulteler();
            return View();
        }

        [HttpPost]
        public ActionResult BolumEkle(Bolum kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("BolumListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return View();
                }
            }
            else
            {
                ViewBag.Fakulteler = SelectListHelper.GetirFakulteler();
                return View(kayit);
            }
        }

        #endregion

        #region Bölüm Düzenle

        public ActionResult BolumDuzenle(int id)
        {
            var bolum = _repository.Getir(id);
            if (bolum.BasariliMi)
            {
                ViewBag.Fakulteler = SelectListHelper.GetirFakultelerCustom(bolum.Veri.FakulteId);
                return View(bolum.Veri);
            }
            return RedirectToAction("BolumListesi");
        }

        [HttpPost]
        public ActionResult BolumDuzenle(Bolum kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("BolumListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region Bölüm Sil

        public ActionResult BolumSil(int id)
        {
            var bolum = _repository.Getir(id);
            if (bolum.BasariliMi)
                return View(bolum.Veri);
            return RedirectToAction("BolumListesi");
        }

        [HttpPost]
        public ActionResult BolumSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("BolumListesi");
            }
            else
            {
                ModelState.AddModelError("", islemSonuc.Mesaj);
                return View();
            }
        }

        #endregion
    }
}