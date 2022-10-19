using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumoDeAPI_MegaRed.Models.RecargasModel;
using ConsumoDeAPI_MegaRed.Servicios;
using ConsumoDeAPI_MegaRed.Tool;

namespace ConsumoDeAPI_MegaRed.Controllers
{
    public class RecargasController : Controller
    {
        RecargasServices recargasServices = new RecargasServices();
        EntregarCambio entregarCambio = new EntregarCambio();
        public static dynamic DetalleRecarga;
        public static dynamic cambio;
        // GET: Recargas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VenderRecarga()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VenderRecarga(int producto, string destinatario, int valor,int InputRecibo)
        {
            if (producto == 0 || destinatario == "" || valor == 0)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                cambio = entregarCambio.DineroDeCambio(InputRecibo,valor);
                DetalleRecarga = await recargasServices.PostEnviarRecarga(producto, destinatario, valor);
                return RedirectToAction("DetalleRecargaVendida");
            }

            return View();
        }

        public ActionResult DetalleRecargaVendida(RespuestaRecargasModel respuestaRecargas)
        {
            try
            {
                respuestaRecargas.ID_TRANSACCION = DetalleRecarga.ID_TRANSACCION;
                respuestaRecargas.COD_APROBACION = DetalleRecarga.COD_APROBACION;
                respuestaRecargas.COD_AUTORIZACION = DetalleRecarga.COD_AUTORIZACION;
                respuestaRecargas.DETALLE = DetalleRecarga.DETALLE;
                respuestaRecargas.Cambio= cambio;
            }
            catch (Exception)
            {

                ViewBag.Alert = "Verifique número de convenio o referencia";
            }

            return View(respuestaRecargas);
        }

        
        public ActionResult ConsultarRecarga()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConsultarRecarga(string InputNumeroCelular, string InputIdTrx)
        {
            var respuesta = await recargasServices.ConsultarRecargas(InputNumeroCelular, InputIdTrx);
            return View();
        }
    }
}