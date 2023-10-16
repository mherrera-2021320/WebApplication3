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
    public class ContratoController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Contrato
        [AuthorizeUser(idOperacion: 6)]
        public async Task<ActionResult> Index()
        {
            var contratos = db.contratos.Include(c => c.clientes).Include(c => c.personal).Include(c => c.vehiculos);
            return View(await contratos.ToListAsync());
        }

        // GET: Contrato/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = await db.contratos.FindAsync(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            ViewBag.cliente_contrato = new SelectList(db.clientes, "dpi_clientes", "nombre_completo");
            ViewBag.instructor_asignado = new SelectList(db.personal, "dpi_empleado", "nombre");
            ViewBag.vehiculo_clases = new SelectList(db.vehiculos, "placas", "placas");
            return View();
        }

        // POST: Contrato/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "numero_contrato,numero_seciones,instructor_asignado,cliente_contrato,vehiculo_clases,fecha_inicio,fecha_final,costo_total,estado_contrato")] contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.contratos.Add(contratos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cliente_contrato = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", contratos.cliente_contrato);
            ViewBag.instructor_asignado = new SelectList(db.personal, "dpi_empleado", "nombre", contratos.instructor_asignado);
            ViewBag.vehiculo_clases = new SelectList(db.vehiculos, "placas", "placas", contratos.vehiculo_clases);
            return View(contratos);
        }

        // GET: Contrato/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = await db.contratos.FindAsync(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_contrato = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", contratos.cliente_contrato);
            ViewBag.instructor_asignado = new SelectList(db.personal, "dpi_empleado", "nombre", contratos.instructor_asignado);
            ViewBag.vehiculo_clases = new SelectList(db.vehiculos, "placas", "marca", contratos.vehiculo_clases);
            return View(contratos);
        }

        // POST: Contrato/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "numero_contrato,numero_seciones,instructor_asignado,cliente_contrato,vehiculo_clases,fecha_inicio,fecha_final,costo_total,estado_contrato")] contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_contrato = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", contratos.cliente_contrato);
            ViewBag.instructor_asignado = new SelectList(db.personal, "dpi_empleado", "nombre", contratos.instructor_asignado);
            ViewBag.vehiculo_clases = new SelectList(db.vehiculos, "placas", "marca", contratos.vehiculo_clases);
            return View(contratos);
        }

        // GET: Contrato/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = await db.contratos.FindAsync(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // POST: Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            contratos contratos = await db.contratos.FindAsync(id);
            db.contratos.Remove(contratos);
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
