using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;

namespace KahveMVC.Controllers
{
    [Authorize] // Sadece giriş yapanlar
    public class ProfilController : Controller
    {
        MobilyaDbEntities db = new MobilyaDbEntities();

        // 1. PROFİL BİLGİLERİNİ GÖSTER (AYARLAR SAYFASI)
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["KullaniciId"] == null) return RedirectToAction("Index", "Login");

            int id = Convert.ToInt32(Session["KullaniciId"]);
            var kullanici = db.kullanici.Find(id);

            return View(kullanici);
        }

        // 2. PROFİLİ GÜNCELLE (KAYDET BUTONU)
        [HttpPost]
        public ActionResult Guncelle(kullanici gelenVeri)
        {
            if (Session["KullaniciId"] == null) return RedirectToAction("Index", "Login");

            int id = Convert.ToInt32(Session["KullaniciId"]);
            var kullanici = db.kullanici.Find(id);

            // Başkasının ID'siyle işlem yapmaya kalkarsa engelle
            if (kullanici.id != id) return RedirectToAction("Index");

            // --- BİLGİLERİ GÜNCELLE ---
            kullanici.ad = gelenVeri.ad;
            kullanici.telefon = gelenVeri.telefon;
            kullanici.adres = gelenVeri.adres;

            // Şifre boş bırakıldıysa değiştirme, doluysa güncelle
            if (!string.IsNullOrEmpty(gelenVeri.sifre))
            {
                kullanici.sifre = Sifrele.MD5Olustur(gelenVeri.sifre);
            }

            db.SaveChanges();

            // Adını değiştirdiyse Session'ı da güncelle ki üst barda hemen değişsin
            Session["KullaniciAdi"] = kullanici.ad;

            TempData["Mesaj"] = "Bilgileriniz başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        // 3. GEÇMİŞ SİPARİŞLERİM
        public ActionResult Siparislerim()
        {
            if (Session["KullaniciId"] == null) return RedirectToAction("Index", "Login");
            int uyeId = Convert.ToInt32(Session["KullaniciId"]);

            var siparisler = db.siparis
                               .Where(x => x.kullaniciId == uyeId)
                               .OrderByDescending(x => x.tarih)
                               .ToList();
            return View(siparisler);
        }

        // 4. SİPARİŞ DETAYI
        public ActionResult Detay(int id)
        {
            var siparis = db.siparis.Find(id);
            // Güvenlik: Sadece kendi siparişini görebilsin
            if (siparis == null || siparis.kullaniciId != Convert.ToInt32(Session["KullaniciId"]))
            {
                return RedirectToAction("Siparislerim");
            }
            return View(siparis);
        }
    }
}