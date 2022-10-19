using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.RecargasModel
{
    public class RespuestaRecargasModel
    {
        public string ID_TRANSACCION { get; set; }
        public string COD_APROBACION { get; set; }
        public string COD_AUTORIZACION { get; set; }
        public string DETALLE { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public int  Cambio { get; set; }
    }
}