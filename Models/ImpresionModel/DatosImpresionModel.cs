using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ImpresionModel
{
    public class DatosImpresionModel
    {
        public string  IdTransaccion { get; set; }
        public string  CodigoAprobacion { get; set; }
        public string  CodigoAutorizacion { get; set; }
        public string  DetalleTransaccion { get; set; }
        public int  Cambio { get; set; }
        public int  valor { get; set; }
        public DateTime Fecha { get; set; }

    }
}