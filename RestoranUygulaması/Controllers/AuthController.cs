using RestoranUygulaması.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RestoranUygulaması.Controllers
{
    public class AuthController : Controller
    {
        private Model1 db = new Model1();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnurl)
        {
            if (ModelState.IsValid)
            {

                var _kullanici = db.Kullanici.Where(k => k.kul_eposta == model.kul_eposta && k.kul_sifre == model.kul_sifre).SingleOrDefault();
                //Aşağıdaki if komutu gönderilen mail ve şifre doğrultusunda kullanıcı kontrolu yapar. Eğer kullanıcı var ise login olur.
                if (_kullanici!=null)
                {
                    Session["kul_ad"] = _kullanici.kul_eposta;
                    FormsAuthentication.SetAuthCookie(model.kul_eposta, true);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "EMail veya şifre hatalı!");
                }
            }
            return View(model);
        }
        public ActionResult logOut()
        {
            Session["kul_ad"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}