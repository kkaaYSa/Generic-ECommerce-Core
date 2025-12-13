using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class IletisimController : Controller
    {
        // GET: Iletisim
        public ActionResult Index()
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {
                var model = db.iletisimformu.ToList();
                return View(model);
            }

        }

        public ActionResult Sil(int id)
        {
            using (kahve2019Entities db = new kahve2019Entities())
            {

                var silincekVeri = db.iletisimformu.Find(id);

                if(silincekVeri==null)
                {
                    return HttpNotFound();
                }

                db.iletisimformu.Remove(silincekVeri);
                db.SaveChanges();



                return RedirectToAction("/index","Iletisim");
            }

        }
    }
}