using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class MagazaController : Controller
    {

        // GET: Magaza
        public ActionResult Index()
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var model = db.magaza.First();
                return View(model);
            }

        }
        public ActionResult MagazaGuncelle()
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var model = db.magaza.First();
                return View(model);
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Kaydet(magaza GelenVeri)
        {

            using (kahve2019Entities db = new kahve2019Entities())
            {
                var GuncellenecekVeri = db.magaza.First();
                if (!ModelState.IsValid)
                {
                    return View("MagazaGuncelle", GelenVeri);
                }

               

                //güncelleme
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);

                db.SaveChanges();
                return RedirectToAction("/index", "magaza");


            }
        }

    }
}