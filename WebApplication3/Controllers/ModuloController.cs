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
    public class ModuloController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Modulo
        [AuthorizeUser(idOperacion: 10)]
        public async Task<ActionResult> Index()
        {
            return View(await db.modulo.ToListAsync());
        }

        // GET: Modulo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = await db.modulo.FindAsync(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Modulo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "codigo,name")] modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.modulo.Add(modulo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(modulo);
        }

        // GET: Modulo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = await db.modulo.FindAsync(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "codigo,name")] modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modulo);
        }

        // GET: Modulo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modulo modulo = await db.modulo.FindAsync(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            modulo modulo = await db.modulo.FindAsync(id);
            db.modulo.Remove(modulo);
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
