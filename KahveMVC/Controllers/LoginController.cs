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
                // 1. Şifreyi şifrele ve kullanıcıyı ara
                kullaniciFormu.sifre = Sifrele.MD5Olustur(kullaniciFormu.sifre);
                var kullaniciVarmi = db.kullanici.FirstOrDefault(
                    x => x.ad == kullaniciFormu.ad && x.sifre == kullaniciFormu.sifre
                );

                if (kullaniciVarmi != null)
                {
                    // 2. Yetkilendirme çerezi oluştur
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.ad, kullaniciFormu.BeniHatirla);

                    // 3. KRİTİK NOKTA: Rolü hafızaya (Session) atıyoruz
                    Session["KullaniciAdi"] = kullaniciVarmi.ad;
                    Session["Rol"] = kullaniciVarmi.Rol;  // Artık hata vermeyecek!
                    Session["KullaniciId"] = kullaniciVarmi.id;

                    // 4. Yönlendirme (Rolüne göre)
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        // Yönetici ise Panele, değilse Siteye
                        if (kullaniciVarmi.Rol == "Yonetici" || kullaniciVarmi.Rol == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Default"); // Müşteri Anasayfası
                        }
                    }
                }

                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!!!";
                return View("index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // <-- BU SATIR ÇOK ÖNEMLİ! Hafızayı tamamen siler.
            return RedirectToAction("Index", "Default"); // Anasayfaya gönder
        }
    }
}