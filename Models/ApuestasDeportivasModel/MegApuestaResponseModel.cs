using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel
{
    public class MegApuestaResponseModel
    {
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string ID_TRANSACCION { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string COD_APROBACION { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string COD_AUTORIZACION { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string DETALLE { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string COMERCIO_MULTIPRODUCTO { get; set; }

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public int Cambio { get; set; }

    }
}