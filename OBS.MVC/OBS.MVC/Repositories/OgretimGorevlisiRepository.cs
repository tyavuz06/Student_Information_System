using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using OBS.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OBS.MVC.Repositories
{
    public class OgretimGorevlisiRepository : BaseRepository
    {
        public OgretimGorevlisiRepository(bool uyelikAlinsinMi = true) : base(uyelikAlinsinMi) { }
        public NIslemSonuc<List<OgretimGorevlisi>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<OgretimGorevlisi>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.OgretimGorevlileri.OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<OgretimGorevlisi>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<List<OgretimGorevlisi>> GetirBolumunOgretimGorevlileri(int bolumId)
        {
            try
            {
                return new NIslemSonuc<List<OgretimGorevlisi>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.OgretimGorevlileri.Where(o => o.BolumId == bolumId).OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<OgretimGorevlisi>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<OgretimGorevlisi> Getir(int id)
        {
            try
            {
                var ogretimGorevlileri = (from o in _veritabani.OgretimGorevlileri
                                          where o.Id == id
                                          orderby o.Ad, o.Soyad
                                          select o);
                if (ogretimGorevlileri.Count() > 0)
                {
                    return new NIslemSonuc<OgretimGorevlisi>
                    {
                        BasariliMi = true,
                        Veri = ogretimGorevlileri.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<OgretimGorevlisi>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<OgretimGorevlisi> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<OgretimGorevlisi> Getir(string kimlikNo)
        {
            try
            {
                var ogretimGorevlileri = (from o in _veritabani.OgretimGorevlileri
                                          where o.KimlikNo == kimlikNo
                                          orderby o.Ad, o.Soyad
                                          select o);
                if (ogretimGorevlileri.Count() > 0)
                {
                    return new NIslemSonuc<OgretimGorevlisi>
                    {
                        BasariliMi = true,
                        Veri = ogretimGorevlileri.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<OgretimGorevlisi>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<OgretimGorevlisi> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(OgretimGorevlisi kayit)
        {
            try
            {
                _veritabani.OgretimGorevlileri.Add(kayit);
                _veritabani.SaveChanges();

                var uyelikEkleSonuc = _uyelik.KullaniciEkle(kayit.KimlikNo, "OgretimGorevlisi");
                if (uyelikEkleSonuc.BasariliMi)
                {
                    return new NIslemSonuc<int>
                    {
                        BasariliMi = true,
                        Veri = kayit.Id
                    };
                }
                else
                {
                    return new NIslemSonuc<int>
                    {
                        BasariliMi = false,
                        Veri = kayit.Id,
                        Mesaj = uyelikEkleSonuc.Mesaj
                    };
                }
                return null;
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
        public NIslemSonuc Guncelle(OgretimGorevlisi kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.OgretimGorevlileri.Where(o => o.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Ad = kayit.Ad;
                    duzenlenecekKayit.Soyad = kayit.Soyad;
                    duzenlenecekKayit.KimlikNo = kayit.KimlikNo;
                    duzenlenecekKayit.BolumId = kayit.BolumId;
                    duzenlenecekKayit.DogumTarih = kayit.DogumTarih;
                    duzenlenecekKayit.GirisTarih = kayit.GirisTarih;
                    duzenlenecekKayit.CikisTarih = kayit.CikisTarih;
                    duzenlenecekKayit.EPosta = kayit.EPosta;

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
                var silinecekKayitlar = _veritabani.OgretimGorevlileri.Where(o => o.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.OgretimGorevlileri.Remove(silinecekKayit);
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


        public NIslemSonuc<List<NDonemDers>> GetirDonemDersleri_SonDonem(int ogretimGorevlisiId)
        {
            var sonDonemler = (from d in _veritabani.Donemler
                               orderby new { d.Yil, d.DonemTip } descending
                               select d).ToList();

            if (sonDonemler.Count() > 0)
            {
                int sonDonem = sonDonemler.FirstOrDefault().Id;

                var dersler = (from d in _veritabani.DonemDersler
                               where d.OgretimGorevlisiId == ogretimGorevlisiId && d.DonemId == sonDonem
                               orderby new { d.Donem.Yil, d.Donem.DonemTip } descending
                               select new NDonemDers
                               {
                                   Id = d.Id,
                                   Ad = d.Ders.Ad,
                                   Yil = d.Donem.Yil,
                                   DonemTip = d.Donem.DonemTip,
                                   DonemId = d.DonemId
                               }).ToList();

                return new NIslemSonuc<List<NDonemDers>>
                {
                    BasariliMi = true,
                    Veri = dersler
                };
            }
            return new NIslemSonuc<List<NDonemDers>>();
        }
        public NIslemSonuc<List<NDonemDers>> GetirDonemDersleri_Tumu(int ogretimGorevlisiId)
        {
            var dersler = (from d in _veritabani.DonemDersler
                           where d.OgretimGorevlisiId == ogretimGorevlisiId
                           orderby new { d.Donem.Yil, d.Donem.DonemTip } descending
                           select new NDonemDers
                           {
                               Id = d.Id,
                               Ad = d.Ders.Ad,
                               Yil = d.Donem.Yil,
                               DonemTip = d.Donem.DonemTip,
                               DonemId = d.DonemId
                           }).ToList();

            return new NIslemSonuc<List<NDonemDers>>
            {
                BasariliMi = true,
                Veri = dersler
            };
        }
        public NIslemSonuc<List<NDersDonem>> GetirDonem_TumDonemler(int ogretimGorevlisiId)
        {
            try
            {
                var donemler = (from dd in _veritabani.DonemDersler
                               where dd.OgretimGorevlisiId == ogretimGorevlisiId
                               orderby new { dd.Donem.Yil, dd.Donem.DonemTip } descending
                               select new NDersDonem
                               {
                                   Id = dd.DonemId,
                                   DonemTip = dd.Donem.DonemTip,
                                   Yil = dd.Donem.Yil
                               }).Distinct().ToList();

                return new NIslemSonuc<List<NDersDonem>>
                {
                    BasariliMi = true,
                    Veri = donemler
                };

            }
            catch (Exception hata) { return new NIslemSonuc<List<NDersDonem>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<List<NDersDonem>> GetirDonem_SonDonem(int ogretimGorevlisiId)
        {
            try
            {
                var sonDonemler = (from d in _veritabani.Donemler
                                   orderby new { d.Yil, d.DonemTip } descending
                                   select d).ToList();

                if (sonDonemler.Count() > 0)
                {
                    int sonDonem = sonDonemler.FirstOrDefault().Id;


                    var donemler = (from dd in _veritabani.DonemDersler
                                    where dd.OgretimGorevlisiId == ogretimGorevlisiId && dd.DonemId == sonDonem
                                    orderby new { dd.Donem.Yil, dd.Donem.DonemTip } descending
                                    select new NDersDonem
                                    {
                                        Id = dd.DonemId,
                                        DonemTip = dd.Donem.DonemTip,
                                        Yil = dd.Donem.Yil
                                    }).Distinct().ToList();


                    return new NIslemSonuc<List<NDersDonem>>
                    {
                        BasariliMi = true,
                        Veri = donemler
                    };
                }
                else
                {
                    return new NIslemSonuc<List<NDersDonem>> { Mesaj = "Öğretim görevlisi son dönemde ders vermemiştir" };
                }
            }
            catch (Exception hata) { return new NIslemSonuc<List<NDersDonem>> { Mesaj = hata.Message }; }
        }

        public NIslemSonuc<List<NOgrenciDersNotu>> GetirDonemDersi_OgrenciNotlari(int donemDersId)
        {
            var dersler = (from d in _veritabani.OgrenciDonemDersler
                           where d.DonemDersId == donemDersId
                           orderby new { d.DonemDers.Donem.Yil, d.DonemDers.Donem.DonemTip, d.DonemDers.Ders.Ad } descending
                           select new NOgrenciDersNotu
                           {
                               Id = d.Id,
                               KimlikNo = d.Ogrenci.KimlikNo,
                               AdSoyad = d.Ogrenci.Ad + " " + d.Ogrenci.Soyad,
                               Vize1 = d.Vize1,
                               Vize2 = d.Vize2,
                               Final = d.Final,
                               Ortalama = d.Ortalama
                           }).ToList();

            return new NIslemSonuc<List<NOgrenciDersNotu>>
            {
                BasariliMi = true,
                Veri = dersler
            };
        }

        public NIslemSonuc<SelectList> GetirBolumunOgretimGorevlileri_SelectList(int bolumId)
        {
            var ogretimGorevlileri = (from o in _veritabani.OgretimGorevlileri
                                      where o.BolumId == bolumId
                                      orderby o.Ad
                                      select new SelectListItem
                                      {
                                          Text = o.Ad + " " + o.Soyad,
                                          Value = o.Id.ToString()
                                      }).ToList();

            return new NIslemSonuc<SelectList>
            {
                BasariliMi = true,
                Veri = new SelectList(ogretimGorevlileri, "Value", "Text")
            };
        }

    }
}