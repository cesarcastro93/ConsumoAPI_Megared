using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumoDeAPI_MegaRed.Servicios;
using ConsumoDeAPI_MegaRed.Tool;
using ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel;

namespace ConsumoDeAPI_MegaRed.Controllers
{
    public class ApuestasDeportivasController : Controller
    {
        ApuestasDeportivasServices servicesApuestas = new ApuestasDeportivasServices();

        EntregarCambio entregarCambio = new EntregarCambio();

        public static dynamic resultadoCambio;
        public static dynamic datapago;
        public static dynamic responsedata;
        public static dynamic responsedataApuestas;


        public static dynamic RespPreventaMegapuesta;
        public static dynamic ValorApuesta;
        public static dynamic ValorApuestaDeportiva;
        public static dynamic DestinatarioApuesta;
        public static dynamic TipoDoc;
        public static dynamic Nombre;
        public static dynamic Apellido;


        // GET: ApuestasDeportivas
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult PreventaMegApuesta()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PreventaMegApuesta(string destinatario, string valor, MegaApuestaModel megaApuestaModel)
        {
            if (destinatario == "" || valor == "")
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                RespPreventaMegapuesta = await servicesApuestas.PreventaMegaApuestas(megaApuestaModel);
                DestinatarioApuesta = destinatario;
                ValorApuesta = valor;
                return RedirectToAction("VentaMegapuesta");
            }

            return View();
        }


        public ActionResult VentaMegapuesta(MegaApuestaModel megaApuestaModel)
        {
            megaApuestaModel.NombreApostador = RespPreventaMegapuesta;
            megaApuestaModel.destinatario = DestinatarioApuesta;
            megaApuestaModel.valor = ValorApuesta;


            return View(megaApuestaModel);
        }

        [HttpPost]
        public async Task<ActionResult> VentaMegapuesta(MegaApuestaModel megaApuestaModel, string InputRecibo)
        {
            try
            {
                megaApuestaModel.NombreApostador = RespPreventaMegapuesta;
                megaApuestaModel.destinatario = DestinatarioApuesta;
                megaApuestaModel.valor = ValorApuesta;
                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), int.Parse(megaApuestaModel.valor));
                datapago = await servicesApuestas.VentaMegapuesta(megaApuestaModel);
                return RedirectToAction("VentaApuestaExitosa");
            }
            catch (Exception)
            {
                return View();
            }

        }

        public ActionResult VentaApuestaExitosa(MegApuestaResponseModel respuestaMegapuestaModel)
        {
            try
            {
                respuestaMegapuestaModel.ID_TRANSACCION = datapago.ID_TRANSACCION;
                respuestaMegapuestaModel.COD_AUTORIZACION = datapago.COD_AUTORIZACION;
                respuestaMegapuestaModel.COD_APROBACION = datapago.COD_APROBACION;
                respuestaMegapuestaModel.DETALLE = datapago.DETALLE;
                respuestaMegapuestaModel.Cambio = resultadoCambio;
            }
            catch (Exception)
            {
                ViewBag.Alert = "Verifique número de convenio o referencia ";
            }


            return View(respuestaMegapuestaModel);
        }

        public ActionResult WplayPreventa()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> WplayPreventa(PreventaRequestModel wplayPreventaRequestModel, string MyTestList)
        {
            string producto = "1501";
            TipoDoc = MyTestList;
            ValorApuestaDeportiva = wplayPreventaRequestModel.valor;

            if (wplayPreventaRequestModel.documento == "" || wplayPreventaRequestModel.valor == 0)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                responsedata = await servicesApuestas.WplayRushBetLuckiaPreventa(wplayPreventaRequestModel, MyTestList, producto);
                wplayPreventaRequestModel.tipoDocumento = TipoDoc;
                return RedirectToAction("PreventaResponse");
            }
            return View();
        }

        public ActionResult PreventaResponse(PreventaResponseModel.Detalle preventaResponseModel)
        {

            preventaResponseModel.fechaTransaccion = responsedata.detalle.fechaTransaccion;
            preventaResponseModel.documento = responsedata.detalle.documento;
            preventaResponseModel.idConfirmacion = responsedata.detalle.idConfirmacion;
            preventaResponseModel.nombre = responsedata.detalle.nombre;



            return View(preventaResponseModel);
        }

        [HttpPost]
        public ActionResult PreventaResponse(PreventaResponseModel.Detalle preventaResponseModel, VentaRequestModel ventaRequestModel)
        {

            try
            {
                preventaResponseModel.fechaTransaccion = responsedata.detalle.fechaTransaccion;
                preventaResponseModel.documento = responsedata.detalle.documento;
                preventaResponseModel.idConfirmacion = responsedata.detalle.idConfirmacion;
                preventaResponseModel.nombre = responsedata.detalle.nombre;
                return RedirectToAction("VentaRequest");
            }
            catch (Exception)
            {
                return View();
            }


        }



        public ActionResult VentaRequest(VentaRequestModel ventaRequestModel)
        {


            try
            {
                string nombreSinDividir = responsedata.detalle.nombre;
                string[] nombreDividido = nombreSinDividir.Split(' ');
                string Nombre = nombreDividido[0];
                string Apellido = nombreDividido[1];
                string IdConf = responsedata.detalle.idConfirmacion;
                string Fecha = responsedata.detalle.fechaTransaccion;

                ventaRequestModel.valor = ValorApuestaDeportiva;
                ventaRequestModel.destinatario = responsedata.detalle.documento;
                ventaRequestModel.extra = new VentaRequestModel.Extra(TipoDoc,
                                                                      Nombre,
                                                                      Apellido,
                                                                      "",
                                                                      IdConf,
                                                                      Fecha
                                                                      );
            }
            catch (Exception)
            {

                throw;
            }

            return View(ventaRequestModel);
        }

        [HttpPost]
        public async Task<ActionResult> VentaRequest(VentaRequestModel ventaRequestModel, string InputRecibo)
        {
            string producto = "1501";
            try
            {
                string nombreSinDividir = responsedata.detalle.nombre;
                string[] nombreDividido = nombreSinDividir.Split(' ');
                string Nombre = nombreDividido[0];
                string Apellido = nombreDividido[1];
                string IdConf = responsedata.detalle.idConfirmacion;
                string Fecha = responsedata.detalle.fechaTransaccion;

                ventaRequestModel.valor = ValorApuestaDeportiva;
                ventaRequestModel.destinatario = responsedata.detalle.documento;
                ventaRequestModel.extra = new VentaRequestModel.Extra(TipoDoc,
                                                                      Nombre,
                                                                      Apellido,
                                                                      "",
                                                                      IdConf,
                                                                      Fecha
                                                                      );
                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), ventaRequestModel.valor);
                responsedataApuestas = await servicesApuestas.WplayRushBetLuckiaVenta(ventaRequestModel, producto, InputRecibo);
                return RedirectToAction("VentaExitosa");
            }
            catch (Exception)
            {

            }
            return View();
        }

        public ActionResult VentaExitosa(VentaResponseModel ventaResponseModel)
        {
            try
            {
                ventaResponseModel.ID_TRANSACCION = responsedataApuestas.ID_TRANSACCION;
                ventaResponseModel.COD_AUTORIZACION = responsedataApuestas.COD_AUTORIZACION;
                ventaResponseModel.COD_APROBACION = responsedataApuestas.COD_APROBACION;
                ventaResponseModel.DETALLE = responsedataApuestas.DETALLE;
                ventaResponseModel.Cambio = resultadoCambio;
            }
            catch (Exception)
            {

                throw;
            }
            return View(ventaResponseModel);
        }

        public ActionResult RushbetPreventa()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RushBetPreventa(PreventaRequestModel preventaRequestModel)
        {
            string producto = "1774";
            ValorApuestaDeportiva = preventaRequestModel.valor;

            if (preventaRequestModel.documento == " " || preventaRequestModel.valor == 0)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                responsedata = await servicesApuestas.WplayRushBetLuckiaPreventa(preventaRequestModel, null, producto);
                return RedirectToAction("RushBetPreventaResponse");
            }
            return View();
        }

        public ActionResult RushBetPreventaResponse(PreventaResponseModel.Detalle preventaResponseModel)
        {
            try
            {
                preventaResponseModel.documento = responsedata.detalle.documento;
                preventaResponseModel.payId = responsedata.detalle.payId;

            }
            catch (Exception)
            {
                throw;
            }
            return View(preventaResponseModel);
        }

        [HttpPost]
        public ActionResult RushBetPreventaResponse(PreventaResponseModel.Detalle preventaResponseModel, VentaRequestModel ventaRequestModel)
        {
            try
            {
                preventaResponseModel.documento = responsedata.detalle.documento;
                preventaResponseModel.payId = responsedata.detalle.payId;
                return RedirectToAction("RushBetVentaRequest");
            }
            catch (Exception)
            {
                return View();
            }

        }

        public ActionResult RushBetVentaRequest(VentaRequestModel ventaRequestModel)
        {

            try
            {
                string PayId = responsedata.detalle.payId;

                ventaRequestModel.valor = ValorApuestaDeportiva;
                ventaRequestModel.destinatario = responsedata.detalle.documento;
                ventaRequestModel.extra = new VentaRequestModel.Extra("", "", "", PayId, "", "");


            }
            catch (Exception)
            {

            }
            return View(ventaRequestModel);
        }

        [HttpPost]
        public async Task<ActionResult> RushBetVentaRequest(VentaRequestModel ventaRequestModel, string InputNombre, string InputApellido, string InputRecibo)
        {
            string producto = "1774";
            try
            {

                string PayId = responsedata.detalle.payId;
                ventaRequestModel.valor = ValorApuestaDeportiva;
                ventaRequestModel.destinatario = responsedata.detalle.documento;
                ventaRequestModel.extra = new VentaRequestModel.Extra("", InputNombre, InputApellido, PayId, "", "");

                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), ventaRequestModel.valor);
                responsedataApuestas = await servicesApuestas.WplayRushBetLuckiaVenta(ventaRequestModel, producto, InputRecibo);

                return RedirectToAction("VentaExitosa");
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult LuckiaPreventa()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LuckiaPreventa(PreventaRequestModel preventaRequestModel)
        {
            string producto = "1778";
            Nombre = preventaRequestModel.name;
            Apellido = preventaRequestModel.last_name;



            ValorApuestaDeportiva = preventaRequestModel.valor;

            if (preventaRequestModel.documento == " " || preventaRequestModel.name == " " || preventaRequestModel.last_name == " " || preventaRequestModel.valor == 0)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            else
            {
                responsedata = await servicesApuestas.WplayRushBetLuckiaPreventa(preventaRequestModel, null, producto);
                return RedirectToAction("LuckiaPreventaResponse");
            }
            return View();
        }

        public ActionResult LuckiaPreventaResponse(PreventaResponseModel.Detalle preventaResponseModel)
        {
            try
            {
                preventaResponseModel.nombre = responsedata.detalle.nombre;
                preventaResponseModel.documento = responsedata.detalle.documento;
                preventaResponseModel.idTransaccion = responsedata.detalle.idTransaccion;
                preventaResponseModel.fechaPlana = responsedata.detalle.fechaPlana;
            }
            catch (Exception)
            {
                return View();
            }


            return View(preventaResponseModel);
        }

        [HttpPost]
        public ActionResult LuckiaPreventaResponse(PreventaResponseModel.Detalle preventaResponseModel, VentaRequestModel ventaRequestModel)
        {

            try
            {
                preventaResponseModel.nombre = responsedata.detalle.nombre;
                preventaResponseModel.documento = responsedata.detalle.documento;
                preventaResponseModel.idTransaccion = responsedata.detalle.idTransaccion;
                preventaResponseModel.fechaPlana = responsedata.detalle.fechaPlana;
                return RedirectToAction("LuckiaVentaRequest");
            }
            catch (Exception)
            {
                return View();
            }


        }

        public ActionResult LuckiaVentaRequest(VentaRequestModel ventaRequestModel)
        {
            string nombre = Nombre;
            string apellido = Apellido;

            ventaRequestModel.destinatario = responsedata.detalle.documento;
            ventaRequestModel.valor = ValorApuestaDeportiva;
            ventaRequestModel.extra = new VentaRequestModel.Extra("", nombre, apellido, "", "", "");

            return View(ventaRequestModel);
        }

        [HttpPost]
        public async Task<ActionResult> LuckiaVentaRequest(VentaRequestModel ventaRequestModel, string InputRecibo)
        {
            string producto = "1778";

            try
            {
                string nombre = Nombre;
                string apellido = Apellido;

                ventaRequestModel.destinatario = responsedata.detalle.documento;
                ventaRequestModel.valor = ValorApuestaDeportiva;
                ventaRequestModel.extra = new VentaRequestModel.Extra("", nombre, apellido, "", "", "");

                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), ventaRequestModel.valor);
                responsedataApuestas = await servicesApuestas.WplayRushBetLuckiaVenta(ventaRequestModel, producto, InputRecibo);
                 return RedirectToAction("VentaExitosa");

            }
            catch (Exception)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }

            return View();
        }

        public ActionResult AquiJuegoVentaResponse()
        {
            return View();
        }

        [HttpPost]
        public async Task <ActionResult> AquiJuegoVentaResponse(VentaRequestModel ventaRequestModel, string InputRecibo) 
        {
            string producto = "1776";
            try
            {
                ventaRequestModel.extra = new VentaRequestModel.Extra("", "", "", "", "", "");

                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), ventaRequestModel.valor);
                responsedataApuestas = await servicesApuestas.WplayRushBetLuckiaVenta(ventaRequestModel, producto, InputRecibo);
                return RedirectToAction("VentaExitosa");
            }
            catch (Exception)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            return View();
        }

        public ActionResult SportiumVentaRequest() 
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SportiumVentaRequest(VentaRequestModel ventaRequestModel, string InputRecibo)
        {
            string producto = "1780";
            try
            {
                ventaRequestModel.extra = new VentaRequestModel.Extra("", "", "", "", "", "");

                resultadoCambio = entregarCambio.DineroDeCambio(int.Parse(InputRecibo), ventaRequestModel.valor);
                responsedataApuestas = await servicesApuestas.WplayRushBetLuckiaVenta(ventaRequestModel, producto, InputRecibo);
                return RedirectToAction("VentaExitosa");
            }
            catch (Exception)
            {
                ViewBag.Alert = "  Hay campos sin diligenciar";
            }
            return View();
        }
    }
}