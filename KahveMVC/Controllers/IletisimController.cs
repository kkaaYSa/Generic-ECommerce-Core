using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;

namespace KahveMVC.Controllers
{
    [Authorize] // Sadece giriş yapan adminler görebilir
    public class IletisimController : Controller
    {
        // GET: Iletisim Listesi
        public ActionResult Index()
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                // Mesajları tarihe göre tersten sırala (En yeni en üstte)
                var model = db.iletisimformu.OrderByDescending(x => x.tarih).ToList();
                return View(model);
            }
        }

        public ActionResult Sil(int id)
        {
            using (MobilyaDbEntities db = new MobilyaDbEntities())
            {
                var silincekVeri = db.iletisimformu.Find(id);

                if (silincekVeri == null)
                {
                    return HttpNotFound();
                }

                db.iletisimformu.Remove(silincekVeri);
                db.SaveChanges();

                return RedirectToAction("/Index","Iletisim");
            }
        }
    }
}