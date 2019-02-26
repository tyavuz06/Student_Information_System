using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OBS.MVC.Repositories
{
    public class DersRepository : BaseRepository
    {
        public DersRepository() : base() { }
        public NIslemSonuc<List<Ders>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<Ders>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Dersler.OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Ders>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<List<Ders>> GetirBolumunDersleri(int bolumId)
        {
            try
            {
                return new NIslemSonuc<List<Ders>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Dersler.Where(d => d.BolumId == bolumId).OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Ders>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<Ders> Getir(int id)
        {
            try
            {
                var dersler = (from d in _veritabani.Dersler
                               where d.Id == id
                               select d);
                if (dersler.Count() > 0)
                {
                    return new NIslemSonuc<Ders>
                    {
                        BasariliMi = true,
                        Veri = dersler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Ders>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Ders> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(Ders kayit)
        {
            try
            {
                _veritabani.Dersler.Add(kayit);
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
        public NIslemSonuc Guncelle(Ders kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.Dersler.Where(d => d.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Ad = kayit.Ad;
                    duzenlenecekKayit.BolumId = kayit.BolumId;
                    duzenlenecekKayit.Kod = kayit.Kod;

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
                var silinecekKayitlar = _veritabani.Dersler.Where(d => d.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.Dersler.Remove(silinecekKayit);
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

        public NIslemSonuc<SelectList> GetirBolumunDersleri_SelectList(int bolumId)
        {
            var dersler = (from d in _veritabani.Dersler
                           where d.BolumId == bolumId
                           orderby d.Ad
                           select new SelectListItem
                           {
                               Text = d.Ad,
                               Value = d.Id.ToString()
                           }).ToList();

            return new NIslemSonuc<SelectList>
            {
                BasariliMi = true,
                Veri = new SelectList(dersler, "Value", "Text")
            };
        }
    }
}