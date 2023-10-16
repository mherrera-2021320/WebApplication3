using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class VehiculoController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Vehiculo
        public ActionResult Index(string orden, string SearchString, int? i, int? pageSize)
        {

            int pageNumber = (i ?? 1);

            using (SQLModels context = new SQLModels())
            {

                var vehiculos = context.vehiculos.AsQueryable();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    vehiculos = vehiculos.Where(v =>
                        v.placas.Contains(SearchString) ||
                        v.marca.Contains(SearchString) ||
                        v.modelo.Contains(SearchString)
                    );
                }

                switch (orden)
                {
                    case "placas_asc":
                        vehiculos = vehiculos.OrderBy(v => v.placas);
                        break;
                    case "placas_desc":
                        vehiculos = vehiculos.OrderByDescending(v => v.placas);
                        break;
                    case "marca_asc":
                        vehiculos = vehiculos.OrderBy(v => v.marca);
                        break;
                    case "marca_desc":
                        vehiculos = vehiculos.OrderByDescending(v => v.marca);
                        break;
                    case "modelo_asc":
                        vehiculos = vehiculos.OrderBy(v => v.modelo);
                        break;
                    case "modelo_desc":
                        vehiculos = vehiculos.OrderByDescending(v => v.modelo);
                        break;
                    case "año_asc":
                        vehiculos = vehiculos.OrderBy(v => v.año);
                        break;
                    case "año_desc":
                        vehiculos = vehiculos.OrderByDescending(v => v.año);
                        break;
                    case "fecha_asc":
                        vehiculos = vehiculos.OrderBy(v => v.fecha_compra);
                        break;
                    case "fecha_desc":
                        vehiculos = vehiculos.OrderByDescending(v => v.fecha_compra);
                        break;

                    default:
                        vehiculos = vehiculos.OrderBy(v => v.placas);
                        break;
                }

                int defaultPageSize = 10;
                int actualPageSize = pageSize ?? defaultPageSize;

                var vehiculosPaginados = vehiculos.ToPagedList(pageNumber, actualPageSize);

                ViewBag.PageSize = actualPageSize;

                return View(vehiculosPaginados);
            }

        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculos vehiculos = db.vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "placas,marca,modelo,año,fecha_compra")] vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.vehiculos.Add(vehiculos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculos);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculos vehiculos = db.vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "placas,marca,modelo,año,fecha_compra")] vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehiculos);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vehiculos vehiculos = db.vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            vehiculos vehiculos = db.vehiculos.Find(id);
            db.vehiculos.Remove(vehiculos);
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
