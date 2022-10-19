using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel
{
    public class VentaRequestModel
    {
        public int canal { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string  producto { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string bolsa { get; set; }
        public int  valor { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string destinatario { get; set; }
        public bool comercio_multiproducto { get; set; }

        public Extra extra;

        public class Extra 
        {
            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string  tipoDocumento { get; set; }


            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string name { get; set; }


            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string  last_name { get; set; }


            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string idConfirmacion { get; set; }


            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string fechaTransaccion { get; set; }



            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string pay { get; set; }


            public Extra(string tipoDocumento, string name, string last_name, string pay ,string idConfirmacion,string fechaTransaccion)
            {
                this.tipoDocumento = tipoDocumento;
                this.name = name;
                this.last_name = last_name;
                this.idConfirmacion=idConfirmacion;
                this.fechaTransaccion= fechaTransaccion;
                this.pay = pay;
            }
        }
    }
}