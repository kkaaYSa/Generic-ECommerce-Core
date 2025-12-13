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
        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Default");
        }
    }
}