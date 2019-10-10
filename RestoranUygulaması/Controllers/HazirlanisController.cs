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
    public class HazirlanisController : Controller
    {
        private Model1 db = new Model1();

        // GET: Hazirlanis
        public ActionResult Index()
        {
            return View(db.Hazirlanis.ToList());
        }

        public ActionResult HazirlanisGör(int id)
        {
            var hazir = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
            return View(hazir.Hazirlanis);
        }

        // GET: Hazirlanis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hazirlanis = db.Hazirlanis.Where(x => x.hazir_id == id).SingleOrDefault();
            if (hazirlanis == null)
            {
                return HttpNotFound();
            }
            return View(hazirlanis);
        }

        // GET: Hazirlanis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hazirlanis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hazir_id,tarif")] Hazirlanis hazirlanis, int id)
        {
            if (ModelState.IsValid)
            {
                YemHaz yh = new YemHaz();
                yh.yem_id = id;
                hazirlanis.hazir_id = id;
                var hazir = db.Yemek.Where(i => i.yem_id == id).SingleOrDefault();
                hazir.Hazirlanis.Add(hazirlanis);
                db.Hazirlanis.Add(hazirlanis);
                db.SaveChanges();
                return RedirectToAction("HazirlanisGör", new { id = id });
                
            }

            return View(hazirlanis);
        }

        // GET: Hazirlanis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hazirlanis hazirlanis = db.Hazirlanis.Find(id);
            if (hazirlanis == null)
            {
                return HttpNotFound();
            }
            return View(hazirlanis);
        }

        // POST: Hazirlanis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hazir_id,tarif")] Hazirlanis hazirlanis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hazirlanis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hazirlanis);
        }

        // GET: Hazirlanis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hazirlanis hazirlanis = db.Hazirlanis.Find(id);
            if (hazirlanis == null)
            {
                return HttpNotFound();
            }
            return View(hazirlanis);
        }

        // POST: Hazirlanis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hazirlanis hazirlanis = db.Hazirlanis.Find(id);
            db.Hazirlanis.Remove(hazirlanis);
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
