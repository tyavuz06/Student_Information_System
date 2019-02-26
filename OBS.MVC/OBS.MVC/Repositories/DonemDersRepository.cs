using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OBS.MVC.Repositories
{
    public class DonemDersRepository : BaseRepository
    {
        public NIslemSonuc<List<DonemDers>> GetirBolumunDonemdekiDersleri(int bolumId, int donemId)
        {
            try
            {
                var dersler = (from d in _veritabani.DonemDersler
                               where d.DonemId == donemId && d.Ders.BolumId == bolumId
                               orderby d.Ders.Ad
                               select d).ToList();

                return new NIslemSonuc<List<DonemDers>>
                {
                    BasariliMi = true,
                    Veri = dersler
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<DonemDers>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<DonemDers> Getir(int id)
        {
            try
            {
                var dersler = (from b in _veritabani.DonemDersler
                               where b.Id == id
                               select b);
                if (dersler.Count() > 0)
                {
                    return new NIslemSonuc<DonemDers>
                    {
                        BasariliMi = true,
                        Veri = dersler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<DonemDers>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<DonemDers> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(DonemDers kayit)
        {
            try
            {
                _veritabani.DonemDersler.Add(kayit);
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
        public NIslemSonuc Guncelle(DonemDers kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.DonemDersler.Where(d => d.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.DersId = kayit.DersId;
                    duzenlenecekKayit.OgretimGorevlisiId = kayit.OgretimGorevlisiId;
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
                var silinecekKayitlar = _veritabani.DonemDersler.Where(d => d.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.DonemDersler.Remove(silinecekKayit);
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
    }
}