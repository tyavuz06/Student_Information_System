using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using OBS.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OBS.MVC.Repositories
{
    public class OgrenciDonemDersRepository : BaseRepository
    {
        public NIslemSonuc<Ders> GetirDers(int id, int ogretimGorevlisiId = 0)
        {
            try
            {
                var dersler = (from d in _veritabani.DonemDersler
                               where d.Id == id && (ogretimGorevlisiId == 0 || d.OgretimGorevlisiId == ogretimGorevlisiId)
                               select d.Ders);
                if (dersler.Count() > 0)
                {
                    return new NIslemSonuc<Ders> { BasariliMi = true, Veri = dersler.FirstOrDefault() };
                }
                return new NIslemSonuc<Ders>() { Mesaj = "Ders Yok" };
            }
            catch { return new NIslemSonuc<Ders>(); }
        }
        public NIslemSonuc Ekle(OgrenciDonemDers ders)
        {
            try
            {
                _veritabani.OgrenciDonemDersler.Add(ders);
                _veritabani.SaveChanges();
                return new NIslemSonuc { BasariliMi = true };
            }
            catch
            {
                return new NIslemSonuc { BasariliMi = false };
            }
        }
        public NIslemSonuc Sil(int id)
        {
            try
            {
                var ders = _veritabani.OgrenciDonemDersler.Where(d => d.Id == id).FirstOrDefault();
                _veritabani.OgrenciDonemDersler.Remove(ders);
                _veritabani.SaveChanges();
                return new NIslemSonuc { BasariliMi = true };
            }
            catch
            {
                return new NIslemSonuc { BasariliMi = false };
            }
        }
        public NIslemSonuc GuncelleNot(NOgrenciDersNotuKayit kayit)
        {
            try
            {
                var ogrenciler = (from o in _veritabani.OgrenciDonemDersler
                                  where o.Id == kayit.Id
                                  select o);
                if (ogrenciler.Count() > 0)
                {
                    var ogrenci = ogrenciler.FirstOrDefault();
                    ogrenci.Vize1 = kayit.Vize1;
                    ogrenci.Vize2 = kayit.Vize2;
                    ogrenci.Final = kayit.Final;
                    ogrenci.Ortalama = kayit.Ortalama;
                    _veritabani.SaveChanges();
                    return new NIslemSonuc { BasariliMi = true };
                }
                return new NIslemSonuc { Mesaj = "Kayıt Yok" };
            }
            catch
            {
                return new NIslemSonuc();
            }
        }

        public NIslemSonuc<List<NDers>> GetirSonDonemDersleri(int ogrenciId, int bolumId)
        {
            try
            {
                var sonDonemler = (from d in _veritabani.Donemler
                                   orderby new { d.Yil, d.DonemTip } descending
                                   select d).ToList();
                if (sonDonemler.Count() > 0)
                {
                    int sonDonem = sonDonemler.FirstOrDefault().Id;

                    var dersler = (from dd in _veritabani.DonemDersler.ToList()
                                   join odd in _veritabani.OgrenciDonemDersler.Where(o => o.OgrenciId == ogrenciId).ToList() on dd.Id equals odd.DonemDersId into d
                                   from ders in d.DefaultIfEmpty()
                                   where dd.DonemId == sonDonem && dd.Ders.BolumId == bolumId && ders == null
                                   orderby dd.Ders.Ad
                                   select new NDers
                                   {
                                       Id = dd.Id,
                                       Ad = dd.Ders.Ad,
                                       OgretimGorevlisiAd = dd.OgretimGorevlisi.Ad + " " + dd.OgretimGorevlisi.Soyad
                                   }).ToList();


                    return new NIslemSonuc<List<NDers>>
                    {
                        BasariliMi = true,
                        Veri = dersler
                    };
                }
                return new NIslemSonuc<List<NDers>> { BasariliMi = false, Mesaj = "Dönem açılmadı" };
            }
            catch (Exception hata) { return new NIslemSonuc<List<NDers>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<List<NDers>> GetirOgrencininDersleri(int ogrenciId)
        {
            try
            {
                var sonDonemler = (from d in _veritabani.Donemler
                                   orderby new { d.Yil, d.DonemTip } descending
                                   select d).ToList();
                if (sonDonemler.Count() > 0)
                {
                    int sonDonem = sonDonemler.FirstOrDefault().Id;

                    var dersler = (from dd in _veritabani.OgrenciDonemDersler
                                   where dd.DonemDers.DonemId == sonDonem && dd.OgrenciId == ogrenciId
                                   orderby dd.DonemDers.Ders.Ad
                                   select new NDers
                                   {
                                       Id = dd.Id,
                                       Ad = dd.DonemDers.Ders.Ad,
                                       OgretimGorevlisiAd = dd.DonemDers.OgretimGorevlisi.Ad + " " + dd.DonemDers.OgretimGorevlisi.Soyad
                                   }).ToList();


                    return new NIslemSonuc<List<NDers>>
                    {
                        BasariliMi = true,
                        Veri = dersler
                    };
                }
                return new NIslemSonuc<List<NDers>> { BasariliMi = false, Mesaj = "Dönem açılmadı" };
            }
            catch (Exception hata) { return new NIslemSonuc<List<NDers>> { Mesaj = hata.Message }; }
        }

        public NIslemSonuc<List<NDersNotlu>> GetirOgrencininDersleri_Transkript(int ogrenciId)
        {
            try
            {
                var dersler = (from dd in _veritabani.OgrenciDonemDersler
                               where dd.OgrenciId == ogrenciId
                               orderby new { dd.DonemDers.Donem.Yil, dd.DonemDers.Donem.DonemTip } descending
                               select new NDersNotlu
                               {
                                   Id = dd.Id,
                                   Ad = dd.DonemDers.Ders.Ad,
                                   OgretimGorevlisiAd = dd.DonemDers.OgretimGorevlisi.Ad + " " + dd.DonemDers.OgretimGorevlisi.Soyad,
                                   BasariDurumTip = dd.BasariDurumTip,
                                   Final = dd.Final,
                                   Ortalama = dd.Ortalama,
                                   Vize1 = dd.Vize1,
                                   Vize2 = dd.Vize2,
                                   DonemId = dd.DonemDers.DonemId
                               }).ToList();


                return new NIslemSonuc<List<NDersNotlu>>
                {
                    BasariliMi = true,
                    Veri = dersler
                };

            }
            catch (Exception hata) { return new NIslemSonuc<List<NDersNotlu>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<List<NDersDonem>> GetirOgrencininDersleri_Donemler(int ogrenciId)
        {
            try
            {
                var dersler = (from dd in _veritabani.OgrenciDonemDersler
                               where dd.OgrenciId == ogrenciId
                               orderby new { dd.DonemDers.Donem.Yil, dd.DonemDers.Donem.DonemTip } descending
                               select new NDersDonem
                               {
                                   Id = dd.DonemDers.DonemId,
                                   DonemTip = dd.DonemDers.Donem.DonemTip,
                                   Yil = dd.DonemDers.Donem.Yil
                               }).Distinct().ToList();

                return new NIslemSonuc<List<NDersDonem>>
                {
                    BasariliMi = true,
                    Veri = dersler
                };

            }
            catch (Exception hata) { return new NIslemSonuc<List<NDersDonem>> { Mesaj = hata.Message }; }
        }
    }
}