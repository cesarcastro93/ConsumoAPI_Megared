using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ConsumoDeAPI_MegaRed.Models.ServiciosPublicosModel;


namespace ConsumoDeAPI_MegaRed.Servicios
{
    public class ServiciosPublicos
    {
        dynamic ConsultaReferencia = null;
        dynamic respuestaPagoServicios = null;

        PagarServicioPublicoModel pagarServicio = new PagarServicioPublicoModel();
        PagarServicioPublicoModel.datapago detalle = new PagarServicioPublicoModel.datapago();


        public async Task<IEnumerable> PostConsultarPagoServiciosPublicos(string convenio, string referen)
        {

            ConsultarServiciosPublicosModel pago = new ConsultarServiciosPublicosModel();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };


            var SPConsultaValor = new ConsultarServiciosPublicosModel
            {
                barcode = "",
                convenio = convenio,
                referencia = referen,
                metodo = "MANUAL"
            };

            var URL = $"https://190.85.203.50:444/servicios_publicos/consultar/";

            string JsonData = JsonConvert.SerializeObject(SPConsultaValor);
            var request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.Accept = "application/json";
            request.Headers["V"] = "1.6.6";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                    {
                        return null;
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBodyConsultaReferencia = objReader.ReadToEndAsync().Result;
                            ConsultaReferencia = JsonConvert.DeserializeObject(responseBodyConsultaReferencia);

                            pagarServicio.STATUS = ConsultaReferencia.STATUS;
                            pagarServicio.canal = ConsultaReferencia.canal=7;
                            pagarServicio.comercio_multiproducto = ConsultaReferencia.comercio_multiproducto = false;
                            detalle.codigoConvenio=ConsultaReferencia.DETALLE.codigoConvenio;
                            detalle.referencia = ConsultaReferencia.DETALLE.referencia;
                            detalle.nombreBanco = ConsultaReferencia.DETALLE.nombreBanco;
                            detalle.typePayBill = ConsultaReferencia.DETALLE.typePayBill;
                            detalle.codigoProducto = ConsultaReferencia.DETALLE.codigoProducto;
                            detalle.amount = ConsultaReferencia.DETALLE.amount;
                            detalle.hashEchoData = ConsultaReferencia.DETALLE.hashEchoData;
                            detalle.hash = ConsultaReferencia.DETALLE.hash;
                            detalle.partialPayment = ConsultaReferencia.DETALLE.partialPayment;
                            detalle.operador = ConsultaReferencia.DETALLE.operador;
                            pagarServicio.VISTA = ConsultaReferencia.VISTA;

                            objReader.Close();
                            strReader.Close();


                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e + " error");
            }
            return ConsultaReferencia;
        }
        public async Task<IEnumerable> PostPagarServiciosPublicos(PagarServicioPublicoModel pagarServicio )
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            RespuestaServiciosPublicos.detalle respuestaServicios = new RespuestaServiciosPublicos.detalle();

            string JsonE = JsonConvert.SerializeObject(pagarServicio );
            var url = $"https://190.85.203.50:444/servicios_publicos/pagar/";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "application/json";
                request.Headers["V"] = "1.6.6";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(JsonE);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                    {
                        return null;
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBodyConsultaReferencia = objReader.ReadToEndAsync().Result;
                            respuestaPagoServicios = JsonConvert.DeserializeObject(responseBodyConsultaReferencia);

                            respuestaServicios.ID_TRANSACCION = respuestaPagoServicios.DETALLE.ID_TRANSACCION;
                            respuestaServicios.COD_AUTORIZACION = respuestaPagoServicios.DETALLE.COD_AUTORIZACION;
                            respuestaServicios.COD_APROBACION = respuestaPagoServicios.DETALLE.COD_APROBACION;
                            respuestaServicios.DETALLE = respuestaPagoServicios.DETALLE.DETALLE;


                            objReader.Close();
                            strReader.Close();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return respuestaPagoServicios;
        }


    }

}