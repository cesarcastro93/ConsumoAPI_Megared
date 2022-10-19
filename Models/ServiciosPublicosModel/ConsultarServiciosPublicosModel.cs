using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ServiciosPublicosModel
{
    public class ConsultarServiciosPublicosModel
    {
        public string barcode { get; set; }
        public string convenio { get; set; }
        public string referencia { get; set; }
        public string metodo { get; set; }
    }
}