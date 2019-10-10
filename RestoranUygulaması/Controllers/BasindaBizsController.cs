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
    public class BasindaBizsController : Controller
    {
        private Model1 db = new Model1();

        // GET: BasindaBizs
        public ActionResult Index()
        {
            return View(db.BasindaBiz.ToList());
        }
        
        public ActionResult BasinGör()
        {
            if (Session["kul_ad"] == null) {
                return Redirect("Home");
            }
            string kisimizAd = Session["kul_ad"].ToString();
            var kisi = db.Kullanici.Where(i => i.kul_eposta == kisimizAd).SingleOrDefault();
            var basin = kisi.BasindaBiz.ToList();
            if (basin == null)
            {
                return RedirectToAction("Create");
            }
            return View(basin);
        }
        // GET: BasindaBizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasindaBiz basindaBiz = db.BasindaBiz.Find(id);
            if (basindaBiz == null)
            {
                return HttpNotFound();
            }
            return View(basindaBiz);
        }

        // GET: BasindaBizs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasindaBizs/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "basin_id,basindan")] BasindaBiz basindaBiz)
        {
            if (ModelState.IsValid)
            {
                string kisimizAd = Session["kul_ad"].ToString();
                var kisi = db.Kullanici.Where(i => i.kul_eposta == kisimizAd).SingleOrDefault();
                kisi.BasindaBiz.Add(basindaBiz);
                basindaBiz.Kullanici.Add(kisi);
                basindaBiz.basin_id += 1;

                db.BasindaBiz.Add(basindaBiz);
                db.SaveChanges();
                return RedirectToAction("BasinGör");
            }

            

            return View(basindaBiz);
        }

        // GET: BasindaBizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasindaBiz basindaBiz = db.BasindaBiz.Find(id);
            if (basindaBiz == null)
            {
                return HttpNotFound();
            }
            return View(basindaBiz);
        }

        // POST: BasindaBizs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "basin_id,basindan")] BasindaBiz basindaBiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basindaBiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basindaBiz);
        }

        // GET: BasindaBizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasindaBiz basindaBiz = db.BasindaBiz.Find(id);
            if (basindaBiz == null)
            {
                return HttpNotFound();
            }
            return View(basindaBiz);
        }

        // POST: BasindaBizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasindaBiz basindaBiz = db.BasindaBiz.Find(id);
            db.BasindaBiz.Remove(basindaBiz);
            
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
