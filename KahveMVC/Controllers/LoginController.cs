using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KahveMVC.Models.EntityFramework;
using System.Web.Security;

namespace KahveMVC.Controllers
{

    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(kullanici kullaniciFormu, string ReturnUrl)
        {

            using (kahve2019Entities db = new kahve2019Entities())
            {

                kullaniciFormu.sifre = Sifrele.MD5Olustur(kullaniciFormu.sifre);
                var kullaniciVarmi = db.kullanici.FirstOrDefault(
                    x => x.ad == kullaniciFormu.ad && x.sifre == kullaniciFormu.sifre
                    );

                if (kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.ad, kullaniciFormu.BeniHatirla);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("/index", "urunler");
                    }

                   
                }

                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!!!";

                return View("index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}