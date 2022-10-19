using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumoDeAPI_MegaRed.Models.RecargasModel
{
    public class RecargaVentaModel
    {
        [Required]
        public int producto { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string destinatario { get; set; }
        [Required]
        public int valor { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string bolsa { get; set; }
        [StringLength(50, ErrorMessage = "Logitud máxima 50")]
        public string referencia { get; set; }
    }
}