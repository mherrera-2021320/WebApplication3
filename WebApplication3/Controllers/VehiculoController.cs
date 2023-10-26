using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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



        SQLModels DBS = new SQLModels();

        public ActionResult ReporteSimple()
        {

            return View(DBS.vehiculos.ToList());
        }

        public ActionResult DescargarRP()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reportes"), "VehiculoReporte.rpt"));

            // Conecto los datos 
            report.SetDataSource(DBS.clientes.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            //Cadena con los datos y el Archivo a reportar/Impriminr
            try
            {
                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "ListaVehiculos.pdf");
            }
            catch (Exception ex)
            {
                // Puedes registrar la excepción o devolver un mensaje de error personalizado
                return Content($"Error: {ex.Message}");
            }

        }

        public ActionResult ExportToExcel()
        {
            var data = DBS.vehiculos.ToList(); // Reemplaza YourTable con el nombre de tu tabla en la base de datos.

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Vehiculos");

                // Llena la hoja de Excel con los datos de la base de datos.
                worksheet.Cells["A1"].LoadFromCollection(data, true);

                // Configura el tipo de contenido y el nombre del archivo.
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=ReporteData.xlsx");

                // Escribe el archivo Excel en la respuesta de la solicitud.
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }

            return View();
        }




        public ActionResult VistaPreviaRP()
        {
            // Lógica para generar el informe con Crystal Reports
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reportes"), "VehiculoReporte.rpt")); ; // Reemplaza la ruta y nombre del informe

            // Configurar los datos del informe (reemplaza "Model" con tus datos reales)
            reportDocument.SetDataSource(DBS.clientes.ToList()); ;

            // Configurar el formato de exportación (PDF en este caso)
            Stream stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            ContentResult contentResult = new ContentResult
            {
                Content = Convert.ToBase64String(ReadStream(stream)),
                ContentType = "application/pdf"
            };

            // Devolver el archivo PDF al navegador
            return PartialView("_MostrarPDF", contentResult);
        }

        private byte[] ReadStream(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
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
