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
    public class PersonalController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Personal
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            var personal = db.personal.Include(p => p.direcciones).Include(p => p.puesto).Include(p => p.usuario);
            return View(personal.ToList());
        }

        // GET: Personal/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal personal = db.personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personal/Create
        public ActionResult Create()
        {
            ViewBag.direccion_personal = new SelectList(db.direcciones, "codigo", "calle_direccion");
            ViewBag.puesto_personal = new SelectList(db.puesto, "codigo", "nombre");
            ViewBag.usuario_personal = new SelectList(db.usuario, "codigo", "nombre_completo");
            return View();
        }

        // POST: Personal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dpi_empleado,direccion_personal,nombre,apellido,fecha_nacimiento,feche_contratacion,fecha_baja,telefono,correo,estado,puesto_personal,usuario_personal")] personal personal)
        {
            if (ModelState.IsValid)
            {
                db.personal.Add(personal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.direccion_personal = new SelectList(db.direcciones, "codigo", "calle_direccion", personal.direccion_personal);
            ViewBag.puesto_personal = new SelectList(db.puesto, "codigo", "nombre", personal.puesto_personal);
            ViewBag.usuario_personal = new SelectList(db.usuario, "codigo", "nombre_completo", personal.usuario_personal);
            return View(personal);
        }

        // GET: Personal/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal personal = db.personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            ViewBag.direccion_personal = new SelectList(db.direcciones, "codigo", "calle_direccion", personal.direccion_personal);
            ViewBag.puesto_personal = new SelectList(db.puesto, "codigo", "nombre", personal.puesto_personal);
            ViewBag.usuario_personal = new SelectList(db.usuario, "codigo", "nombre_completo", personal.usuario_personal);
            return View(personal);
        }

        // POST: Personal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dpi_empleado,direccion_personal,nombre,apellido,fecha_nacimiento,feche_contratacion,fecha_baja,telefono,correo,estado,puesto_personal,usuario_personal")] personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.direccion_personal = new SelectList(db.direcciones, "codigo", "calle_direccion", personal.direccion_personal);
            ViewBag.puesto_personal = new SelectList(db.puesto, "codigo", "nombre", personal.puesto_personal);
            ViewBag.usuario_personal = new SelectList(db.usuario, "codigo", "nombre_completo", personal.usuario_personal);
            return View(personal);
        }

        // GET: Personal/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal personal = db.personal.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            personal personal = db.personal.Find(id);
            db.personal.Remove(personal);
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
