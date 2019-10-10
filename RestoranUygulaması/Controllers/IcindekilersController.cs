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
    public class IcindekilersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Icindekilers
        public ActionResult Index()
        {
            return View(db.Icindekiler.ToList());
        }

        public ActionResult IcindekilerGör(int id)
        {
            var yemic = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
            return View(yemic.Icindekiler);
        }

        // GET: Icindekilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var icindekiler = db.Icindekiler.Where(x => x.icin_id == id).SingleOrDefault();
            if (icindekiler == null)
            {
                return HttpNotFound();
            }
            return View(icindekiler);
        }

        // GET: Icindekilers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Icindekilers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "icin_id,malzemeler")] Icindekiler icindekiler, int id)
        {
            if (ModelState.IsValid)
            {
                YemIcin kh = new YemIcin();
                kh.yem_id = id;
                kh.icin_id = icindekiler.icin_id;
                var yemic = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
                yemic.Icindekiler.Add(icindekiler);
                db.Icindekiler.Add(icindekiler);
                db.SaveChanges();
                return RedirectToAction("IcindekilerGör", new { id = id });

            }

            return View(icindekiler);
        }

        // GET: Icindekilers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Icindekiler icindekiler = db.Icindekiler.Find(id);
            if (icindekiler == null)
            {
                return HttpNotFound();
            }
            return View(icindekiler);
        }

        // POST: Icindekilers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "icin_id,malzemeler")] Icindekiler icindekiler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(icindekiler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(icindekiler);
        }

        // GET: Icindekilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Icindekiler icindekiler = db.Icindekiler.Find(id);
            if (icindekiler == null)
            {
                return HttpNotFound();
            }
            return View(icindekiler);
        }

        // POST: Icindekilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Icindekiler icindekiler = db.Icindekiler.Find(id);
            db.Icindekiler.Remove(icindekiler);
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
