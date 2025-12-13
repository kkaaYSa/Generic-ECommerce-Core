using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KahveMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [Route("admin")]
        public ActionResult Index()
        {
            return RedirectToAction("/index","urunler");
        }
    }
}