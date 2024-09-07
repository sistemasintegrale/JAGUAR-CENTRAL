using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SGE.Common
{
    public static class WebDavTest
    {
        public static bool Put(string pathBase, string filename, string ubicacion)
        {
            try
            {
                string szURL1 = pathBase + filename;


                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                byte[] fileBytes;
                using (FileStream fileStream = new FileStream(ubicacion, FileMode.Open))
                {
                    using (BinaryReader binReader = new BinaryReader(fileStream))
                    {
                        fileBytes = binReader.ReadBytes((int)fileStream.Length);
                    }
                }
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                HttpWebRequest httpPutRequest = (HttpWebRequest)WebRequest.Create(szURL1);
                httpPutRequest.Credentials = new NetworkCredential(szUsername, szPassword);
                httpPutRequest.PreAuthenticate = true;
                httpPutRequest.Method = @"PUT";
                httpPutRequest.Headers.Add(@"Overwrite", @"T");
                httpPutRequest.ContentLength = fileBytes.Length;
                httpPutRequest.SendChunked = true;
                Stream requestStream = httpPutRequest.GetRequestStream();
                requestStream.Write(fileBytes, 0, fileBytes.Length);
                requestStream.Close();
                HttpWebResponse httpPutResponse = (HttpWebResponse)httpPutRequest.GetResponse();
                Console.WriteLine(@"PUT Response: {0}", httpPutResponse.StatusDescription);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }


        public static bool Put(string pathBase, string filename, Image image)
        {
            try
            {
                string szURL1 = pathBase + filename;

                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                byte[] fileBytes;


                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Puedes cambiar el formato si lo necesitas
                    fileBytes = ms.ToArray();
                }

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                HttpWebRequest httpPutRequest = (HttpWebRequest)WebRequest.Create(szURL1);
                httpPutRequest.Credentials = new NetworkCredential(szUsername, szPassword);
                httpPutRequest.PreAuthenticate = true;
                httpPutRequest.Method = @"PUT";
                httpPutRequest.Headers.Add(@"Overwrite", @"T");
                httpPutRequest.ContentLength = fileBytes.Length;
                httpPutRequest.SendChunked = true;
                Stream requestStream = httpPutRequest.GetRequestStream();
                requestStream.Write(fileBytes, 0, fileBytes.Length);
                requestStream.Close();
                HttpWebResponse httpPutResponse = (HttpWebResponse)httpPutRequest.GetResponse();
                Console.WriteLine(@"PUT Response: {0}", httpPutResponse.StatusDescription);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }

        public static Stream Get(string pathBase, string filename)
        {
            try
            {

                string szURL2 = pathBase + filename;

                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                HttpWebRequest httpGetRequest = (HttpWebRequest)WebRequest.Create(szURL2);
                httpGetRequest.Credentials = new NetworkCredential(szUsername, szPassword);
                httpGetRequest.PreAuthenticate = true;
                httpGetRequest.Method = @"GET";
                httpGetRequest.Headers.Add(@"Translate", "F");
                HttpWebResponse httpGetResponse = (HttpWebResponse)httpGetRequest.GetResponse();
                Stream responseStream = httpGetResponse.GetResponseStream();
                long responseLength = httpGetResponse.ContentLength;
                MemoryStream ms = new MemoryStream();
                responseStream.CopyTo(ms);
                byte[] data2 = ms.ToArray();
                responseStream.Read(data2, 0, data2.Length);
                System.Diagnostics.Debug.WriteLine(@"GET Response: {0}", httpGetResponse.StatusDescription);
                System.Diagnostics.Debug.WriteLine(@"  Response Length: {0}", responseLength);
                return ms;

            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static bool Crearcarpeta(string pathBase, string filename)
        {
            string url = pathBase + filename;
            try
            {
                // Crear la solicitud WebDAV
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "MKCOL"; // Usar el método MKCOL para crear la carpeta
                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                request.Credentials = new NetworkCredential(szUsername, szPassword);
                // Realizar la solicitud
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("Estado de la respuesta: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            return true;
        }



        public static async Task<bool> EliminarArchivo(string pathBase)
        {
            string url = pathBase;
            try
            {
                // Crear la solicitud WebDAV
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = @"DELETE"; // Usar el método MKCOL para crear la carpeta
                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                request.Credentials = new NetworkCredential(szUsername, szPassword);
                // Realizar la solicitud
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("Estado de la respuesta: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public static string GetCertificado(string pathBase, string filename)
        {
            try
            {

                string szURL2 = pathBase + filename;

                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                HttpWebRequest httpGetRequest = (HttpWebRequest)WebRequest.Create(szURL2);
                httpGetRequest.Credentials = new NetworkCredential(szUsername, szPassword);
                httpGetRequest.PreAuthenticate = true;
                httpGetRequest.Method = @"GET";
                httpGetRequest.Headers.Add(@"Translate", "F");
                HttpWebResponse httpGetResponse = (HttpWebResponse)httpGetRequest.GetResponse();
                Stream responseStream = httpGetResponse.GetResponseStream();
                long responseLength = httpGetResponse.ContentLength;
                MemoryStream ms = new MemoryStream();
                responseStream.CopyTo(ms);
                byte[] data2 = ms.ToArray();
                responseStream.Read(data2, 0, data2.Length);
                System.Diagnostics.Debug.WriteLine(@"GET Response: {0}", httpGetResponse.StatusDescription);
                System.Diagnostics.Debug.WriteLine(@"  Response Length: {0}", responseLength);
                byte[] byteArray = ms.ToArray();

                // Convertir la matriz de bytes a una cadena Base64
                return Convert.ToBase64String(byteArray);


            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }


        public static void Descargar(string szURL2, string rutaDestino)
        {
            try
            {
                string szUsername = @"cloudjaguar";
                string szPassword = @"8qei58O2!";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                HttpWebRequest httpGetRequest = (HttpWebRequest)WebRequest.Create(szURL2);
                httpGetRequest.Credentials = new NetworkCredential(szUsername, szPassword);
                httpGetRequest.PreAuthenticate = true;
                httpGetRequest.Method = @"GET";
                httpGetRequest.Headers.Add(@"Translate", "F");
                HttpWebResponse httpGetResponse = (HttpWebResponse)httpGetRequest.GetResponse();
                Stream responseStream = httpGetResponse.GetResponseStream();
                long responseLength = httpGetResponse.ContentLength;
                MemoryStream ms = new MemoryStream();
                responseStream.CopyTo(ms);
                byte[] data2 = ms.ToArray();
                responseStream.Read(data2, 0, data2.Length);
                System.Diagnostics.Debug.WriteLine(@"GET Response: {0}", httpGetResponse.StatusDescription);
                System.Diagnostics.Debug.WriteLine(@"  Response Length: {0}", responseLength);
                FileStream fs = File.Create(rutaDestino);
                fs.Write(data2, 0, data2.Length);
                fs.Close();
                responseStream.Close();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<bool> GuardarImagenes(string pathBase, string filename, Image image)
        {
            return await Task.Run(() => Put(pathBase, filename, image));
        }
    }
}
