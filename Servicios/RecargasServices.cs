using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using ConsumoDeAPI_MegaRed.Models.RecargasModel;
using ConsumoDeAPI_MegaRed.Models.SaldosModel;
using Newtonsoft.Json;

namespace ConsumoDeAPI_MegaRed.Servicios
{
    public class RecargasServices
    {
        public RespuestaRecargasModel respuestaRecargas = new RespuestaRecargasModel();
        SaldosModel saldos = new SaldosModel();

        public static dynamic RespRecarga;
        public static dynamic RecargaconsultaTransaccion;
        public async Task<IEnumerable> PostEnviarRecarga(int Operador, string Destinatario, int Valor)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            var url = "https://190.85.203.50:444/transacciones/venta/";

            var Recargas = new RecargaVentaModel
            {
                producto = Operador,
                destinatario = Destinatario,
                valor = Valor,
                bolsa = "multiproducto",
                referencia = "123444"

            };

            string JsonData = JsonConvert.SerializeObject(Recargas);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
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
                            RespRecarga = JsonConvert.DeserializeObject(responseBodyConsultaReferencia);

                            respuestaRecargas.ID_TRANSACCION = RespRecarga.ID_TRANSACCION;
                            respuestaRecargas.COD_APROBACION = RespRecarga.COD_APROBACION;
                            respuestaRecargas.COD_AUTORIZACION = RespRecarga.COD_AUTORIZACION;
                            respuestaRecargas.DETALLE = RespRecarga.DETALLE;
                            saldos.SALDO = RespRecarga.SALDOS.SALDO;
                            saldos.SALDO_ESPECIAL = RespRecarga.SALDOS.SALDO_ESPECIAL;
                            saldos.MULTI_PRODUCTO = RespRecarga.SALDOS.MULTI_PRODUCTO;


                            objReader.Close();
                            strReader.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RespRecarga;
        }

        public async Task<IEnumerable> ConsultarRecargas(string InputNumeroCelular, string InputIdTrx)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            var url = "https://190.85.203.50:444/reportes/consultaTransaccion/?celular=[" + InputNumeroCelular + "]&id_trx=[" + InputIdTrx + "]";

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
                        if (strReader == null)
                        {
                            return null;
                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBodyConsultaSaldos = objReader.ReadToEndAsync().Result;
                            RecargaconsultaTransaccion = JsonConvert.DeserializeObject(responseBodyConsultaSaldos);


                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RecargaconsultaTransaccion;
        }
    }

}