using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass) 
        {
            try
            {
                using (Models.SQLModels db = new Models.SQLModels() )
                {
                    var oUser = (from d in db.usuario
                                where d.username.Trim() == User && d.password == Pass.Trim()
                                select d).FirstOrDefault();

                    if ( oUser == null ) 
                    {
                        ViewBag.Error = "Usuario o Contraseña Invalida";
                            return View();
                    }

                    Session["User"] = oUser;    

                }
                    

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                ViewBag.Error = "Error";
                return View();
            }    
            
        
        }


    } 




}