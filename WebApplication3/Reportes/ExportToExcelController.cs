using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Reportes
{
    public class ExportToExcelController : Controller
    {
        SQLModels db = new SQLModels();

        // GET: ExportToExcel
        public ActionResult ExportToExcel()
        {
            var datos = db.clientes.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");
             


                var stream = new MemoryStream();
                package.SaveAs(stream);


                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Reporte.xlsx");
                Response.BinaryWrite(stream.ToArray());
                Response.End();

            }


            return RedirectToAction("Index");


        }
    }
}