using OBS.MVC.Helpers;
using OBS.MVC.Models;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class DersController : Controller
    {
        DersRepository _repository = new DersRepository();

        #region Ders Listesi

        public ActionResult DersListesi()
        {
            var dersler = _repository.GetirTumu();
            if (dersler.BasariliMi)
                return View(dersler.Veri);
            return View();
        }

        public ActionResult BolumunDersleri(int id)
        {
            var dersler = _repository.GetirBolumunDersleri(id);
            if (dersler.BasariliMi)
                return View("DersListesi", dersler.Veri);
            return View();
        }

        #endregion

        #region Ders Detay

        public ActionResult DersDetay(int id)
        {
            var ders = _repository.Getir(id);
            if (ders.BasariliMi)
                return View(ders.Veri);
            return RedirectToAction("DersListesi");
        }

        #endregion

        #region Ders Ekle

        public ActionResult DersEkle()
        {
            ViewBag.Fakulteler = SelectListHelper.GetirFakulteler();
            return View();
        }

        [HttpPost]
        public ActionResult DersEkle(Ders kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("DersListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return DersEkle();
                }
            }
            else
            {
                return DersEkle();
            }
        }

        #endregion

        #region Ders Düzenle

        public ActionResult DersDuzenle(int id)
        {

            var ders = _repository.Getir(id);
            if (ders.BasariliMi)
            {
                var fakulteler = SelectListHelper.GetirFakultelerCustom(ders.Veri.Bolum.FakulteId);
                ViewBag.Fakulteler = fakulteler;

                return View(ders.Veri);
            }

            return RedirectToAction("DersListesi");
        }

        [HttpPost]
        public ActionResult DersDuzenle(Ders kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("DersListesi");
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

        #region Ders Sil

        public ActionResult DersSil(int id)
        {
            var ders = _repository.Getir(id);
            if (ders.BasariliMi)
                return View(ders.Veri);
            return RedirectToAction("DersListesi");
        }

        [HttpPost]
        public ActionResult DersSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("DersListesi");
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