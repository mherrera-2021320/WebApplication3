using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{
    public class RolOperacionController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: RolOperacion
        [AuthorizeUser(idOperacion: 13)]
        public async Task<ActionResult> Index()
        {
            var rol_operacion = db.rol_operacion.Include(r => r.operaciones).Include(r => r.rol);
            return View(await rol_operacion.ToListAsync());
        }

        // GET: RolOperacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rol_operacion rol_operacion = await db.rol_operacion.FindAsync(id);
            if (rol_operacion == null)
            {
                return HttpNotFound();
            }
            return View(rol_operacion);
        }

        // GET: RolOperacion/Create
        public ActionResult Create()
        {
            ViewBag.operacionId = new SelectList(db.operaciones, "id", "nombre");
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre");
            return View();
        }

        // POST: RolOperacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "codigo,rolId,operacionId")] rol_operacion rol_operacion)
        {
            if (ModelState.IsValid)
            {
                db.rol_operacion.Add(rol_operacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.operacionId = new SelectList(db.operaciones, "id", "nombre", rol_operacion.operacionId);
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", rol_operacion.rolId);
            return View(rol_operacion);
        }

        // GET: RolOperacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rol_operacion rol_operacion = await db.rol_operacion.FindAsync(id);
            if (rol_operacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.operacionId = new SelectList(db.operaciones, "id", "nombre", rol_operacion.operacionId);
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", rol_operacion.rolId);
            return View(rol_operacion);
        }

        // POST: RolOperacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "codigo,rolId,operacionId")] rol_operacion rol_operacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol_operacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.operacionId = new SelectList(db.operaciones, "id", "nombre", rol_operacion.operacionId);
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", rol_operacion.rolId);
            return View(rol_operacion);
        }

        // GET: RolOperacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rol_operacion rol_operacion = await db.rol_operacion.FindAsync(id);
            if (rol_operacion == null)
            {
                return HttpNotFound();
            }
            return View(rol_operacion);
        }

        // POST: RolOperacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rol_operacion rol_operacion = await db.rol_operacion.FindAsync(id);
            db.rol_operacion.Remove(rol_operacion);
            await db.SaveChangesAsync();
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
