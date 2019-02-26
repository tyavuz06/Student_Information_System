using OBS.MVC.Models;
using OBS.MVC.Repositories;
using System.Web.Mvc;

namespace OBS.MVC.Areas.Yonetim.Controllers
{
    [Authorize(Roles = "BilgiIslem")]
    public class FakulteController : Controller
    {
        FakulteRepository _repository = new FakulteRepository();

        #region Fakülte Listesi

        public ActionResult FakulteListesi()
        {
            var fakulteler = _repository.GetirTumu();
            if (fakulteler.BasariliMi)
                return View(fakulteler.Veri);
            return View();
        }

        #endregion

        #region Fakülte Detay

        public ActionResult FakulteDetay(int id)
        {
            var fakulte = _repository.Getir(id);
            if (fakulte.BasariliMi)
                return View(fakulte.Veri);
            return RedirectToAction("FakulteListesi");
        }

        #endregion

        #region Fakülte Ekle

        public ActionResult FakulteEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FakulteEkle(Fakulte kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Kaydet(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("FakulteListesi");
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

        #region Fakülte Düzenle

        public ActionResult FakulteDuzenle(int id)
        {
            var fakulte = _repository.Getir(id);
            if (fakulte.BasariliMi)
                return View(fakulte.Veri);
            return RedirectToAction("FakulteListesi");
        }

        [HttpPost]
        public ActionResult FakulteDuzenle(Fakulte kayit)
        {
            if (ModelState.IsValid)
            {
                var islemSonuc = _repository.Guncelle(kayit);
                if (islemSonuc.BasariliMi)
                {
                    return RedirectToAction("FakulteListesi");
                }
                else
                {
                    ModelState.AddModelError("", islemSonuc.Mesaj);
                    return View(kayit);
                }
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region Fakülte Sil

        public ActionResult FakulteSil(int id)
        {
            var fakulte = _repository.Getir(id);
            if (fakulte.BasariliMi)
                return View(fakulte.Veri);
            return RedirectToAction("FakulteListesi");
        }

        [HttpPost]
        public ActionResult FakulteSil(int id, FormCollection bilgiler)
        {
            var islemSonuc = _repository.Sil(id);
            if (islemSonuc.BasariliMi)
            {
                return RedirectToAction("FakulteListesi");
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