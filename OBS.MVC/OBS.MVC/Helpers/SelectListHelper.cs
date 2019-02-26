using OBS.MVC.Models;
using OBS.MVC.Models.ViewModels;
using OBS.MVC.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OBS.MVC.Helpers
{
    public class SelectListHelper
    {
        #region Değişkenler

        private static FakulteRepository _fakulteRepository = new FakulteRepository();
        private static BolumRepository _bolumRepository = new BolumRepository();
        private static DonemRepository _donemRepository = new DonemRepository();
        private static OgretimGorevlisiRepository _ogretimGorevlisiRepository = new OgretimGorevlisiRepository();
        private static DersRepository _dersRepository = new DersRepository();

        private static IEnumerable<SelectListItem> _donemTipler;
        public static IEnumerable<SelectListItem> DonemTipler
        {
            get
            {
                if (_donemTipler == null)
                    _donemTipler = new List<SelectListItem>()
                    {
                        new SelectListItem{ Value = ((int)DonemTipEnum.Bahar).ToString(), Text = "Bahar" },
                        new SelectListItem{ Value = ((int)DonemTipEnum.Guz).ToString(), Text = "Güz" },
                        new SelectListItem{ Value = ((int)DonemTipEnum.Yaz).ToString(), Text = "Yaz" }
                    };
                return _donemTipler;
            }
        }

        #endregion

        public static IEnumerable<SelectListItem> GetirFakulteler()
        {
            var fakulteler = _fakulteRepository.GetirTumu_SelectList();
            if (fakulteler.BasariliMi)
                return fakulteler.Veri;
            return new SelectList(null);
        }
        public static List<NSelectListItem> GetirFakultelerCustom(int seciliFakulteId = 0)
        {
            var fakulteler = _fakulteRepository.GetirTumu_NSelectList(seciliFakulteId);
            if (fakulteler.BasariliMi)
                return fakulteler.Veri;
            return null;
        }

        public static IEnumerable<SelectListItem> Bolumler
        {
            get
            {
                var bolumler = _bolumRepository.GetirTumu_SelectList();
                if (bolumler.BasariliMi)
                    return bolumler.Veri;
                return new SelectList(null);
            }
        }

        public static IEnumerable<SelectListItem> Donemler
        {
            get
            {
                var donemler = _donemRepository.GetirTumu_SelectList();
                if (donemler.BasariliMi)
                    return donemler.Veri;
                return new SelectList(null);
            }
        }

        public static IEnumerable<SelectListItem> GetirBolumunOgretimGorevlileri(int bolumId)
        {
            var ogretimGorevlileri = _ogretimGorevlisiRepository.GetirBolumunOgretimGorevlileri_SelectList(bolumId);
            if (ogretimGorevlileri.BasariliMi)
                return ogretimGorevlileri.Veri;
            return new SelectList(null);
        }
        public static IEnumerable<SelectListItem> GetirBolumunDersleri(int bolumId)
        {
            var dersler = _dersRepository.GetirBolumunDersleri_SelectList(bolumId);
            if (dersler.BasariliMi)
                return dersler.Veri;
            return new SelectList(null);
        }


    }
}