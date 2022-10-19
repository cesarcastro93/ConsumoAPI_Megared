using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ServiciosPublicosModel
{
    public class PagarServicioPublicoModel
    {
        public string STATUS { get; set; }
        public int canal { get; set; }
        public bool comercio_multiproducto { get; set; }
        public datapago dataPago;



        public class datapago
        {
            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string codigoConvenio { get; set; }

            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string referencia { get; set; }

            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string nombreBanco { get; set; }

            public int typePayBill { get; set; }

            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string codigoProducto { get; set; }

            [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
            public int amount { get; set; }

            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string hashEchoData { get; set; }

            [StringLength(50, ErrorMessage = "Logitud máxima 50")]
            public string hash { get; set; }

            public int partialPayment { get; set; }

            public int operador { get; set; }





            //public datapago(string codigoConvenio, string referencia, string nombreBanco, int typePayBill, string codigoProducto, int amount, string hashEchoData, string hash, int partialPayment, int operador, int pagoParcial)
            //{
            //    this.codigoConvenio = codigoConvenio;
            //    this.referencia = referencia;
            //    this.nombreBanco = nombreBanco;
            //    this.typePayBill = typePayBill;
            //    this.codigoProducto = codigoProducto;
            //    this.amount = amount;
            //    this.hashEchoData = hashEchoData;
            //    this.hash = hash;
            //    this.partialPayment = partialPayment;
            //    this.operador = operador;
            //    this.pagoParcial = pagoParcial;


            //}
        }
        public string VISTA { get; set; }



    }
}