using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel
{
    public class MegaApuestaModel
    {
        [StringLength(100, ErrorMessage = "Logitud máxima 100")]
        public string NombreApostador { get; set; }


        public int canal { get; set; }


        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string  producto { get; set; }


        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string bolsa { get; set; }


        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string valor { get; set; }


        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string  destinatario { get; set; }



    }
}