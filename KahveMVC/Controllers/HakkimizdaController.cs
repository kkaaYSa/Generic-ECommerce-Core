using KahveMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: Hakimizda
        public ActionResult Index()
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }

        }
        public ActionResult HakkimizdaGuncelle()
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Kaydet(hakkimizda GelenVeri) {

            using (MobilyaDbEntities db = new MobilyaDbEntities ())
            {
                var GuncellenecekVeri = db.hakkimizda.First();
                if(!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", GelenVeri);
                }

                if (GelenVeri.fotoFile != null)
                {
                    GelenVeri.foto = GelenVeri.fotoFile.FileName;
                    //kaydetme
                    GelenVeri.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"),Path.GetFileName(GelenVeri.fotoFile.FileName)));
                }

                //güncelleme
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);

                db.SaveChanges();
                return RedirectToAction("/index", "hakkimizda");

                
            }
        }

    }
}