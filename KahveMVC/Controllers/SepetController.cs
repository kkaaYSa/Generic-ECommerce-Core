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
            return View();
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
    }
}