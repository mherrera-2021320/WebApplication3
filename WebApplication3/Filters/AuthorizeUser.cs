using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Filters
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false )]
    public class AuthorizeUser: AuthorizeAttribute  
    {
        private usuario oUsuario;
        private SQLModels db = new SQLModels();
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization( AuthorizationContext filterContext ) 
        {
            String nombreModulo = "";
            String nombreOperacion = "";

            try
            {
                oUsuario = (usuario)HttpContext.Current.Session["User"];
                var lstMisOperaciones = from m in db.rol_operacion
                                        where m.rolId == oUsuario.rolId
                                        && m.operacionId == idOperacion
                                        select m;
                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.operaciones.Find(idOperacion);
                    int? idModulo = oOperacion.idModulo;
                    nombreOperacion = getNombredeOperacion(idOperacion);
                    nombreModulo = getNombredelModulo(idModulo);
                    nombreOperacion = nombreOperacion.Replace("", "+");
                    nombreModulo = nombreModulo.Replace("", "+");
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion);
                }


            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion);
            }

        
        }

        private string getNombredelModulo(int? idModulo)
        {
            var ope = from op in db.modulo
                      where op.codigo == idModulo
                      select op.name;
            String nombreModulo;
            try
            {
                nombreModulo = ope.First();
            }
            catch (Exception)
            {

                nombreModulo = "";
            }

            return nombreModulo;

        }

        private string getNombredeOperacion(int idOperacion)
        {
            var ope = from op in db.operaciones
                      where op.id == idOperacion
                      select op.nombre;
            String nombreOperacion;
            try
            {
                nombreOperacion = ope.First();
            }
            catch (Exception)
            {

                nombreOperacion = "";
            }

            return nombreOperacion;

        }
    }
}