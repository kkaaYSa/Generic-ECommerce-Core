using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: Urunler
        public ActionResult Index()
        {
            // 1. GÜVENLİK BEKÇİSİ (Sadece 'Yonetici' girebilir)
            // Eğer rolü Yönetici değilse, Sipariş sayfasına postala.
            if (Session["Rol"] == null || Session["Rol"].ToString() != "Yonetici")
            {
                return RedirectToAction("Index", "SiparisYonetim");
            }

            // 2. VERİLERİ GETİR
            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }
        }

        public ActionResult Yeni()
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            var model = new urunler();
            return View("UrunForm", model);
        }

        public ActionResult Guncelle(int id)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var model = db.urunler.Find(id);
                if (model == null) return HttpNotFound();
                return View("UrunForm", model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Kaydet(urunler gelenUrun)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                if (!ModelState.IsValid)
                {
                    return View("UrunForm", gelenUrun);
                }

                if (gelenUrun.id == 0) // Yeni Kayıt
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin";
                        return View("UrunForm", gelenUrun);
                    }
                    string fotoAdi = Turkce.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    gelenUrun.foto = fotoAdi;
                    db.urunler.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urunEkle"] = "ekle";
                }
                else // Güncelleme
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.id);
                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Turkce.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.foto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urunGuncelle"] = "güncelleme";
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public void aktif(int id, bool durum)
        {
            // Ajax ile geldiği için void dönüyor ama içine yine de güvenlik koyalım
            if (Session["Rol"]?.ToString() != "Yonetici") return;

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var GuncellenecekVeri = db.urunler.Find(id);
                if (GuncellenecekVeri != null)
                {
                    GuncellenecekVeri.aktif = durum;
                    db.SaveChanges();
                }
            }
        }

        public ActionResult Sil(int id)
        {
            if (Session["Rol"]?.ToString() != "Yonetici") return RedirectToAction("Index", "SiparisYonetim");

            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var silincekVeri = db.urunler.Find(id);
                if (silincekVeri == null) return HttpNotFound();

                db.urunler.Remove(silincekVeri);
                db.SaveChanges();
                TempData["urunSil"] = "silindi";
                return RedirectToAction("Index");
            }
        }
    }
}