using System;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Controllers;
using WebApplication3.Models;

namespace WebApplication3.Filters
{
    public class VerificacionSession : ActionFilterAttribute
    {

        private usuario oUsuario;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            try
            {
            base.OnActionExecuted(filterContext);
                 
                oUsuario = (usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }
        }


    }
}