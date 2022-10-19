using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using ConsumoDeAPI_MegaRed.Models.ApuestasDeportivasModel;

namespace ConsumoDeAPI_MegaRed.Servicios
{
    public class ApuestasDeportivasServices
    {
        MegaApuestaModel PreventaMegapuestaModel = new MegaApuestaModel();
        PreventaRequestModel preventaRequestModel = new PreventaRequestModel();


        public static dynamic RespuestaPreventaApuestas;
        public static dynamic RespuestaVentaApuestas;


        public async Task<IEnumerable> PreventaMegaApuestas(MegaApuestaModel megaApuestaModel)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var url = $"https://190.85.203.50:444/megapuesta/preventa/?usuario=[" + megaApuestaModel.destinatario + "]&valor=[" + megaApuestaModel.valor + "]";


            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.Accept = "application/json";
                request.Headers["V"] = "1.6.6";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBodyPreVentaMega = objReader.ReadToEndAsync().Result;
                            RespuestaPreventaApuestas = JsonConvert.DeserializeObject(responseBodyPreVentaMega);
                            PreventaMegapuestaModel.NombreApostador = RespuestaPreventaApuestas;
                        }
                    }

                }
                else

                {
                    Console.WriteLine("Hubo un error");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error" + ex.ToString());
            }
            return RespuestaPreventaApuestas;
        }


        public async Task<IEnumerable> VentaMegapuesta(MegaApuestaModel megaApuestaModel)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var url = $"https://190.85.203.50:444/transacciones/venta/";

            var VentaMegaPuesta = new MegaApuestaModel
            {
                canal = 5,
                producto = "360",
                bolsa = "multiproducto",
                valor = megaApuestaModel.valor,
                destinatario = megaApuestaModel.destinatario
            };

            string JsonE = JsonConvert.SerializeObject(VentaMegaPuesta);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Accept = "application/json";
                request.Headers["V"] = "1.6.6";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";
                using (var streamWriter = new StreamWriter(request.GetRequestStreamAsync().Result))
                {
                    streamWriter.Write(JsonE);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBodyApuestaMega = objReader.ReadToEndAsync().Result;
                        RespuestaVentaApuestas = JsonConvert.DeserializeObject(responseBodyApuestaMega);



                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error" + ex.Message);
            }


            return RespuestaVentaApuestas;
        }

        public async Task<IEnumerable> WplayRushBetLuckiaPreventa(PreventaRequestModel PreventaRequestModel, string MyTestList, string producto)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var url = $"https://190.85.203.50:444/apuestas/preventa/?operador=" + producto;

            var RequestPreventa = new PreventaRequestModel
            {
                documento = PreventaRequestModel.documento,
                tipoDocumento = MyTestList,
                valor = PreventaRequestModel.valor,
                name = PreventaRequestModel.name,
                last_name = PreventaRequestModel.last_name
            };

            string JsonE = JsonConvert.SerializeObject(RequestPreventa);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "application/json";
                request.Headers["V"] = "1.6.6";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";
                using (var streamWriter = new StreamWriter(request.GetRequestStreamAsync().Result))
                {
                    streamWriter.Write(JsonE);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBodyApuestaWplay = objReader.ReadToEndAsync().Result;
                        RespuestaPreventaApuestas = JsonConvert.DeserializeObject(responseBodyApuestaWplay);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error" + ex.ToString());
            }
            return RespuestaPreventaApuestas;
        }

        public async Task<IEnumerable> WplayRushBetLuckiaVenta(VentaRequestModel ventaRequestModel, string producto, string inputRecibo)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var url = $"https://190.85.203.50:444/transacciones/venta/";

            VentaRequestModel request = new VentaRequestModel();
            request.canal = 5;
            request.producto = producto;
            request.bolsa = "multiproducto";
            request.valor = ventaRequestModel.valor;
            request.destinatario = ventaRequestModel.destinatario;
            request.comercio_multiproducto = false;
            request.extra = new VentaRequestModel.Extra(ventaRequestModel.extra.tipoDocumento,
                                                        ventaRequestModel.extra.name,
                                                        ventaRequestModel.extra.last_name,
                                                        ventaRequestModel.extra.pay,
                                                        ventaRequestModel.extra.idConfirmacion,
                                                        ventaRequestModel.extra.fechaTransaccion);

            string JsonE = JsonConvert.SerializeObject(request);

            try
            {
                HttpWebRequest WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

               

                WebRequest.Method = "POST";
                WebRequest.Accept = "application/json";
                WebRequest.Headers["V"] = "1.6.6";
                WebRequest.ContentType = "application/json";
                WebRequest.Headers["Authorization"] = "token 437eb8a49ebc2963d70e7912c26c7de5004387da";



                using (var streamWriter = new StreamWriter(WebRequest.GetRequestStreamAsync().Result))
                {
                    streamWriter.Write(JsonE);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse response = (HttpWebResponse)await WebRequest.GetResponseAsync();
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null ;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBodyRecargaWplay = objReader.ReadToEndAsync().Result;
                        RespuestaVentaApuestas = JsonConvert.DeserializeObject(responseBodyRecargaWplay);
                       
                        objReader.Close();
                        strReader.Close();

                    }
                }
               
            }
            catch (Exception ex)
            {
               Console.WriteLine("Hubo un error" + ex.ToString());
            }

            return RespuestaVentaApuestas;
        }
    }

}
