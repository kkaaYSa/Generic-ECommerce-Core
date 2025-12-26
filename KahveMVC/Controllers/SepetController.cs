using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KahveMVC.Models; // SepetUrun modelinin olduğu yer

namespace KahveMVC.Controllers
{
    public class SepetController : Controller
    {
        MobilyaDbEntities db = new MobilyaDbEntities();

        // 1. SEPETİ GÖSTER
        public ActionResult Index()
        {
            var sepet = (List<SepetUrun>)Session["Sepet"];
            if (sepet == null) sepet = new List<SepetUrun>();
            return View(sepet);
        }

        // 2. SEPETE EKLE (Stok Kontrollü)
        public ActionResult SepeteEkle(int id)
        {
            var urun = db.urunler.Find(id);
            if (urun != null)
            {
                if (Session["Sepet"] == null) Session["Sepet"] = new List<SepetUrun>();
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];

                var sepettekiUrun = sepet.FirstOrDefault(x => x.UrunId == id);

                // --- STOK KONTROLÜ ---
                int suankiAdet = (sepettekiUrun != null) ? sepettekiUrun.adet : 0;

                if (urun.stok <= suankiAdet) // Stok yetersizse ekleme
                {
                    TempData["StokHata"] = "Stok yetersiz! Bu üründen daha fazla yok.";
                    return RedirectToAction("Index", "Default"); // Ürünlere geri dön
                }
                // ---------------------

                if (sepettekiUrun != null)
                {
                    sepettekiUrun.adet++;
                }
                else
                {
                    sepet.Add(new SepetUrun
                    {
                        UrunId = urun.id,
                        UrunAd = urun.baslik,
                        Resim = urun.foto,
                        adet = 1,
                        Fiyat = urun.Fiyat ?? 0
                    });
                }
                Session["Sepet"] = sepet; // Güncel sepeti kaydet
            }
            return RedirectToAction("Index"); // Sepete git
        }

        // 3. ADET ARTTIR (+ Butonu)
        public ActionResult AdetArttir(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var urunSepet = sepet.FirstOrDefault(x => x.UrunId == id);
                var urunDb = db.urunler.Find(id); // Gerçek stoga bak

                if (urunSepet != null && urunDb != null)
                {
                    if (urunSepet.adet < urunDb.stok)
                    {
                        urunSepet.adet++;
                    }
                    else
                    {
                        // İstersen burada hata mesajı verdirebilirsin
                    }
                }
                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }

        // 4. ADET AZALT (- Butonu)
        public ActionResult AdetAzalt(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var urun = sepet.FirstOrDefault(x => x.UrunId == id);

                if (urun != null)
                {
                    if (urun.adet > 1)
                        urun.adet--;
                    else
                        sepet.Remove(urun); // 1 taneyse sil
                }
                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }

        // 5. SEPETTEN SİL
        public ActionResult SepettenSil(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var silinecek = sepet.FirstOrDefault(x => x.UrunId == id);
                if (silinecek != null)
                {
                    sepet.Remove(silinecek);
                }
                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }

        public ActionResult SepetiOnayla()
        {
            return RedirectToAction("SiparisSecim");
        }

        
        [HttpGet]
        public ActionResult SiparisSecim()
        {
            // Eğer kullanıcı giriş yapmışsa bilgilerini getir
            if (Session["KullaniciId"] != null)
            {
                int id = Convert.ToInt32(Session["KullaniciId"]);
                var kullanici = db.kullanici.Find(id);

                if (kullanici != null)
                {
                    ViewBag.Ad = kullanici.ad;       // Ad Soyad
                    ViewBag.Tel = kullanici.telefon; // Telefon
                    ViewBag.Adres = kullanici.adres; // Adres
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult SiparisiTamamla(string adSoyad, string telefon, string adres)
        {
            var sepet = Session["Sepet"] as List<SepetUrun>;
            if (sepet == null) return RedirectToAction("Index");

            // --- SİPARİŞİ KAYDET VE STOKTAN DÜŞ ---
            siparis yeniSiparis = new siparis();
            yeniSiparis.adSoyad = adSoyad;
            yeniSiparis.telefon = telefon;
            yeniSiparis.adres = adres;
            yeniSiparis.tarih = DateTime.Now;
            yeniSiparis.durum = "Bekleniyor";
            yeniSiparis.toplamTutar = sepet.Sum(x => x.Fiyat * x.adet);

            if (Session["KullaniciId"] != null)
                yeniSiparis.kullaniciId = Convert.ToInt32(Session["KullaniciId"]);

            db.siparis.Add(yeniSiparis);
            db.SaveChanges(); // Önce siparişi oluştur ki ID oluşsun

            foreach (var item in sepet)
            {
                // Detay Ekle
                siparisdetay d = new siparisdetay();
                d.siparisId = yeniSiparis.id;
                d.urunId = item.UrunId;
                d.adet = item.adet;
                d.fiyat = item.Fiyat;
                db.siparisdetay.Add(d);

                // ** STOKTAN DÜŞME İŞLEMİ **
                var urunDb = db.urunler.Find(item.UrunId);
                if (urunDb != null)
                {
                    urunDb.stok = urunDb.stok - item.adet;
                }
            }
            db.SaveChanges();

            Session["Sepet"] = null; // Sepeti boşalt
            return View("SiparisTamamlandi");
        }
    }
}