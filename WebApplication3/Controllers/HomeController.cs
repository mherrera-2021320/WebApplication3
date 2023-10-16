using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{

    public class HomeController : Controller
    {

        [AuthorizeUser(idOperacion:1)]
        public ActionResult Index()
        {
                return View();
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}