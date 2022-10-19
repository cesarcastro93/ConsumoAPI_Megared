using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.RecargasModel
{
    public class RecargaConsultarModel
    {
        public string FECHA { get; set; }
        public int NUMERO { get; set; }
        public string OPERADOR { get; set; }
        public int VALOR { get; set; }
        public string COD_APROVACION { get; set; }
        public string ESTADO { get; set; }
    }
}