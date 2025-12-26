using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;
using System.Web.Security;

namespace KahveMVC.Controllers
{
    public class LoginController : Controller
    {
        MobilyaDbEntities db = new MobilyaDbEntities();

        // 1. ANA SAYFAYA YÖNLENDİRME (GET Index)
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Default");
        }

        // 2. KAYIT OL SAYFASINI AÇ (GET)
        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }

        // 3. KAYIT İŞLEMİNİ YAP (POST) - DÜZELTİLMİŞ HALİ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOl(kullanici gelenKullanici)
        {
            // ADIM 1: Form validasyonu (Boş alan var mı?)
            if (!ModelState.IsValid)
            {
                return View(gelenKullanici);
            }

            // ADIM 2: Kullanıcı adı daha önce alınmış mı kontrol et
            var kontrol = db.kullanici.FirstOrDefault(x => x.ad == gelenKullanici.ad);
            if (kontrol != null)
            {
                ViewBag.Hata = "Bu kullanıcı adı zaten kullanılıyor!";
                return View(gelenKullanici);
            }

            // ADIM 3: Verileri Hazırla ve Kaydet (SADECE BURADA KAYDEDİYORUZ)
            gelenKullanici.Rol = "Musteri"; // Yeni gelen herkes Müşteridir
            gelenKullanici.sifre = Sifrele.MD5Olustur(gelenKullanici.sifre);

            db.kullanici.Add(gelenKullanici);
            db.SaveChanges();

            // ADIM 4: Otomatik Giriş Yap (Beni Hatırla kapalı olarak)
            FormsAuthentication.SetAuthCookie(gelenKullanici.ad, false);
            Session["KullaniciAdi"] = gelenKullanici.ad;
            Session["Rol"] = gelenKullanici.Rol;
            Session["KullaniciId"] = gelenKullanici.id;

            return RedirectToAction("Index", "Default");
        }

        // 4. GİRİŞ YAP (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(kullanici kullaniciFormu, string ReturnUrl, bool BeniHatirla = false)
        {
            // Şifreleme
            string md5Sifre = Sifrele.MD5Olustur(kullaniciFormu.sifre);

            var kullaniciVarmi = db.kullanici.FirstOrDefault(
                x => x.ad == kullaniciFormu.ad && x.sifre == md5Sifre
            );

            if (kullaniciVarmi != null)
            {
                // --- GİRİŞ BAŞARILI ---
                FormsAuthentication.SetAuthCookie(kullaniciVarmi.ad, BeniHatirla);
                Session["KullaniciAdi"] = kullaniciVarmi.ad;
                Session["Rol"] = kullaniciVarmi.Rol;
                Session["KullaniciId"] = kullaniciVarmi.id;

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    // Yönetici ise Panele, Müşteri ise Ana Sayfaya
                    // Dikkat: Admin controller'ının adı 'Admin' mi yoksa 'AnaSayfa' mı? 
                    // Senin projende panele giden controller hangisiyse onu yaz. 
                    // Genelde admin paneli için RedirectToAction("Index", "Admin") kullanılır.
                    // Eğer senin admin panelin AnasayfaController ise RedirectToAction("Index", "Anasayfa") yap.

                    if (kullaniciVarmi.Rol == "Yonetici" || kullaniciVarmi.Rol == "Admin")
                        return RedirectToAction("Index", "Admin"); // veya "Anasayfa"
                    else
                        return RedirectToAction("Index", "Default");
                }
            }
            else
            {
                // --- GİRİŞ HATALI ---
                TempData["GirisHata"] = "Kullanıcı adı veya şifre hatalı!";

                if (Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.ToString());

                return RedirectToAction("Index", "Default");
            }
        }

        // 5. ÇIKIŞ YAP
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }
    }
}