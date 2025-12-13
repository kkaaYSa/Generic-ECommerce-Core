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
        // GET: Kullanici
        // GET: Urunler
        public ActionResult Index()
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var model = db.kullanici.ToList();
                return View(model);
            }
        }

        public ActionResult Yeni()
        {
            var model = new kullanici();
            return View("KullaniciForm", model);

        }

        public ActionResult Guncelle(int id)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {

                var model = db.kullanici.Find(id);

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("KullaniciForm", model);

            }

        }

        public ActionResult Kaydet(kullanici gelenKullanici)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                if (!ModelState.IsValid) //formun doğru dolduruludu mu?
                {
                    return View("KullaniciForm", gelenKullanici);
                }

                gelenKullanici.sifre = Sifrele.MD5Olustur(gelenKullanici.sifre);

                if (gelenKullanici.id == 0)//Yeni ürün kaydı
                {
                    db.kullanici.Add(gelenKullanici);

                }
                else  //Güncelleme 
                {
                    var GuncellenecekVeri = db.kullanici.Find(gelenKullanici.id);

                    //güncelleme
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenKullanici);
                    TempData["Guncelle"] = "güncelleme";
                }

                db.SaveChanges();
                return RedirectToAction("/index", "Kullanici");
            }
        }

     

        public ActionResult Sil(int id)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {

                var silincekVeri = db.kullanici.Find(id);

                if (silincekVeri == null)
                {
                    return HttpNotFound();
                }

                db.kullanici.Remove(silincekVeri);
                db.SaveChanges();


                TempData["Sil"] = "silindi";
                return RedirectToAction("/index", "Kullanici");
            }

        }



    }
}