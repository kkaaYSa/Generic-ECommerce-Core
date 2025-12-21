using KahveMVC.Models.EntityFramework;
using KahveMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KahveMVC.Controllers
{
    public class DefaultController : Controller
    {
        kahve2019Entities db = new kahve2019Entities();
        // GET: Default
        public ActionResult Index()
        {
            //id si 1 olan kaydı getirir
            var model = db.anasayfa.Find(1);
            return View(model);
        }

        [Route("Murunler")]
        public ActionResult Murunler()
        {
            //tüm ürünleri getirir
            //tüm ürünleri getirir
            //var model = (from x in db.urunler
            //             where x.aktif.Equals(1)
            //             orderby x.sira ascending
            //             select x).ToList();

            //var mod = db.urunler.Where(x => x.aktif.Equals(1)).OrderByDescending(a => a.sira).ToList();

            var model = db.urunler.Where(x=>x.aktif==true).OrderBy(x => x.sira).ToList();
            return View( model);
        }

        [Route("urun/{id}/{baslik}")]
        public ActionResult UrunDetay(int id)
        {
            var model = db.urunler.Where(x=>x.aktif==true && x.id==id).FirstOrDefault();
            //var model = db.urunler.Find(id); //kısıtlama yoksa 
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [Route("hakkimizda")]
        public ActionResult Hakkimizda()
        {
            //id si 1 olan kaydı getirir
            var model = db.hakkimizda.Find(1);
           
            return View(model);
        }

        [Route("magaza")]
        public ActionResult Magaza()
        {
            var model = new MagazaViewModel()
            {
                MagazaSaatler = db.magazasaat.ToList(),
                Magaza = db.magaza.First(),
                Hakkimizda = db.hakkimizda.First(),
            };

            return View(model);
        }

        [HttpGet]
        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }


        [HttpPost]
        [Route("iletisim")]
        public ActionResult Iletisim(iletisimformu veri)
        {
            if (ModelState.IsValid)
            {
                db.iletisimformu.Add(veri);
                db.SaveChanges();
                ModelState.Clear(); //formu temizledi
                ViewBag.Mesaj = veri.adSoyad + " Form bize ulaştı.";
                return View();
            }
            else
            {
                ViewBag.Hata = "Hata! Formu düzgün doldurun!";
                return View();
            }


        }

    }
}