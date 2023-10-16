using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Filters;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PuestosController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Puestos
        [AuthorizeUser(idOperacion: 14)]
        public ActionResult Index()
        {
            return View(db.puesto.ToList());
        }

        // GET: Puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            puesto puesto = db.puesto.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // GET: Puestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Puestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,nombre,descripcion")] puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.puesto.Add(puesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(puesto);
        }

        // GET: Puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            puesto puesto = db.puesto.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // POST: Puestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,descripcion")] puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(puesto);
        }

        // GET: Puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            puesto puesto = db.puesto.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // POST: Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            puesto puesto = db.puesto.Find(id);
            db.puesto.Remove(puesto);
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
