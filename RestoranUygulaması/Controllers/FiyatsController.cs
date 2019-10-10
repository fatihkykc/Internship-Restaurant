
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
    public class FiyatsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Fiyats
        public ActionResult Index()
        {
            return View(db.Fiyat.ToList());
        }
        public ActionResult FiyatGör(int id)
        {
            var fiyatt = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
            return View(fiyatt.Fiyat);
        }

        // GET: Fiyats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fiyat = db.Fiyat.Where(x => x.fiyat_id == id).SingleOrDefault();
            if (fiyat == null)
            {
                return HttpNotFound();
            }

            return View(fiyat);
        }

        // GET: Fiyats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fiyats/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fiyat_id,fiyat1")] Fiyat fiyattable, int id)
        {
            if (ModelState.IsValid)
            {
                YemFiy kh = new YemFiy();
                kh.yem_id = id;
                kh.fiyat_id = fiyattable.fiyat_id;
                var yemfiy = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
                yemfiy.Fiyat.Add(fiyattable);
                db.Fiyat.Add(fiyattable);
                db.SaveChanges();
                return RedirectToAction("FiyatGör", new { id = id });

            }

            return View(fiyattable);
        }

        // GET: Fiyats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiyat fiyat = db.Fiyat.Find(id);
            if (fiyat == null)
            {
                return HttpNotFound();
            }
            return View(fiyat);
        }

        // POST: Fiyats/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fiyat_id, fiyat1")] Fiyat fiyat1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiyat1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fiyat1);
        }

        // GET: Fiyats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiyat fiyat = db.Fiyat.Find(id);
            if (fiyat == null)
            {
                return HttpNotFound();
            }
            return View(fiyat);
        }

        // POST: Fiyats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiyat fiyat = db.Fiyat.Find(id);
            db.Fiyat.Remove(fiyat);
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
