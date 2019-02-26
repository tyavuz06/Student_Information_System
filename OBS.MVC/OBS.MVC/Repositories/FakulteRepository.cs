using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using OBS.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OBS.MVC.Repositories
{
    public class FakulteRepository : BaseRepository
    {
        public NIslemSonuc<List<Fakulte>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<Fakulte>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Fakulteler.ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Fakulte>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<Fakulte> Getir(int id)
        {
            try
            {
                var fakulteler = (from f in _veritabani.Fakulteler
                                  where f.Id == id
                                  select f);
                if (fakulteler.Count() > 0)
                {
                    return new NIslemSonuc<Fakulte>
                    {
                        BasariliMi = true,
                        Veri = fakulteler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Fakulte>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Fakulte> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(Fakulte kayit)
        {
            try
            {
                _veritabani.Fakulteler.Add(kayit);
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
        public NIslemSonuc Guncelle(Fakulte kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.Fakulteler.Where(f => f.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Ad = kayit.Ad;
                    duzenlenecekKayit.Adres = kayit.Adres;
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
                var silinecekKayitlar = _veritabani.Fakulteler.Where(f => f.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.Fakulteler.Remove(silinecekKayit);
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
            var fakulteler = (from f in _veritabani.Fakulteler
                              orderby f.Ad
                              select new SelectListItem
                              {
                                  Text = f.Ad,
                                  Value = f.Id.ToString()
                              }).ToList();

            return new NIslemSonuc<SelectList>
            {
                BasariliMi = true,
                Veri = new SelectList(fakulteler, "Value", "Text")
            };
        }
        public NIslemSonuc<List<NSelectListItem>> GetirTumu_NSelectList(int seciliFakulteId = 0)
        {
            var fakulteler = (from f in _veritabani.Fakulteler
                              orderby f.Ad
                              select new NSelectListItem
                              {
                                  Text = f.Ad,
                                  Value = f.Id.ToString()
                              }).ToList();
            if (seciliFakulteId > 0)
            {
                for (int i = 0; i < fakulteler.Count; i++)
                {
                    if (fakulteler[i].Value == seciliFakulteId.ToString())
                    {
                        fakulteler[i].Selected = true;
                        break;
                    }
                }
            }

            return new NIslemSonuc<List<NSelectListItem>>
            {
                BasariliMi = true,
                Veri = fakulteler
            };
        }
    }
}