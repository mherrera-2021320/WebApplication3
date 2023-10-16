using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Filters;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class ClienteController : Controller


    {
        // GET: Cliente
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Index(string orden, string SearchString, int? i, int? pageSize)
        {

            int pageNumber = (i ?? 1);

            using (SQLModels context = new SQLModels())
            {
                var clientes = context.clientes.AsQueryable();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    clientes = clientes.Where(v =>
                        v.dpi_clientes.Contains(SearchString) ||
                        v.nombre_completo.Contains(SearchString) ||
                        v.direccion.Contains(SearchString) ||
                        v.correo.Contains(SearchString) 
                        );
                }

                switch (orden)
                {
                    case "dpi_asc":
                        clientes = clientes.OrderBy(v => v.dpi_clientes);
                        break;
                    case "dpi_desc":
                        clientes = clientes.OrderByDescending(v => v.dpi_clientes);
                        break;
                    case "nombre_asc":
                        clientes = clientes.OrderBy(v => v.nombre_completo);
                        break;
                    case "nombre_desc":
                        clientes = clientes.OrderByDescending(v => v.nombre_completo);
                        break;
                    case "direccion_asc":
                        clientes = clientes.OrderBy(v => v.direccion);
                        break;
                    case "direccion_desc":
                        clientes = clientes.OrderByDescending(v => v.direccion);
                        break;
                    case "fecha_asc":
                        clientes = clientes.OrderBy(v => v.fecha_nacimiento);
                        break;
                    case "fecha_desc":
                        clientes = clientes.OrderByDescending(v => v.fecha_nacimiento);
                        break;
                    case "correo_asc":
                        clientes = clientes.OrderBy(v => v.correo);
                        break;
                    case "correo_desc":
                        clientes = clientes.OrderByDescending(v => v.correo);
                        break;
                    case "telefono_asc":
                        clientes = clientes.OrderBy(v => v.telefono);
                        break;
                    case "telefono_desc":
                        clientes = clientes.OrderByDescending(v => v.telefono);
                        break;
                    case "estado_asc":
                        clientes = clientes.OrderBy(v => v.estado_actual);
                        break;
                    case "estado_desc":
                        clientes = clientes.OrderByDescending(v => v.estado_actual);
                        break;
                    // Agrega más casos para otras columnas si es necesario
                    default:
                        // Orden por defecto, puedes cambiar esto según tus necesidades
                        clientes = clientes.OrderBy(v => v.dpi_clientes);
                        break;
                }

                int defaultPageSize = 10;
                int actualPageSize = pageSize ?? defaultPageSize;

                var clientesPaginados = clientes.ToPagedList(pageNumber, actualPageSize);

                ViewBag.PageSize = actualPageSize;

                return View(clientesPaginados);
            }   
        }

        // GET: Cliente/Details/5
        [AuthorizeUser(idOperacion: 40)]
        public ActionResult Details(string id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.clientes.Where(x => x.dpi_clientes == id).FirstOrDefault());
            }
        }

        // GET: Cliente/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create

        [HttpPost]
        public ActionResult Create(clientes clientes)
        {
            try
            {
                // TODO: Add insert logic here

                using (SQLModels context = new SQLModels())
                {
                    context.clientes.Add(clientes);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5

        public ActionResult Edit(string id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.clientes.Where(x => x.dpi_clientes == id).FirstOrDefault());

            }
        }

        // POST: Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, clientes clientes)
        {
            try
            {
                // TODO: Add update logic here
                using (SQLModels context = new SQLModels())
                {
                    context.Entry(clientes).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5

        public ActionResult Delete(string id)
        {
            using (SQLModels context = new SQLModels())
            {
                return View(context.clientes.Where(x => x.dpi_clientes == id).FirstOrDefault());
            }
        }

        // POST: Cliente/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (SQLModels context = new SQLModels())
                {
                    clientes clientes = context.clientes.Where(x => x.dpi_clientes == id).FirstOrDefault();
                    context.clientes.Remove(clientes);
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

            return View(DBS.clases.ToList());
        }

        public ActionResult DescargarRP()
        {


            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reportes"), "ClienteReporte.rpt"));

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
                return File(stream, "application/pdf", "ListaClientes.pdf");
            }
            catch (Exception ex)
            {
                // Puedes registrar la excepción o devolver un mensaje de error personalizado
                return Content($"Error: {ex.Message}");
            }

        }

        public ActionResult DescargarExcel()
        {
            ReportDocument report = new ReportDocument();

            report.Load(Path.Combine(Server.MapPath("~/Reportes"), "ClienteReporte.rpt"));

            report.SetDataSource(DBS.vehiculos.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {

                Stream stream = report.ExportToStream(ExportFormatType.Excel);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/vnd.ms-excel", "empleadoRpt.xls");

            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }

        }



        public ActionResult VistaPreviaRP()
        {
            // Lógica para generar el informe con Crystal Reports
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reportes"), "ClienteReporte.rpt")); ; // Reemplaza la ruta y nombre del informe

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

    }
}
