using OBS.MVC.Helpers;
using OBS.MVC.Models;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class DonemDersController : Controller
    {
        DonemDersRepository _repository = new DonemDersRepository();

        #region Dönem Ders Listesi

        public ActionResult BolumListesi(int id)
        {
            DonemRepository donemRepository = new DonemRepository();
            var donemBilgi = donemRepository.Getir(id);
            if (donemBilgi.BasariliMi)
                ViewBag.Donem = donemBilgi.Veri.Yil + " " + EnumHelper.GetirDonemAdi(donemBilgi.Veri.DonemTip) + " Dönemi";

            BolumRepository bolumRepository = new BolumRepository();
            var bolumler = bolumRepository.GetirTumu();
            return View(bolumler.Veri);
        }

        public ActionResult DonemDersListesi(int bolumid, int donemid)
        {
            var dersler = _repository.GetirBolumunDonemdekiDersleri(bolumid, donemid);
            if (dersler.BasariliMi)
                return View(dersler.Veri);
            return View();
        }

        #endregion

        #region Dönem Ders Detay

        public ActionResult DonemDersDetay(int donemid, int bolumid, int dersid)
        {
            var ders = _repository.Getir(dersid);
            if (ders.BasariliMi)
                return View(ders.Veri);
            return Redirect("../../../DonemDersListesi/" + donemid + "/" + bolumid);
        }

        #endregion

        #region Dönem Ders Ekle

        public ActionResult DonemDersEkle(int donemid, int bolumid)
        {
            DonemRepository donemRepository = new DonemRepository();
            var donemBilgisi = donemRepository.Getir(donemid);
            if (donemBilgisi.BasariliMi)
                ViewBag.Title = donemBilgisi.Veri.Yil + " " + EnumHelper.GetirDonemAdi(donemBilgisi.Veri.DonemTip) + " Dönemine Ders Ekle";

            ViewBag.Dersler = SelectListHelper.GetirBolumunDersleri(bolumid);
            ViewBag.OgretimGorevlileri = SelectListHelper.GetirBolumunOgretimGorevlileri(bolumid);
            return View();
        }

        [HttpPost]
        public ActionResult DonemDersEkle(int donemid, int bolumid, DonemDers kayit)
        {
            if (ModelState.IsValid)
            {
                kayit.DonemId = donemid;
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return Redirect("../../DonemDersListesi/" + donemid + "/" + bolumid);
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return DonemDersEkle(donemid, bolumid);
                }
            }
            else
            {
                return DonemDersEkle(donemid, bolumid);
            }
        }

        #endregion

        #region Dönem Ders Düzenle

        public ActionResult DonemDersDuzenle(int donemid, int bolumid, int dersid)
        {
            var ders = _repository.Getir(dersid);
            if (ders.BasariliMi)
            {
                DonemRepository donemRepository = new DonemRepository();
                var donemBilgisi = donemRepository.Getir(donemid);
                if (donemBilgisi.BasariliMi)
                    ViewBag.Title = donemBilgisi.Veri.Yil + " " + EnumHelper.GetirDonemAdi(donemBilgisi.Veri.DonemTip) + " Döneminin Dersini Düzenle";

                ViewBag.Dersler = SelectListHelper.GetirBolumunDersleri(bolumid);
                ViewBag.OgretimGorevlileri = SelectListHelper.GetirBolumunOgretimGorevlileri(bolumid);

                return View(ders.Veri);
            }

            return RedirectToAction("DonemDersListesi");
        }

        [HttpPost]
        public ActionResult DonemDersDuzenle(int donemid, int bolumid, int dersid, DonemDers kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return Redirect("../../../DonemDersListesi/" + donemid + "/" + bolumid);
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return DonemDersDuzenle(donemid, bolumid, dersid);
                }
            }
            else
            {
                return DonemDersDuzenle(donemid, bolumid, dersid);
            }
        }

        #endregion

        #region Dönem Ders Sil

        public ActionResult DonemDersSil(int donemid, int bolumid, int dersid)
        {
            var ogrenci = _repository.Getir(dersid);
            if (ogrenci.BasariliMi)
                return View(ogrenci.Veri);
            return Redirect("../../../DonemDersListesi/" + donemid + "/" + bolumid);
        }

        [HttpPost]
        public ActionResult DonemDersSil(int donemid, int bolumid, int dersid, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(dersid);
            if (islemSonuc.BasariliMi)
            {
                return Redirect("../../../DonemDersListesi/" + donemid + "/" + bolumid);
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