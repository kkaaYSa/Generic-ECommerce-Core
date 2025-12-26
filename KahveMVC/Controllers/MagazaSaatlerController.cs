using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class MagazaSaatlerController : Controller
    {
        public ActionResult Index()
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {
                var model = db.magazasaat.ToList();
                return View(model);
            }
        }



        public ActionResult MagazaSaatGuncelle(int id)
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {

                var model = db.magazasaat.Find(id);

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View(model);

            }

        }

        public ActionResult Kaydet(magazasaat gelenVeri)
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {
                if (!ModelState.IsValid) //formun doğru dolduruludu mu?
                {
                    return View("MagazaSaatGuncelle", gelenVeri);
                }



                var GuncellenecekVeri = db.magazasaat.Find(gelenVeri.id);
                if (!ModelState.IsValid)
                {
                    return View("MagazaSaatGuncelle", gelenVeri);
                }

                //güncelleme
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenVeri);
                TempData["saatGuncelle"] = "güncelleme";


                db.SaveChanges();
                return RedirectToAction("/index");
            }
        }





    }
}