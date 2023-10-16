using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Filters;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DireccionController : Controller
    {
        // GET: Direccion
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Index( string orden)
        {
            using (SQLModels context = new SQLModels())
            {
                var direcciones = context.direcciones.ToList();

                switch (orden)
                {
                    case "codigo_asc":
                        direcciones = direcciones.OrderBy(v => v.codigo).ToList();
                        break;
                    case "codigo_desc":
                        direcciones = direcciones.OrderByDescending(v => v.codigo).ToList();
                        break;
                    case "numero_asc":
                        direcciones = direcciones.OrderBy(v => v.numero_casa).ToList();
                        break;
                    case "numero_desc":
                        direcciones = direcciones.OrderByDescending(v => v.numero_casa).ToList();
                        break;
                    case "calle_asc":
                        direcciones = direcciones.OrderBy(v => v.calle_direccion).ToList();
                        break;
                    case "calle_desc":
                        direcciones = direcciones.OrderByDescending(v => v.calle_direccion).ToList();
                        break;
                    case "ciudad_asc":
                        direcciones = direcciones.OrderBy(v => v.ciudad).ToList();
                        break;
                    case "ciudad_desc":
                        direcciones = direcciones.OrderByDescending(v => v.ciudad).ToList();
                        break;
                    case "postal_asc":
                        direcciones = direcciones.OrderBy(v => v.codigo_postal).ToList();
                        break;
                    case "postal_desc":
                        direcciones = direcciones.OrderByDescending(v => v.codigo_postal).ToList();
                        break;
                    case "colonia_asc":
                        direcciones = direcciones.OrderBy(v => v.colonia).ToList();
                        break;
                    case "colonia_desc":
                        direcciones = direcciones.OrderByDescending(v => v.colonia).ToList();
                        break;
                    case "departamento_asc":
                        direcciones = direcciones.OrderBy(v => v.departamento).ToList();
                        break;
                    case "departamento_desc":
                        direcciones = direcciones.OrderByDescending(v => v.departamento).ToList();
                        break;
                    case "municipio_asc":
                        direcciones = direcciones.OrderBy(v => v.municipio).ToList();
                        break;
                    case "municipio_desc":
                        direcciones = direcciones.OrderByDescending(v => v.municipio).ToList();
                        break;
                    case "estado_asc":
                        direcciones = direcciones.OrderBy(v => v.estado_actual).ToList();
                        break;
                    case "estado_desc":
                        direcciones = direcciones.OrderByDescending(v => v.estado_actual).ToList();
                        break;
                    // Agrega más casos para otras columnas si es necesario
                    default:
                        // Orden por defecto, puedes cambiar esto según tus necesidades
                        direcciones = direcciones.OrderBy(v => v.codigo).ToList();
                        break;
                }

                return View(direcciones);
            }
        }

        // GET: Direccion/Details/5
        public ActionResult Details(int id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.direcciones.Where(x => x.codigo == id).FirstOrDefault());
            }
        }

        // GET: Direccion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Direccion/Create
        [HttpPost]
        public ActionResult Create(direcciones direcciones)
        {
            try
            {
                // TODO: Add insert logic here
                using (SQLModels context = new SQLModels())
                {
                    context.direcciones.Add(direcciones);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(int id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.direcciones.Where(x => x.codigo == id).FirstOrDefault());

            }
        }

        // POST: Direccion/Edit/5
        [HttpPost]
        public ActionResult Edit( direcciones direcciones)
        {
            try
            {
                // TODO: Add update logic here
                using (SQLModels context = new SQLModels())
                {
                    context.Entry(direcciones).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Direccion/Delete/5
        public ActionResult Delete(int id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.direcciones.Where(x => x.codigo == id).FirstOrDefault());
            }
        }

        // POST: Direccion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (SQLModels context = new SQLModels())
                {
                    direcciones direcciones = context.direcciones.Where(x => x.codigo == id).FirstOrDefault();
                    context.direcciones.Remove(direcciones);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        SQLModels DBS = new SQLModels();

        public ActionResult ReporteSimple()
        {

            return View(DBS.direcciones.ToList());
        }

        public ActionResult DescargarRPVehiculos()
        {

            // Instalacion a traves del Objeto }
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reportes"), "DireccionReporte.rpt"));

            // Conecto los datos 
            report.SetDataSource(DBS.direcciones.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            //Cadena con los datos y el Archivo a reportar/Impriminr
            try
            {
                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "ListaDirecciones.pdf");
            }
            catch (Exception ex)
            {
                // Puedes registrar la excepción o devolver un mensaje de error personalizado
                return Content($"Error: {ex.Message}");
            }

        }


        public ActionResult VistaPreviaRPVehiculos()
        {
            // Lógica para generar el informe con Crystal Reports
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reportes"), "DireccionReporte.rpt")); ; // Reemplaza la ruta y nombre del informe

            // Configurar los datos del informe (reemplaza "Model" con tus datos reales)
            reportDocument.SetDataSource(DBS.direcciones.ToList()); ;

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

    }
}

