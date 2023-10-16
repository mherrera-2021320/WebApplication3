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
    public class ClaseController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Clase

        [AuthorizeUser(idOperacion: 7)]
        public async Task<ActionResult> Index()
        {
            var clases = db.clases.Include(c => c.clientes).Include(c => c.contratos);
            return View(await clases.ToListAsync());
        }

        // GET: Clase/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clases clases = await db.clases.FindAsync(id);
            if (clases == null)
            {
                return HttpNotFound();
            }
            return View(clases);
        }

        // GET: Clase/Create
        public ActionResult Create()
        {
            ViewBag.cliente_clases = new SelectList(db.clientes, "dpi_clientes", "nombre_completo");
            ViewBag.contrato_clases = new SelectList(db.contratos, "numero_contrato", "instructor_asignado");
            return View();
        }

        // POST: Clase/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_clases,fecha,cliente_clases,hora_inicio,hora_final,contrato_clases")] clases clases)
        {
            if (ModelState.IsValid)
            {
                db.clases.Add(clases);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cliente_clases = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", clases.cliente_clases);
            ViewBag.contrato_clases = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", clases.contrato_clases);
            return View(clases);
        }

        // GET: Clase/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clases clases = await db.clases.FindAsync(id);
            if (clases == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_clases = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", clases.cliente_clases);
            ViewBag.contrato_clases = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", clases.contrato_clases);
            return View(clases);
        }

        // POST: Clase/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_clases,fecha,cliente_clases,hora_inicio,hora_final,contrato_clases")] clases clases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clases).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_clases = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", clases.cliente_clases);
            ViewBag.contrato_clases = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", clases.contrato_clases);
            return View(clases);
        }

        // GET: Clase/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clases clases = await db.clases.FindAsync(id);
            if (clases == null)
            {
                return HttpNotFound();
            }
            return View(clases);
        }

        // POST: Clase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            clases clases = await db.clases.FindAsync(id);
            db.clases.Remove(clases);
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
