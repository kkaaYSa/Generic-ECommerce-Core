using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;
using System.Security.Cryptography;
using System.Text;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        // GET: Kullanici Listesi
        public ActionResult Index()
        {
            // 1. GÜVENLİK BEKÇİSİ (Sadece 'Yonetici' girebilir)
            if (Session["Rol"] == null || Session["Rol"].ToString() != "Yonetici")
            {
                // Yetkin yok dayı, siparişlere git sen
                return RedirectToAction("Index", "SiparisYonetim");
            }

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var model = db.kullanici.ToList();
                return View(model);
            }
        }

        public ActionResult Yeni()
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            var model = new kullanici();
            return View("KullaniciForm", model);
        }

        public ActionResult Guncelle(int id)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var model = db.kullanici.Find(id);
                if (model == null) return HttpNotFound();
                return View("KullaniciForm", model);
            }
        }

        [HttpPost]
        public ActionResult Kaydet(kullanici gelenKullanici)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                if (!ModelState.IsValid)
                {
                    return View("KullaniciForm", gelenKullanici);
                }

                if (gelenKullanici.id == 0) // Yeni Kullanıcı
                {
                    gelenKullanici.sifre = Sifrele.MD5Olustur(gelenKullanici.sifre);
                    if (string.IsNullOrEmpty(gelenKullanici.Rol))
                    {
                        gelenKullanici.Rol = "Musteri";
                    }
                    db.kullanici.Add(gelenKullanici);
                    TempData["Ekle"] = "eklendi";
                }
                else // Güncelleme
                {
                    var guncellenecekVeri = db.kullanici.Find(gelenKullanici.id);

                    // Şifre boşsa elleme, doluysa güncelle
                    if (string.IsNullOrEmpty(gelenKullanici.sifre))
                    {
                        gelenKullanici.sifre = guncellenecekVeri.sifre;
                    }
                    else
                    {
                        gelenKullanici.sifre = Sifrele.MD5Olustur(gelenKullanici.sifre);
                    }

                    db.Entry(guncellenecekVeri).CurrentValues.SetValues(gelenKullanici);
                    TempData["Guncelle"] = "güncelleme";
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sil(int id)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var silincekVeri = db.kullanici.Find(id);
                if (silincekVeri == null) return HttpNotFound();

                db.kullanici.Remove(silincekVeri);
                db.SaveChanges();
                TempData["Sil"] = "silindi";
                return RedirectToAction("Index");
            }
        }
    }
}