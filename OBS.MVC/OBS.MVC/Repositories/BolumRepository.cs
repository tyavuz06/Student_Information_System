using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OBS.MVC.Repositories
{
    public class BolumRepository : BaseRepository
    {
        public NIslemSonuc<List<Bolum>> GetirTumu()
        {
            try
            {
                return new NIslemSonuc<List<Bolum>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Bolumler.ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Bolum>> { Mesaj = hata.Message }; }

        }
        public NIslemSonuc<List<Bolum>> GetirFakulteninBolumleri(int fakulteId)
        {
            try
            {
                return new NIslemSonuc<List<Bolum>>
                {
                    BasariliMi = true,
                    Veri = _veritabani.Bolumler.Where(b => b.FakulteId == fakulteId).ToList()
                };
            }
            catch (Exception hata) { return new NIslemSonuc<List<Bolum>> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<Bolum> Getir(int id)
        {
            try
            {
                var bolumler = (from b in _veritabani.Bolumler
                                where b.Id == id
                                select b);
                if (bolumler.Count() > 0)
                {
                    return new NIslemSonuc<Bolum>
                    {
                        BasariliMi = true,
                        Veri = bolumler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NIslemSonuc<Bolum>();
                }
            }
            catch (Exception hata) { return new NIslemSonuc<Bolum> { Mesaj = hata.Message }; }
        }
        public NIslemSonuc<int> Kaydet(Bolum kayit)
        {
            try
            {
                _veritabani.Bolumler.Add(kayit);
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
        public NIslemSonuc Guncelle(Bolum kayit)
        {
            try
            {
                var duzenlenecekKayitlar = _veritabani.Bolumler.Where(b => b.Id == kayit.Id);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Ad = kayit.Ad;
                    duzenlenecekKayit.Adres = kayit.Adres;
                    duzenlenecekKayit.FakulteId = kayit.FakulteId;
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
                var silinecekKayitlar = _veritabani.Bolumler.Where(b => b.Id == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    _veritabani.Bolumler.Remove(silinecekKayit);
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
            var bolumler = (from f in _veritabani.Bolumler
                            orderby f.Ad
                            select new SelectListItem
                            {
                                Text = f.Ad,
                                Value = f.Id.ToString()
                            }).ToList();

            return new NIslemSonuc<SelectList>
            {
                BasariliMi = true,
                Veri = new SelectList(bolumler, "Value", "Text")
            };
        }
    }
}