using OBS.MVC.Helpers;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class OgretimGorevlisiController : Controller
    {
        OgretimGorevlisiRepository _repository = new OgretimGorevlisiRepository();

        #region Öğretim Görevlisi Listesi

        public ActionResult OgretimGorevlisiListesi()
        {
            var ogretimGorevlileri = _repository.GetirTumu();
            if (ogretimGorevlileri.BasariliMi)
                return View(ogretimGorevlileri.Veri);
            return View();
        }

        public ActionResult BolumunOgretimGorevlileri(int id)
        {
            var ogretimGorevlileri = _repository.GetirBolumunOgretimGorevlileri(id);
            if (ogretimGorevlileri.BasariliMi)
                return View("OgretimGorevlisiListesi", ogretimGorevlileri.Veri);
            return View();
        }

        #endregion

        #region Öğretim Görevlisi Detay

        public ActionResult OgretimGorevlisiDetay(int id)
        {
            var ogretimGorevlisi = _repository.Getir(id);
            if (ogretimGorevlisi.BasariliMi)
                return View(ogretimGorevlisi.Veri);
            return RedirectToAction("OgretimGorevlisiListesi");
        }

        #endregion

        #region Öğretim Görevlisi Ekle

        public ActionResult OgretimGorevlisiEkle()
        {
            ViewBag.Fakulteler = SelectListHelper.GetirFakulteler();
            return View();
        }

        [HttpPost]
        public ActionResult OgretimGorevlisiEkle(Models.OgretimGorevlisi kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("OgretimGorevlisiListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return OgretimGorevlisiEkle();
                }
            }
            else
            {
                return OgretimGorevlisiEkle();
            }
        }

        #endregion

        #region Öğretim Görevlisi Düzenle

        public ActionResult OgretimGorevlisiDuzenle(int id)
        {
            var ogretimGorevlisi = _repository.Getir(id);
            if (ogretimGorevlisi.BasariliMi)
            {
                var fakulteler = SelectListHelper.GetirFakultelerCustom(ogretimGorevlisi.Veri.Bolum.FakulteId);
                ViewBag.Fakulteler = fakulteler;

                return View(ogretimGorevlisi.Veri);
            }

            return RedirectToAction("OgretimGorevlisiListesi");
        }

        [HttpPost]
        public ActionResult OgretimGorevlisiDuzenle(Models.OgretimGorevlisi kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("OgretimGorevlisiListesi");
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

        #region Öğretim Görevlisi Sil

        public ActionResult OgretimGorevlisiSil(int id)
        {
            var ogretimGorevlisi = _repository.Getir(id);
            if (ogretimGorevlisi.BasariliMi)
                return View(ogretimGorevlisi.Veri);
            return RedirectToAction("OgretimGorevlisiListesi");
        }

        [HttpPost]
        public ActionResult OgretimGorevlisiSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("OgretimGorevlisiListesi");
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