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
    
    public partial class contratos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contratos()
        {
            this.clases = new HashSet<clases>();
            this.pagos = new HashSet<pagos>();
        }
    
        public int numero_contrato { get; set; }
        public int numero_seciones { get; set; }
        public string instructor_asignado { get; set; }
        public string cliente_contrato { get; set; }
        public string vehiculo_clases { get; set; }
        public System.DateTime fecha_inicio { get; set; }
        public System.DateTime fecha_final { get; set; }
        public decimal costo_total { get; set; }
        public bool estado_contrato { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clases> clases { get; set; }
        public virtual clientes clientes { get; set; }
        public virtual personal personal { get; set; }
        public virtual vehiculos vehiculos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pagos> pagos { get; set; }
    }
}
