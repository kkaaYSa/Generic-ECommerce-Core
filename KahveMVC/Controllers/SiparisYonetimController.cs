using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework; // Model klasörün burası

namespace KahveMVC.Controllers
{
    // Sadece Yöneticiler girebilsin
    // Eğer senin admin rolün 'Yonetici' ise "Authorize(Roles='Yonetici')" yap.
    // Şimdilik sadece giriş yapmış olması yeterli diyelim, istersen rol eklersin.
    [Authorize]
    public class SiparisYonetimController : Controller
    {
        MobilyaDbEntities db = new MobilyaDbEntities();

        // 1. SİPARİŞLERİ LİSTELE
        public ActionResult Index()
        {
            // En son gelen sipariş en üstte görünsün (OrderByDescending)
            var siparisler = db.siparis.OrderByDescending(x => x.tarih).ToList();
            return View(siparisler);
        }

        // 2. SİPARİŞ DETAYINI GÖR
        public ActionResult Detay(int id)
        {
            // Siparişi bul
            var siparis = db.siparis.Find(id);

            // O siparişe ait ürünleri getir (SiparisDetay tablosundan)
            // Not: Senin veritabanında sipariş ile ürünler arası ilişki olduğunu varsayıyorum.
            // Eğer lazy loading açıksaa "siparis.siparisdetay" diyerek ürünlere ulaşabiliriz.

            return View(siparis);
        }

        // 3. SİPARİŞ DURUMUNU GÜNCELLE (Kargolandı, Teslim Edildi vs.)
        [HttpPost]
        public ActionResult DurumDegistir(int id, string durum)
        {
            var siparis = db.siparis.Find(id);
            siparis.durum = durum;
            db.SaveChanges();

            // Detay sayfasına geri dön
            return RedirectToAction("Detay", new { id = id });
        }
    }
}