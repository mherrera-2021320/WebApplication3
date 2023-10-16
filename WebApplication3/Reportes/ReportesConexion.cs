using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Reportes
{
    public class ReportesConexion
    {
        public static CrystalDecisions.Shared.ConnectionInfo GetConnection()
        {

            CrystalDecisions.Shared.ConnectionInfo infocon = 
                new CrystalDecisions.Shared.ConnectionInfo();

            infocon.ServerName = @".\SQLEXPRESS";
            infocon.DatabaseName = "projectsql";
            infocon.IntegratedSecurity = true;

            return infocon;

            //var strcon = new System.Data.SqlClient.SqlConnectionStringBuilder(

            //    System.Configuration.ConfigurationManager.
            //    ConnectionStrings["SQLModels"].ConnectionString

            //    );
            //CrystalDecisions.Shared.ConnectionInfo infocon =
            //    new CrystalDecisions.Shared.ConnectionInfo();
            //infocon.ServerName = strcon.DataSource;
            //infocon.DatabaseName = strcon.InitialCatalog;
            //infocon.IntegratedSecurity = strcon.IntegratedSecurity;
            //return infocon;



        }





    }
}