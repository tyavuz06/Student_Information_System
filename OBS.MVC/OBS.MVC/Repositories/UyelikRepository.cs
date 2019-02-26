using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OBS.MVC.Models;
using OBS.MVC.Models.HelperModels;
using System;
using System.Linq;
using System.Web;

namespace OBS.MVC.Repositories
{
    public class UyelikRepository
    {
        #region Değişkenler

        OBSIdentityContext _db = new OBSIdentityContext();

        UserManager<ApplicationUser> UserManager { get; set; }
        IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }


        #endregion

        #region Constructor

        public UyelikRepository()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OBSIdentityContext()));
        }

        #endregion

        #region Kullanıcı İşlemleri

        public NIslemSonuc<string> KullaniciEkle(string kullaniciAd, string rol)
        {
            if (string.IsNullOrEmpty(kullaniciAd))
                return new NIslemSonuc<string> { Mesaj = "Lütfen kişinin kullanıcı adını belirtiniz" };
            if (string.IsNullOrEmpty(rol))
                return new NIslemSonuc<string> { Mesaj = "Lütfen kişinin rolünü belirtiniz" };
            try
            {
                var kontrolKullanici = UserManager.FindByName(kullaniciAd);
                if (kontrolKullanici != null)
                    return new NIslemSonuc<string> { Mesaj = "Bu kişi sistemde kayıtlıdır" };

                //Kullanıcıyı ekle
                ApplicationUser kullanici = new ApplicationUser
                {
                    UserName = kullaniciAd
                };
                _db.Users.Add(kullanici);
                _db.SaveChanges();

                UserManager.AddPassword(kullanici.Id, kullaniciAd);

                //Kullanıcıya rol ekle
                var rolEklemeSonuc = UserManager.AddToRole(kullanici.Id, rol);
                if (rolEklemeSonuc.Succeeded)
                    return new NIslemSonuc<string> { BasariliMi = true };
                else
                    return new NIslemSonuc<string> { BasariliMi = false, Mesaj = "Kullanıcıya rol tanımlaması yapılamadı" };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<string> { Mesaj = hata.ToString() };
            }
        }
        public NIslemSonuc<string> KullaniciRoleEkle(string kullaniciAd, string rol)
        {
            if (string.IsNullOrEmpty(kullaniciAd))
                return new NIslemSonuc<string> { Mesaj = "Lütfen kişinin kullanıcı adını belirtiniz" };
            if (string.IsNullOrEmpty(rol))
                return new NIslemSonuc<string> { Mesaj = "Lütfen kişinin rolünü belirtiniz" };
            try
            {
                var kullanici = UserManager.FindByName(kullaniciAd);
                var sonuc = UserManager.AddToRole(kullanici.Id, rol);
                if (sonuc.Succeeded)
                    return new NIslemSonuc<string> { BasariliMi = true };
                else
                    return new NIslemSonuc<string> { Mesaj = sonuc.Errors.ToString() };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc<string> { Mesaj = hata.ToString() };
            }

        }

        public NIslemSonuc<NSession> GirisYap(string kullaniciAdi, string sifre, UyeTip tip)
        {
            NSession session = new NSession();
            if (tip == UyeTip.Ogrenci)
            {
                OgrenciRepository ogrenciRepository = new OgrenciRepository(false);
                var ogrenci = ogrenciRepository.Getir(kullaniciAdi);
                if (!ogrenci.BasariliMi)
                    return new NIslemSonuc<NSession> { Mesaj = "Lütfen öğrenci bilgilerini kontrol ediniz" };

                session.Id = ogrenci.Veri.Id;
                session.BolumId = ogrenci.Veri.BolumId;
            }
            else if (tip == UyeTip.OgretimGorevlisi)
            {
                OgretimGorevlisiRepository ogretimGorevlisiRepository = new OgretimGorevlisiRepository(false);
                var ogretimGorevlisi = ogretimGorevlisiRepository.Getir(kullaniciAdi);
                if (!ogretimGorevlisi.BasariliMi)
                    return new NIslemSonuc<NSession> { Mesaj = "Lütfen öğretim görevlisi bilgilerini kontrol ediniz" };

                session.Id = ogretimGorevlisi.Veri.Id;
                session.BolumId = ogretimGorevlisi.Veri.BolumId;
            }
            else if (tip == UyeTip.BilgiIslem)//Bilgi işlem kullanıcısı öğrenci veya öğretim görevlisi olamaz
            {
                OgrenciRepository ogrenciRepository = new OgrenciRepository(false);
                var ogrenci = ogrenciRepository.Getir(kullaniciAdi);
                if (ogrenci.BasariliMi)
                    return new NIslemSonuc<NSession> { Mesaj = "Lütfen üye bilgilerini kontrol ediniz" };

                OgretimGorevlisiRepository ogretimGorevlisiRepository = new OgretimGorevlisiRepository(false);
                var ogretimGorevlisi = ogretimGorevlisiRepository.Getir(kullaniciAdi);
                if (ogretimGorevlisi.BasariliMi)
                    return new NIslemSonuc<NSession> { Mesaj = "Lütfen üye bilgilerini kontrol ediniz" };
            }

            // var kullanici = UserManager.Find(kullaniciAdi, sifre);
            if (kullanici != null)
            {
                AuthenticationManager.SignOut();
                var kimlik = UserManager.CreateIdentity(kullanici, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties(), kimlik);
                return new NIslemSonuc<NSession> { BasariliMi = true, Veri = session };
            }
            else
            {
                return new NIslemSonuc<NSession> { Mesaj = "Kullanıcı adı ve şifrenizi kontrol ediniz." };
            }
        }
        public NIslemSonuc CikisYap()
        {
            try
            {
                AuthenticationManager.SignOut();
                return new NIslemSonuc { BasariliMi = true };
            }
            catch (Exception hata)
            {
                return new NIslemSonuc { Mesaj = hata.ToString() };
            }
        }
        public NIslemSonuc<string> GirisYapanKullanici()
        {
            try
            {
                bool kullaniciGirisYaptiMi = AuthenticationManager.User.Identity.IsAuthenticated;
                if (kullaniciGirisYaptiMi)
                {
                    return new NIslemSonuc<string> { BasariliMi = true, Veri = AuthenticationManager.User.Identity.Name };
                }
            }
            catch { }
            return new NIslemSonuc<string>();
        }

        #endregion
    }

}