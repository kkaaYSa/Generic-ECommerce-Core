using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models;
using System.Net.NetworkInformation;


namespace KahveMVC.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet
        public ActionResult Index()
        {
            // Session'daki sepeti çekiyoruz
            var sepet = (List<SepetUrun>)Session["Sepet"];

            // Eğer sepet henüz oluşmamışsa (boşsa), boş bir liste gönderelim ki hata vermesin
            if (sepet == null)
            {
                sepet = new List<SepetUrun>();
            }

            // Listeyi View'a gönderiyoruz (Bunu yapmazsan ekran boş kalır!)
            return View(sepet);
        }
        public ActionResult SepeteEkle(int id)
        {
            kahve2019Entities db = new kahve2019Entities();
            var urun = db.urunler.Find(id);
            if (urun != null)
            {
                if (Session["Sepet"] == null)
                {
                    Session["Sepet"] = new List<SepetUrun>();
                }
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var sepettekiUrun = sepet.FirstOrDefault(x => x.UrunId == id);
                if (sepettekiUrun != null)
                {
                    sepettekiUrun.adet++;
                }
                else
                {
                    sepet.Add(new SepetUrun { UrunId = urun.id, UrunAd = urun.baslik, Resim = urun.foto, adet = 1, fiyat = 50 });

                }

                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }
        // --- SepetController.cs dosyasının içine eklenecekler ---

        // 1. ADET ARTIRMA (+)
        public ActionResult AdetArttir(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var urun = sepet.FirstOrDefault(x => x.UrunId == id);
                if (urun != null)
                {
                    urun.adet++; // Adeti 1 artır
                }
                Session["Sepet"] = sepet; // Güncel hali kaydet
            }
            return RedirectToAction("Index");
        }

        // 2. ADET AZALTMA (-)
        public ActionResult AdetAzalt(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var urun = sepet.FirstOrDefault(x => x.UrunId == id);
                if (urun != null)
                {
                    if (urun.adet > 1)
                        urun.adet--; // 1'den büyükse azalt
                    else
                        sepet.Remove(urun); // 1 ise ve eksiye bastıysa sil
                }
                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }

        // 3. SEPETTEN SİL (Çöp Kutusu)
        public ActionResult SepettenSil(int id)
        {
            if (Session["Sepet"] != null)
            {
                List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
                var silinecek = sepet.FirstOrDefault(x => x.UrunId == id);
                if (silinecek != null)
                {
                    sepet.Remove(silinecek); // Listeden uçur
                }
                Session["Sepet"] = sepet;
            }
            return RedirectToAction("Index");
        }

        // 4. SEPETİ ONAYLA
        public ActionResult SepetiOnayla()
        {
            // Sepet boşsa onaylatma
            if (Session["Sepet"] == null)
            {
                return RedirectToAction("Index");
            }

            List<KahveMVC.Models.SepetUrun> sepet = (List<KahveMVC.Models.SepetUrun>)Session["Sepet"];

            if (sepet.Count == 0)
            {
                return RedirectToAction("Index"); // Sepet boşsa geri dön
            }

            // Kullanıcı zaten giriş yapmışsa (Müşteri ise) direkt Özet sayfasına git
            if (Session["Kullanici"] != null)
            {
                return RedirectToAction("SiparisTamamla", "Sepet"); // Burayı sonra yapacağız
            }

            // Giriş yapmamışsa, o dediğin "Seçim Ekranına" git
            return RedirectToAction("GirisSecimi", "Kullanici");
        }
    }
}