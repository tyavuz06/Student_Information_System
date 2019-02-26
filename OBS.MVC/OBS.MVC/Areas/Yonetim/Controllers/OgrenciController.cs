using OBS.MVC.Helpers;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class OgrenciController : Controller
    {
        OgrenciRepository _repository = new OgrenciRepository();

        #region Öğrenci Listesi

        public ActionResult OgrenciListesi()
        {
            var ogrenciler = _repository.GetirTumu();
            if (ogrenciler.BasariliMi)
                return View(ogrenciler.Veri);
            return View();
        }

        public ActionResult BolumunOgrencileri(int id)
        {
            var ogrenciler = _repository.GetirBolumunOgrencileri(id);
            if (ogrenciler.BasariliMi)
                return View("OgrenciListesi", ogrenciler.Veri);
            return View();
        }

        #endregion

        #region Öğrenci Detay

        public ActionResult OgrenciDetay(int id)
        {
            var ogrenci = _repository.Getir(id);
            if (ogrenci.BasariliMi)
                return View(ogrenci.Veri);
            return RedirectToAction("OgrenciListesi");
        }

        #endregion

        #region Öğrenci Ekle

        public ActionResult OgrenciEkle()
        {
            ViewBag.Fakulteler = SelectListHelper.GetirFakulteler();
            return View();
        }

        [HttpPost]
        public ActionResult OgrenciEkle(Models.Ogrenci kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("OgrenciListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return OgrenciEkle();
                }
            }
            else
            {
                return OgrenciEkle();
            }
        }

        #endregion

        #region Öğrenci Düzenle

        public ActionResult OgrenciDuzenle(int id)
        {

            var ogrenci = _repository.Getir(id);
            if (ogrenci.BasariliMi)
            {
                var fakulteler = SelectListHelper.GetirFakultelerCustom(ogrenci.Veri.Bolum.FakulteId);
                ViewBag.Fakulteler = fakulteler;

                return View(ogrenci.Veri);
            }

            return RedirectToAction("OgrenciListesi");
        }

        [HttpPost]
        public ActionResult OgrenciDuzenle(Models.Ogrenci kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("OgrenciListesi");
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

        #region Öğrenci Sil

        public ActionResult OgrenciSil(int id)
        {
            var ogrenci = _repository.Getir(id);
            if (ogrenci.BasariliMi)
                return View(ogrenci.Veri);
            return RedirectToAction("OgrenciListesi");
        }

        [HttpPost]
        public ActionResult OgrenciSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("OgrenciListesi");
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