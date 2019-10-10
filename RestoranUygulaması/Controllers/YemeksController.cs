using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestoranUygulaması.Models;

namespace RestoranUygulaması.Controllers
{
    public class YemeksController : Controller
    {
        private Model1 db = new Model1();

        // GET: Yemeks
        public ActionResult Index()
        {
            return View(db.Yemek.ToList());
        }

        public ActionResult YemekGör()
        {
            if (Session["kul_ad"] == null)
            {
                return Redirect("Home");

            }
            string kisimizAd = Session["kul_ad"].ToString();
            var kisi = db.Kullanici.Where(i => i.kul_eposta == kisimizAd).SingleOrDefault();
            var yemek = kisi.Yemek.ToList();
            if (yemek == null)
            {
                return RedirectToAction("Create");
            }
            return View(yemek);
        }

        public ActionResult HazirlanisEkle(int id)
        {
            return RedirectToAction("Create", "Hazirlanis", new { id = id });
        }

        public ActionResult FiyatEkle(int id)
        {
            return RedirectToAction("Create", "Fiyats", new { id = id });
        }

        public ActionResult IcindekilerEkle(int id)
        {
            return RedirectToAction("Create", "Icindekilers", new { id = id });
        }

            // GET: Yemeks/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yemek yemek = db.Yemek.Find(id);
            if (yemek == null)
            {
                return HttpNotFound();
            }
            return View(yemek);
        }

        // GET: Yemeks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yemeks/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "yem_id, yemek_adi")]*/ Yemek yemek)
        {
            if (ModelState.IsValid)
            {
                string kisimizAd = Session["kul_ad"].ToString();
                var kisi = db.Kullanici.Where(i => i.kul_eposta == kisimizAd).SingleOrDefault();
                kisi.Yemek.Add(yemek);
                yemek.Kullanici.Add(kisi);
                db.Yemek.Add(yemek);
                db.SaveChanges();
                return RedirectToAction("YemekGör");
            }

            return View(yemek);
        }

        // GET: Yemeks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yemek yemek = db.Yemek.Find(id);
            if (yemek == null)
            {
                return HttpNotFound();
            }
            return View(yemek);
        }

        // POST: Yemeks/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yem_id,yemek_adi")] Yemek yemek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yemek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yemek);
        }

        // GET: Yemeks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yemek yemek = db.Yemek.Find(id);
            if (yemek == null)
            {
                return HttpNotFound();
            }
            return View(yemek);
        }

        // POST: Yemeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yemek yemek = db.Yemek.Find(id);
            db.Yemek.Remove(yemek);
            db.SaveChanges();
            return RedirectToAction("YemekGör");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
