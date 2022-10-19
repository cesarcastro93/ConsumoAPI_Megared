using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumoDeAPI_MegaRed.Servicios;
using ConsumoDeAPI_MegaRed.Models.ServiciosPublicosModel;
using Newtonsoft.Json;
using ConsumoDeAPI_MegaRed.Tool;

namespace ConsumoDeAPI_MegaRed.Controllers
{
    public class HomeController : Controller
    {
        ServiciosPublicos serviciosPublicos = new ServiciosPublicos();
        EntregarCambio cambio = new EntregarCambio();
        public static dynamic datapago;
        public static dynamic resultadoCambio;





        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Consultar factura en vipay";

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> About(string convenio, string referencia)
        {

            if (convenio == "" || referencia == "")
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                datapago = await serviciosPublicos.PostConsultarPagoServiciosPublicos(convenio, referencia);
                return RedirectToAction("Contact");

            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(PagarServicioPublicoModel CanalComercio, string InputRecibo)
        {
            CanalComercio.dataPago = new PagarServicioPublicoModel.datapago();
            try
            {

                try
                {
                    CanalComercio.canal = datapago.canal;
                    CanalComercio.comercio_multiproducto = datapago.comercio_multiproducto;
                    CanalComercio.dataPago.codigoConvenio = datapago.DETALLE.codigoConvenio;
                    CanalComercio.dataPago.referencia = datapago.DETALLE.referencia;
                    CanalComercio.dataPago.nombreBanco = datapago.DETALLE.nombreBanco;
                    CanalComercio.dataPago.typePayBill = datapago.DETALLE.typePayBill;
                    CanalComercio.dataPago.codigoProducto = datapago.DETALLE.codigoProducto;
                    CanalComercio.dataPago.amount = datapago.DETALLE.amount;
                    CanalComercio.dataPago.hashEchoData = datapago.DETALLE.hashEchoData;
                    CanalComercio.dataPago.hash = datapago.DETALLE.hash;
                    CanalComercio.dataPago.partialPayment = datapago.DETALLE.partialPayment;
                    CanalComercio.dataPago.operador = datapago.DETALLE.operador;

                    resultadoCambio = cambio.DineroDeCambio(int.Parse(InputRecibo), CanalComercio.dataPago.amount);
                    datapago = await serviciosPublicos.PostPagarServiciosPublicos(CanalComercio);
                    return RedirectToAction("ServicioPagado");

                }
                catch (Exception)
                {
                    ViewBag.Alert = "Verifique el número de convenio o referencia";
                }
            }
            catch (Exception)
            {
                ViewBag.Alert = "Mal";
            }
            return View();
        }

        public ActionResult Contact(PagarServicioPublicoModel.datapago detalleServicio)
        {

            try
            {
                detalleServicio.codigoConvenio = datapago.DETALLE.codigoConvenio;
                detalleServicio.referencia = datapago.DETALLE.referencia;
                detalleServicio.nombreBanco = datapago.DETALLE.nombreBanco;
                detalleServicio.typePayBill = datapago.DETALLE.typePayBill;
                detalleServicio.codigoProducto = datapago.DETALLE.codigoProducto;
                detalleServicio.amount = datapago.DETALLE.amount;
                detalleServicio.hashEchoData = datapago.DETALLE.hashEchoData;
                detalleServicio.hash = datapago.DETALLE.hash;
                detalleServicio.partialPayment = datapago.DETALLE.partialPayment;
                detalleServicio.operador = datapago.DETALLE.operador;
            }
            catch (Exception)
            {
                ViewBag.Alert = "Verifique número de convenio o referencia";
            }
            return View(detalleServicio);
        }

        public ActionResult ServicioPagado(RespuestaServiciosPublicos.detalle respuestaServicios)
        {

            try
            {
                respuestaServicios.ID_TRANSACCION = datapago.DETALLE.ID_TRANSACCION;
                respuestaServicios.COD_AUTORIZACION = datapago.DETALLE.COD_AUTORIZACION;
                respuestaServicios.COD_APROBACION = datapago.DETALLE.COD_APROBACION;
                respuestaServicios.DETALLE = datapago.DETALLE.DETALLE;
                respuestaServicios.Cambio = resultadoCambio;

            }
            catch (Exception)
            {
                ViewBag.Alert = "Verifique número de convenio o referencia";
            }
            return View(respuestaServicios);
        }

        public ActionResult AlistarImpresion() 
        {
            
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Print()
        {
            var Impresion = new Rotativa.ViewAsPdf("AlistarImpresion");
            Impresion.PageSize = Rotativa.Options.Size.A3;
            Impresion.PageWidth = 297;
            Impresion.PageHeight = 420;
            Impresion.PageMargins = new Rotativa.Options.Margins(0,0,0,0);
            return Impresion;
        }

    }
}