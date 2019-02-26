using OBS.MVC.Helpers;
using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OBS.MVC.Repositories
{
    public class DonemRepository : BaseRepository
    {
        public NIslemSonuc<List<Donem>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<Donem>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Donemler.ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Donem>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<Donem> Getir(int id)
        {
            try
            {
                var donemler = (from d in _veritabani.Donemler
                                where d.Id == id
                                select d);
                if (donemler.Count() > 0)
                {
                    return new NIslemSonuc<Donem>
                    {
                        BasariliMi = true,
                        Veri = donemler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Donem>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Donem> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(Donem kayit)
        {
            try
            {
                _veritabani.Donemler.Add(kayit);
                _veritabani.SaveChanges();
                return new NIslemSonuc<int>
                {
                    BasariliMi = true,
                    Veri = kayit.Id
                };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<int>()
                {
                    BasariliMi = false,
                    Mesaj = hata.Message
                };
            }
        }
        public NIslemSonuc Guncelle(Donem kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.Donemler.Where(d => d.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.DonemTip = kayit.DonemTip;
                    duzenlenecekKayit.Yil = kayit.Yil;
                    _veritabani.SaveChanges();
                    return new NIslemSonuc { BasariliMi = true };
                }
                else
                {
                    return new NIslemSonuc
                    {
                        BasariliMi = false,
                        Mesaj = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NIslemSonuc
                {
                    BasariliMi = false,
                    Mesaj = hata.Message
                };
            }
        }
        public NIslemSonuc Sil(int id)
        {
            try
            {
                var silinecekKayitlar = _veritabani.Donemler.Where(d => d.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.Donemler.Remove(silinecekKayit);
                    _veritabani.SaveChanges();
                    return new NIslemSonuc { BasariliMi = true };
                }
                else
                {
                    return new NIslemSonuc
                    {
                        BasariliMi = false,
                        Mesaj = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<int>()
                {
                    BasariliMi = false,
                    Mesaj = hata.Message
                };
            }
        }

        public NIslemSonuc<SelectList> GetirTumu_SelectList()
        {
            var donemler = (from d in _veritabani.Donemler
                            orderby d.Yil
                            select d).ToList();

            List<SelectListItem> donemListesi = new List<SelectListItem>();
            foreach (var donem in donemler)
            {
                donemListesi.Add(new SelectListItem
                {
                    Text = donem.Yil + " " + EnumHelper.GetirDonemAdi(donem.DonemTip) + " Dönemi",
                    Value = donem.Id.ToString()
                });
            }

            return new NIslemSonuc<SelectList>
            {
                BasariliMi = true,
                Veri = new SelectList(donemListesi, "Value", "Text")
            };
        }
    }
}