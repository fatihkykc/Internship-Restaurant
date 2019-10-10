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
    public class RezervasyonsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Rezervasyons
        public ActionResult Index()
        {
            return View(db.Rezervasyons.ToList());
        }

        public ActionResult RezervasyonGör()
        {
            return View(db.Rezervasyons.ToList());
        }



        // GET: Rezervasyons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rezervasyon = db.Rezervasyons.Where(x => x.rez_id == id).SingleOrDefault();
            if (rezervasyon == null)
            {
                return HttpNotFound();
            }
            return View(rezervasyon);
        }

        // GET: Rezervasyons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rezervasyons/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rez_id,rez_ad,rez_soyad,rez_eposta,rez_tel")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                db.Rezervasyons.Add(rezervasyon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rezervasyon);
        }

        // GET: Rezervasyons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervasyon rezervasyon = db.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return HttpNotFound();
            }
            return View(rezervasyon);
        }

        // POST: Rezervasyons/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rez_id,rez_ad,rez_soyad,rez_eposta,rez_tel")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervasyon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rezervasyon);
        }

        // GET: Rezervasyons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervasyon rezervasyon = db.Rezervasyons.Find(id);
            if (rezervasyon == null)
            {
                return HttpNotFound();
            }
            return View(rezervasyon);
        }

        // POST: Rezervasyons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervasyon rezervasyon = db.Rezervasyons.Find(id);
            db.Rezervasyons.Remove(rezervasyon);
            db.SaveChanges();
            return RedirectToAction("Index");
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
