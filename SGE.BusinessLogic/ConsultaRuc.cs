using Newtonsoft.Json;
using SGE.Entity;
using System;
using System.IO;
using System.Net;

namespace SGE.BusinessLogic
{
    public static class ConsultaRucApi
    {
        public static objresultDNI ConsutaDNI(string _dni)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var consult = new ObjConsultaDNI()
            {
                token = "ee270056-7c9d-4d98-b471-af25004b3172-de11769b-ebfe-4a1d-8d85-05c108a9d8d0",
                dni = _dni
            };
            string json = JsonConvert.SerializeObject(consult);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var resultado = JsonConvert.DeserializeObject<objresultDNI>(result);

                    return resultado;
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Objesultado ConsultaRuc(string _ruc)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var consult = new ObjConsulta()
            {
                token = "ee270056-7c9d-4d98-b471-af25004b3172-de11769b-ebfe-4a1d-8d85-05c108a9d8d0",
                ruc = _ruc
            };
            string json = JsonConvert.SerializeObject(consult);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var resultado = JsonConvert.DeserializeObject<Objesultado>(result);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
