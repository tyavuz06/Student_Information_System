using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OBS.MVC.Repositories
{
    public class OgrenciRepository : BaseRepository
    {
        public OgrenciRepository(bool uyelikAlinsinMi = true) : base(uyelikAlinsinMi) { }
        public NIslemSonuc<List<Ogrenci>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<Ogrenci>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Ogrenciler.OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Ogrenci>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<List<Ogrenci>> GetirBolumunOgrencileri(int bolumId)
        {
            try
            {
                return new NIslemSonuc<List<Ogrenci>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Ogrenciler.Where(o => o.BolumId == bolumId).OrderBy(o => o.Ad).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Ogrenci>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<Ogrenci> Getir(int id)
        {
            try
            {
                var ogrenciler = (from o in _veritabani.Ogrenciler
                                  where o.Id == id
                                  orderby o.Ad, o.Soyad
                                  select o);
                if (ogrenciler.Count() > 0)
                {
                    return new NIslemSonuc<Ogrenci>
                    {
                        BasariliMi = true,
                        Veri = ogrenciler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Ogrenci>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Ogrenci> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<Ogrenci> Getir(string kimlikNo)
        {
            try
            {
                var ogrenciler = (from o in _veritabani.Ogrenciler
                                  where o.KimlikNo == kimlikNo
                                  orderby o.Ad, o.Soyad
                                  select o);
                if (ogrenciler.Count() > 0)
                {
                    return new NIslemSonuc<Ogrenci>
                    {
                        BasariliMi = true,
                        Veri = ogrenciler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Ogrenci>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Ogrenci> { Mesaj = hata.Message }; }
        }

        public NIslemSonuc<int> Kaydet(Ogrenci kayit)
        {
            try
            {
                _veritabani.Ogrenciler.Add(kayit);
                _veritabani.SaveChanges();

                var uyelikEkleSonuc = _uyelik.KullaniciEkle(kayit.KimlikNo, "Ogrenci");
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
        public NIslemSonuc Guncelle(Ogrenci kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.Ogrenciler.Where(o => o.Id == kayit.Id);
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
                var silinecekKayitlar = _veritabani.Ogrenciler.Where(o => o.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.Ogrenciler.Remove(silinecekKayit);
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