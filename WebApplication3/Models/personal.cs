//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class personal
    {

        public personal()
        {
            this.contratos = new HashSet<contratos>();
        }
    
        public string dpi_empleado { get; set; }
        public int direccion_personal { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public System.DateTime fecha_nacimiento { get; set; }
        public System.DateTime feche_contratacion { get; set; }
        public System.DateTime fecha_baja { get; set; }
        public int telefono { get; set; }
        public string correo { get; set; }
        public bool estado { get; set; }
        public Nullable<int> puesto_personal { get; set; }
        public Nullable<int> usuario_personal { get; set; }
    
        public virtual ICollection<contratos> contratos { get; set; }
        public virtual direcciones direcciones { get; set; }
        public virtual puesto puesto { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
