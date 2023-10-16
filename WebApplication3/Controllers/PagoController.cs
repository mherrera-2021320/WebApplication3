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
    public class PagoController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Pago
        [AuthorizeUser(idOperacion: 8)]
        public ActionResult Index(int pg=1)
        {
            var pagos = db.pagos.Include(p => p.clientes).Include(p => p.contratos).Include(p => p.tipo_pago);
            return View(pagos.ToList());
        }

        // GET: Pago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            ViewBag.cliente_pago = new SelectList(db.clientes, "dpi_clientes", "nombre_completo");
            ViewBag.contrato_servicio = new SelectList(db.contratos, "numero_contrato", "instructor_asignado");
            ViewBag.nombre_pago = new SelectList(db.tipo_pago, "id_tipo", "nombre");
            return View();
        }

        // POST: Pago/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pagos,fecha_pago,nombre_pago,cliente_pago,monto,contrato_servicio")] pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cliente_pago = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", pagos.cliente_pago);
            ViewBag.contrato_servicio = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", pagos.contrato_servicio);
            ViewBag.nombre_pago = new SelectList(db.tipo_pago, "id_tipo", "nombre", pagos.nombre_pago);
            return View(pagos);
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_pago = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", pagos.cliente_pago);
            ViewBag.contrato_servicio = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", pagos.contrato_servicio);
            ViewBag.nombre_pago = new SelectList(db.tipo_pago, "id_tipo", "nombre", pagos.nombre_pago);
            return View(pagos);
        }

        // POST: Pago/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pagos,fecha_pago,nombre_pago,cliente_pago,monto,contrato_servicio")] pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_pago = new SelectList(db.clientes, "dpi_clientes", "nombre_completo", pagos.cliente_pago);
            ViewBag.contrato_servicio = new SelectList(db.contratos, "numero_contrato", "instructor_asignado", pagos.contrato_servicio);
            ViewBag.nombre_pago = new SelectList(db.tipo_pago, "id_tipo", "nombre", pagos.nombre_pago);
            return View(pagos);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagos pagos = db.pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pagos pagos = db.pagos.Find(id);
            db.pagos.Remove(pagos);
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
