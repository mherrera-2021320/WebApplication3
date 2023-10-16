using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.ViewModels
{
    public class SearchViewModel
    {
        public string BuscarString { get; set; }
        public int? BuscarInt { get; set; }
        public DateTime? BuscarFecha { get; set; }

    }
}