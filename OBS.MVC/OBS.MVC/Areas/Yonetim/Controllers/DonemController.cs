using OBS.MVC.Helpers;
using OBS.MVC.Models;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class DonemController : Controller
    {
        DonemRepository _repository = new DonemRepository();

        #region Dönem Listesi

        public ActionResult DonemListesi()
        {
            var donemler = _repository.GetirTumu();
            if (donemler.BasariliMi)
                return View(donemler.Veri);
            return View();
        }

        #endregion

        #region Dönem Detay

        public ActionResult DonemDetay(int id)
        {
            var donem = _repository.Getir(id);
            if (donem.BasariliMi)
                return View(donem.Veri);
            return RedirectToAction("DonemListesi");
        }

        #endregion

        #region Dönem Ekle

        public ActionResult DonemEkle()
        {
            ViewBag.Donemler = SelectListHelper.DonemTipler;
            return View();
        }

        [HttpPost]
        public ActionResult DonemEkle(Donem kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("DonemListesi");
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

        #region Dönem Düzenle

        public ActionResult DonemDuzenle(int id)
        {
            ViewBag.Donemler = SelectListHelper.DonemTipler;

            var donem = _repository.Getir(id);
            if (donem.BasariliMi)
                return View(donem.Veri);
            return RedirectToAction("DonemListesi");
        }

        [HttpPost]
        public ActionResult DonemDuzenle(Donem kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("DonemListesi");
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

        #region Dönem Sil

        public ActionResult DonemSil(int id)
        {
            var donem = _repository.Getir(id);
            if (donem.BasariliMi)
                return View(donem.Veri);
            return RedirectToAction("DonemListesi");
        }

        [HttpPost]
        public ActionResult DonemSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("DonemListesi");
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