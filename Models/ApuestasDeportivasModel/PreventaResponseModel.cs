using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel
{
    public class PreventaResponseModel
    {
        public int status { get; set; }

        public Detalle DETALLE;
        public class Detalle
        {
            public string codigo_respuesta { get; set; }
            public string detalle { get; set; }
            public string idConfirmacion { get; set; }
            public string fechaTransaccion { get; set; }
            public string documento { get; set; }
            public string nombre { get; set; }


            public string payId { get; set; }


            public string idTransaccion { get; set; }
            public string fechaPlana { get; set; }
        }
    }
}