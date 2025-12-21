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
            // 1. GÜVENLİK BEKÇİSİ (Yönetici değilse anasayfaya kışkışla)
            if (Session["Rol"] == null || (Session["Rol"].ToString() != "Yonetici" && Session["Rol"].ToString() != "Admin"))
            {
                return RedirectToAction("Index", "Default");
            }

            // 2. VERİLERİ GETİR VE SAYFAYI AÇ
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }
        }

        public ActionResult Yeni()
        {
            var model = new urunler();
            return View("UrunForm", model);

        }

        public ActionResult Guncelle(int id)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {

                var model = db.urunler.Find(id);

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);

            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Kaydet(urunler gelenUrun)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                if (!ModelState.IsValid) //formun doğru dolduruludu mu?
                {
                    return View("UrunForm", gelenUrun);
                }

                if (gelenUrun.id == 0)//Yeni ürün kaydı
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
                else  //Güncelleme 
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.id);
                    if (!ModelState.IsValid)
                    {
                        return View("UrunForm", gelenUrun);
                    }

                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Turkce.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);

                        gelenUrun.foto = fotoAdi;
                        //kaydetme
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }

                    //güncelleme
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urunGuncelle"] = "güncelleme";
                }

                db.SaveChanges();
                return RedirectToAction("/index");
            }
        }

        [HttpPost]
        public void aktif(int id, bool durum)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var GuncellenecekVeri = db.urunler.Find(id);
                GuncellenecekVeri.aktif = durum;
                db.SaveChanges();
            }



        }



        public ActionResult Sil(int id)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {

                var silincekVeri = db.urunler.Find(id);

                if (silincekVeri == null)
                {
                    return HttpNotFound();
                }

                db.urunler.Remove(silincekVeri);
                db.SaveChanges();


                TempData["urunSil"] = "silindi";
                return RedirectToAction("Index");
            }

        }
    }
}