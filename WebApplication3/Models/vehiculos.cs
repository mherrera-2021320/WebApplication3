
namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class vehiculos
    {
        public vehiculos()
        {
            this.contratos = new HashSet<contratos>();
        }
        

        public string placas { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int año { get; set; }
            
        public System.DateTime fecha_compra { get; set; }
    
        public virtual ICollection<contratos> contratos { get; set; }
    }
}
