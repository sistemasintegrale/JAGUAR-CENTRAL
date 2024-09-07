using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SIDE.COMUN.DTO.Intercambio;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SGE.WindowForms
{
    public class DocumentoElectronicoResponse
    {
        //Metodo de facturacion electronica
        //Jaguar
        public async Task<EFacturaElectronicaResponse> FacturaElectronica(DocumentoElectronico objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioFacturaElectronica;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {

                    objFirmar.CertificadoDigital = lstParametro.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = objrespuesta.TramaXmlSinFirma,
                        CertificadoDigital = WebDavTest.GetCertificado(lstParametro.DirecciónXML, Rutas.Cetificado),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"{objdocumento.IdDocumento.Trim()}firma.xml");

                        File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));
                        var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(rutaArchivo));

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado, //objrespuestaFirma.TramaXmlFirmado
                            OSE = true
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);


                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {
                                //GUARDAR EN NEXTCLUD

                                string nombreArchivo = $"{lstParametro.Ruc}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento}.zip";
                                string rutaArchivoEnviado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nombreArchivo}");
                                if (File.Exists(rutaArchivoEnviado))
                                    File.Delete(rutaArchivoEnviado);
                                File.WriteAllBytes(rutaArchivoEnviado, Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));
                                if (objdocumento.TipoDocumento == "03")
                                {
                                    WebDavTest.Put($"{lstParametro.DirecciónXML}{Rutas.CarpataBOV}", $"{nombreArchivo}", rutaArchivoEnviado);
                                }
                                if (objdocumento.TipoDocumento == "01")
                                {
                                    WebDavTest.Put($"{lstParametro.DirecciónXML}{Rutas.CarpataFAV}", $"{nombreArchivo}", rutaArchivoEnviado);
                                }
                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                                path = path + objrespuestaEnvioDoc.NombreArchivo + ".xml";

                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }





        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "cafeasp demo 01";
        private void insertar(string ruta)
        {
            UserCredential credential;

            credential = GetCredentials();
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            UploadBasicImage(ruta, service);



        }
        private UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("Credenciales/credentials_jhonatan.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                // Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }
        private static void UploadBasicImage(string path, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Path.GetFileName(path);
            fileMetadata.MimeType = "application/xml";
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/xml");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);

        }

        //Metodo de Nota credito
        public async Task<EFacturaElectronicaResponse> NotaCreditoElectronica(DocumentoElectronico objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;

            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioNotaCredito;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));


                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = WebDavTest.GetCertificado(lstParametro.DirecciónXML, Rutas.Cetificado),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificad
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {

                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        string rutaArchivo_fir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                          $"{objdocumento.IdDocumento.Trim()}-firma.xml");

                        File.WriteAllBytes(rutaArchivo_fir, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));


                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado, //objrespuestaFirma.TramaXmlFirmado
                            OSE = true
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {

                                string nombreArchivo = $"{lstParametro.Ruc}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento}.zip";
                                string rutaArchivoEnviado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nombreArchivo}");
                                if (File.Exists(rutaArchivoEnviado))
                                    File.Delete(rutaArchivoEnviado);
                                File.WriteAllBytes(rutaArchivoEnviado, Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));

                                WebDavTest.Put($"{lstParametro.DirecciónXML}{Rutas.CarpataNTC}", $"{nombreArchivo}", rutaArchivoEnviado);


                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }


        //Metodo  de Nota debito
        public async Task<EFacturaElectronicaResponse> NotaDebitoElectronica(DocumentoElectronico objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = ConfigurationManager.AppSettings.Get("rutaOutSunatNCrditoNDebito");
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioNotaDebito;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    objFirmar.CertificadoDigital = lstParametro.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{objFirmar.CertificadoDigital}"))),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificad
                        UnSoloNodoExtension = false
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = objdocumento.TipoDocumento.Trim(),
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarDocumentoResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito && !string.IsNullOrEmpty(objrespuestaEnvioDoc.TramaZipCdr))
                            {
                                //System.IO.File.WriteAllBytes($"{path}{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                //    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                //System.IO.File.WriteAllBytes($"{path}R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                //Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));

                                response.TramaZipCdr = objrespuestaEnvioDoc.TramaZipCdr;
                                response.NroTicketCdr = objrespuestaEnvioDoc.NroTicketCdr;
                                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        public async Task<ESunatDocumentosBajaResponse> DocumentoElectronicoBaja(ComunicacionBaja objdocumento)
        {
            string path = Application.StartupPath;
            path = path.Replace(@"bin\Debug", "");
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            ESunatDocumentosBajaResponse response = new ESunatDocumentosBajaResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicioDocumentoBaja;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnvioResumen; //lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{lstParametro.CertificadoDigital}"))),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacion == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacion == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = "",
                            TramaXmlFirmado = objrespuestaFirma.TramaXmlFirmado //objrespuestaFirma.TramaXmlFirmado
                        };

                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarResumenResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarResumenResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito)
                            {
                                //System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\{objrespuestaEnvioDoc.NombreArchivo}.xml",
                                //    Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));

                                //System.IO.File.WriteAllBytes($"{path}\\ResultadoSunat\\R-{objrespuestaEnvioDoc.NombreArchivo}.zip",
                                //Convert.FromBase64String(objrespuestaEnvioDoc.TramaZipCdr));


                                response.Pila = objrespuestaEnvioDoc.Pila;
                                response.NroTicket = objrespuestaEnvioDoc.NroTicket;
                                response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                                response.IdDocumento = objdocumento.IdDocumento;
                                //response.IdItems += $"{objrespuestaEnvioDoc.NombreArchivo},";
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }


        public async Task<EResumenResponse> EnviarResumen(ResumenDiarioNuevo objdocumento)
        {
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = Application.StartupPath;
            path = lstParametro.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            //EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            EResumenResponse response = new EResumenResponse();
            try
            {
                string urlserviceGenerarTramaSinFirmar = lstParametro.urlServicoGenerarResumen;
                string urlserviceFirmar = lstParametro.urlServicioFirma;
                string urlServiceEnviarDocumento = lstParametro.urlServicioEnvioResumen; //lstParametro.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest(); ;
                var responde = await HttpRequestFactory.Post(urlserviceGenerarTramaSinFirmar, objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.IdDocumento.Trim()}.xml");

                    System.IO.File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));

                    //Firmar
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        CertificadoDigital = WebDavTest.GetCertificado(lstParametro.DirecciónXML, Rutas.Cetificado),
                        PasswordCertificado = lstParametro.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        if (lstParametro.IdServiceValidacionResumen == "0")
                            EndPointUrl = lstParametro.EndPointUrlPrueba;
                        if (lstParametro.IdServiceValidacionResumen == "1")
                            EndPointUrl = lstParametro.EndPointUrlDesarrollo;

                        string rutaArchivoFirmado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"{objdocumento.IdDocumento.Trim()}firma.xml");

                        File.WriteAllBytes(rutaArchivoFirmado, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));
                        var tramaXmlSinFirmado = Convert.ToBase64String(File.ReadAllBytes(rutaArchivoFirmado));

                        var enviarDocumentoRequest = new EnviarDocumentoRequest
                        {
                            Ruc = lstParametro.Ruc,
                            UsuarioSol = lstParametro.UsuarioSol,
                            ClaveSol = lstParametro.ClaveSol,
                            EndPointUrl = EndPointUrl,
                            IdDocumento = objdocumento.IdDocumento.Trim(),
                            TipoDocumento = "",
                            TramaXmlFirmado = tramaXmlSinFirmado, //objrespuestaFirma.TramaXmlFirmado
                            OSE = true
                        };



                        var respondeEnvioDoc = await HttpRequestFactory.Post(urlServiceEnviarDocumento, enviarDocumentoRequest);
                        EnviarResumenResponse objrespuestaEnvioDoc = JsonConvert.DeserializeObject<EnviarResumenResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);

                        response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;

                        if (objrespuestaEnvioDoc.Exito)
                        {
                            if (objrespuestaEnvioDoc.Exito)
                            {

                                response.Pila = objrespuestaEnvioDoc.Pila;
                                response.NroTicket = objrespuestaEnvioDoc.NroTicket;
                                response.NombreArchivo = objrespuestaEnvioDoc.NombreArchivo;
                                response.Exito = objrespuestaEnvioDoc.Exito;
                                response.IdDocumento = objdocumento.IdDocumento;
                            }
                        }
                        else
                        {
                            response.MensajeError = objrespuestaEnvioDoc.MensajeError;
                            response.CodigoRespuesta = objrespuestaEnvioDoc.MensajeError.Substring(7, 4);
                            response.ProcesoEnviar = 1;
                            response.Exito = objrespuestaEnvioDoc.Exito;

                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }
        public async Task<EnviarDocumentoResponse> ConsultaTiket(ConsultaTicketRequest objdocumento, string path)
        {
            string EndPointUrl = string.Empty;
            EParametro lstParametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            EnviarDocumentoResponse response = new EnviarDocumentoResponse();
            try
            {
                if (lstParametro.IdServiceValidacion == "0")
                    EndPointUrl = lstParametro.EndPointUrlPrueba;
                if (lstParametro.IdServiceValidacion == "1")
                    EndPointUrl = lstParametro.EndPointUrlDesarrollo;


                var enviarConsultaTiketRequest = new ConsultaTicketRequest
                {
                    Ruc = lstParametro.Ruc,
                    UsuarioSol = lstParametro.UsuarioSol,
                    ClaveSol = lstParametro.ClaveSol,
                    EndPointUrl = EndPointUrl,
                    IdDocumento = "",
                    TipoDocumento = "",
                    NroTicket = objdocumento.NroTicket,
                    OSE = true
                };

                var respondeEnvioDoc = await HttpRequestFactory.Post(lstParametro.ServiceConsultaTiket, enviarConsultaTiketRequest);

                response = JsonConvert.DeserializeObject<EnviarDocumentoResponse>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);
                if (response.Exito)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                    string rurtaCDR = $"{path}{response.NombreArchivo.Replace(".xml", "")}.zip";
                    if (File.Exists(rurtaCDR))
                        File.Delete(rurtaCDR);

                    File.WriteAllBytes(rurtaCDR, Convert.FromBase64String(response.TramaZipCdr));
                    WebDavTest.Put($"{lstParametro.DirecciónXML}{Rutas.CarpataCDR}", $"{response.NombreArchivo.Replace(".xml", "")}.zip", rurtaCDR);

                }


            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }

        public async Task<EFacturaElectronicaResponse> GuiaRemisionElectronica(GuiaRemision objdocumento)
        {
            EParametro parametros = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = parametros.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            try
            {
                string urlserviceGenerarTramaSinFirmar = parametros.urlServicioFacturaElectronica;
                string urlserviceFirmar = parametros.urlServicioFirma;
                string urlServiceEnviarDocumento = parametros.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest();
                var responde = await HttpRequestFactory.Post("http://www.apisunat.somee.com/api/GenerarGuiaRemision", objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.Remitente.NroDocumento}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento.Trim()}.xml");

                    File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));


                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));
                    //Firmar
                    objFirmar.CertificadoDigital = parametros.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        // CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{objFirmar.CertificadoDigital}"))),// objFirmar.CertificadoDigital,
                        CertificadoDigital = WebDavTest.GetCertificado(parametros.DirecciónXML, Rutas.Cetificado),
                        PasswordCertificado = parametros.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(rutaArchivo);

                        // Modificar el atributo "encoding" del nodo XmlDeclaration
                        XmlDeclaration xmlDeclaration = xmlDoc.FirstChild as XmlDeclaration;
                        if (xmlDeclaration != null)
                        {
                            xmlDeclaration.Encoding = "ISO-8859-1"; // Cambiar a la codificación deseada (por ejemplo, UTF-8)
                        }

                        // Modificar el atributo "standalone" del nodo XmlDeclaration
                        if (xmlDeclaration != null)
                        {
                            xmlDeclaration.Standalone = "no"; // Cambiar a "yes" o "no" según sea necesario
                        }

                        // Guardar los cambios en el archivo XML
                        xmlDoc.Save(rutaArchivo);

                        var requestToken = new GenerarTokenrequest
                        {
                            client_id = parametros.usuarioGRE,
                            client_secret = parametros.claveGRE,
                            username = parametros.UsuarioSolGRE,
                            password = parametros.ClaveSolGRE,
                            ruc = parametros.Ruc,
                            endPoint = $"{parametros.UrlBaseEnvioGRE}/clientessol"
                        };
                        var respondeEnvioToken = await HttpRequestFactory.Post($"{parametros.UrlBaseServicioGRE}/GenerarTokenGuiaRemision", requestToken);
                        var resToken = JsonConvert.DeserializeObject<BaseResponse<string>>(respondeEnvioToken.Content.ReadAsStringAsync().Result);
                        if (resToken.Succes)
                        {
                            string rutaArchivoZip = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{parametros.Ruc}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento}.zip");
                            CrearArchivoZip(rutaArchivo, rutaArchivoZip);
                            var bytexml = File.ReadAllBytes(rutaArchivoZip);
                            var base64encodestring = Convert.ToBase64String(bytexml);

                            string hash = "";
                            using (SHA256 sHA256 = SHA256.Create())
                            {
                                hash = string.Concat(sHA256.ComputeHash(bytexml).Select(x => x.ToString("x2")));
                            }

                            if (parametros.UrlBaseEnvioGRE == "https://api-seguridad.sunat.gob.pe/v1")
                                parametros.UrlBaseEnvioGRE = "https://api-cpe.sunat.gob.pe/v1";
                            var requestEnvio = new EnviarGuiaRemisionResquest
                            {
                                nombreDoc = $"{parametros.Ruc}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento}",
                                hash = hash,
                                tramaZimp = base64encodestring,
                                token = resToken.Data,
                                endPoint = $"{parametros.UrlBaseEnvioGRE}/contribuyente/gem/comprobantes"
                            };
                            string r = JsonConvert.SerializeObject(requestEnvio);
                            var respondeEnvioDoc = await HttpRequestFactory.Post($"{parametros.UrlBaseServicioGRE}/EnviarGuiaRemision", requestEnvio);
                            var resDoc = JsonConvert.DeserializeObject<BaseResponse<string>>(respondeEnvioDoc.Content.ReadAsStringAsync().Result);
                            if (resDoc.Succes)
                            {


                                response.TramaZipCdr = "";
                                response.NroTicketCdr = resDoc.Data;
                                response.MensajeRespuesta = "";
                                response.CodigoRespuesta = "0";
                                response.Exito = true;
                                response.Urldescarga = "";


                            }
                            else
                            {
                                response.MensajeError = resDoc.Data;
                                response.ProcesoFirmar = 1;
                                response.Exito = resDoc.Succes;
                                response.CodigoRespuesta = "0";
                            }

                        }
                        else
                        {
                            response.MensajeError = resToken.Data;
                            response.ProcesoFirmar = 1;
                            response.Exito = resToken.Succes;
                            response.CodigoRespuesta = "0";
                        }
                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }


        public async Task<EFacturaElectronicaResponse> DescargarCDR(string NumeroTicket)
        {
            var parametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            var response = new EFacturaElectronicaResponse();
            try
            {
                var requestToken = new GenerarTokenrequest
                {
                    client_id = parametro.usuarioGRE,
                    client_secret = parametro.claveGRE,
                    username = parametro.UsuarioSolGRE,
                    password = parametro.ClaveSolGRE,
                    ruc = parametro.Ruc,
                    endPoint = $"{parametro.UrlBaseEnvioGRE}/clientessol"
                };
                var respondeEnvioToken = await HttpRequestFactory.Post($"{parametro.UrlBaseServicioGRE}/GenerarTokenGuiaRemision", requestToken);
                var resToken = JsonConvert.DeserializeObject<BaseResponse<string>>(respondeEnvioToken.Content.ReadAsStringAsync().Result);

                var requestCDR = new CDRGuiaRemisionResquest
                {
                    token = resToken.Data,
                    ticket = NumeroTicket,
                    endPoint = $"https://api-cpe.sunat.gob.pe/v1/contribuyente/gem/comprobantes/envios"
                };

                var respondeCDR = await HttpRequestFactory.Post($"{parametro.UrlBaseServicioGRE}/CDRGuiaRemision", requestCDR);
                var resCDR = JsonConvert.DeserializeObject<BaseResponse<string>>(respondeCDR.Content.ReadAsStringAsync().Result);
                var objrespuestaEnvioDoc = new DocumentoElectronicoResponse().GenerarDocumentoRespuesta(resCDR.Data);
                response.TramaZipCdr = resCDR.Data;
                response.NroTicketCdr = NumeroTicket;
                response.MensajeRespuesta = objrespuestaEnvioDoc.MensajeRespuesta;
                response.CodigoRespuesta = objrespuestaEnvioDoc.CodigoRespuesta;
                response.Exito = objrespuestaEnvioDoc.Exito;
                response.Urldescarga = objrespuestaEnvioDoc.Urldescarga;


            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        public async Task<string> ValidarCPE(ConsultaCPERequest request)
        {
            string response;
            try
            {
                var respondeCDR = await HttpRequestFactory.Post($"http://www.apisunat.somee.com/api/ValidarCPE", request);
                var res = JsonConvert.DeserializeObject<BaseResponse<string>>(respondeCDR.Content.ReadAsStringAsync().Result);
                response = res.Data;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }


        public async Task<EFacturaElectronicaResponse> GuiaRemisionElectronicaXML(GuiaRemision objdocumento)
        {
            EParametro parametros = new BAdministracionSistema().listarParametro().FirstOrDefault();
            string path = string.Empty;
            path = parametros.DirecciónXML;
            string mensaje = string.Empty;
            string EndPointUrl = string.Empty;
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            try
            {
                string urlserviceGenerarTramaSinFirmar = parametros.urlServicioFacturaElectronica;
                string urlserviceFirmar = parametros.urlServicioFirma;
                string urlServiceEnviarDocumento = parametros.urlServicioEnviarDocumento;

                FirmadoRequest objFirmar = new FirmadoRequest();
                var responde = await HttpRequestFactory.Post("http://apisunat.somee.com/api/GenerarGuiaRemision", objdocumento);

                DocumentoResponse objrespuesta = JsonConvert.DeserializeObject<DocumentoResponse>(responde.Content.ReadAsStringAsync().Result);
                if (objrespuesta.Exito)
                {
                    string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                           $"{objdocumento.Remitente.NroDocumento}-{objdocumento.TipoDocumento}-{objdocumento.IdDocumento.Trim()}.xml");

                    File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuesta.TramaXmlSinFirma));

                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(rutaArchivo);

                    // Modificar el atributo "encoding" del nodo XmlDeclaration
                    XmlDeclaration xmlDeclaration = xmlDoc.FirstChild as XmlDeclaration;
                    if (xmlDeclaration != null)
                    {
                        xmlDeclaration.Encoding = "ISO-8859-1"; // Cambiar a la codificación deseada (por ejemplo, UTF-8)
                    }

                    // Modificar el atributo "standalone" del nodo XmlDeclaration
                    if (xmlDeclaration != null)
                    {
                        xmlDeclaration.Standalone = "no"; // Cambiar a "yes" o "no" según sea necesario
                    }

                    // Guardar los cambios en el archivo XML
                    xmlDoc.Save(rutaArchivo);
                    var tramaXmlSinFirma = Convert.ToBase64String(System.IO.File.ReadAllBytes(rutaArchivo));
                    //Firmar
                    objFirmar.CertificadoDigital = parametros.CertificadoDigital;
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = tramaXmlSinFirma,
                        // CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(($@"{objFirmar.CertificadoDigital}"))),// objFirmar.CertificadoDigital,
                        CertificadoDigital = WebDavTest.GetCertificado(parametros.DirecciónXML, Rutas.Cetificado),
                        PasswordCertificado = parametros.PasswordCertificado,//Contraseña para el certificado
                        UnSoloNodoExtension = true
                    };

                    var respondeFirma = await HttpRequestFactory.Post(urlserviceFirmar, firmadoRequest);
                    FirmadoResponse objrespuestaFirma = JsonConvert.DeserializeObject<FirmadoResponse>(respondeFirma.Content.ReadAsStringAsync().Result);

                    //Enviar el documento firmado
                    if (objrespuestaFirma.Exito)
                    {
                        File.WriteAllBytes(rutaArchivo, Convert.FromBase64String(objrespuestaFirma.TramaXmlFirmado));
                        response.TramaZipCdr = objrespuestaFirma.TramaXmlFirmado;
                        response.Exito = true;

                    }
                    else
                    {
                        response.MensajeError = objrespuestaFirma.MensajeError;
                        response.ProcesoFirmar = 1;
                        response.Exito = objrespuestaFirma.Exito;
                        response.CodigoRespuesta = objrespuestaFirma.MensajeError.Substring(7, 4);
                    }
                }
                else
                {
                    response.MensajeError = objrespuesta.MensajeError;
                    response.CodigoRespuesta = objrespuesta.MensajeError.Substring(7, 4);
                    response.ProcesoGenerar = 1;
                }
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Exito = false;
            }
            return response;
        }
        private void CrearArchivoZip(string archivoPlano, string rutaArchivoZip)
        {
            // Creamos un nuevo archivo ZIP y lo abrimos para escritura                                        
            using (var zipFile = new FileStream(rutaArchivoZip, FileMode.Create))
            {
                // Creamos el archivo ZIP usando ZipArchive
                using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
                {
                    // Agregamos el archivo plano al archivo ZIP
                    var entry = archive.CreateEntry(Path.GetFileName(archivoPlano));
                    using (var entryStream = entry.Open())
                    {
                        // Leemos el contenido del archivo plano y lo escribimos en el archivo ZIP
                        byte[] contenidoArchivo = File.ReadAllBytes(archivoPlano);
                        entryStream.Write(contenidoArchivo, 0, contenidoArchivo.Length);
                    }
                }
            }

        }

        public class CrdResponse
        {
            public string CodigoRespuesta { get; set; }
            public string MensajeRespuesta { get; set; }

            public string NroTicketCdr { get; set; }
            public string TramaZipCdr { get; set; }
            public string NombreArchivo { get; set; }
            public bool Exito { get; set; }
            public string Urldescarga { get; set; }
            public string MensajeError { get; set; }
            public string Pila { get; set; }
        }

        public CrdResponse GenerarDocumentoRespuesta(string constanciaRecepcion)
        {
            var response = new CrdResponse(); ;
            try
            {
                var returnByte = Convert.FromBase64String(constanciaRecepcion);
                using (var memRespuesta = new MemoryStream(returnByte))
                {
                    if (memRespuesta.Length <= 0)
                    {

                    }
                    else
                    {
                        using (var zipFile = new ZipArchive(memRespuesta, ZipArchiveMode.Read))
                        {
                            foreach (var entry in zipFile.Entries)
                            {
                                if (!entry.Name.EndsWith(".xml")) continue;
                                using (var ms = entry.Open())
                                {
                                    var responseReader = new StreamReader(ms);
                                    var responseString = responseReader.ReadToEnd();
                                    try
                                    {
                                        var xmlDoc = new XmlDocument();
                                        xmlDoc.LoadXml(responseString);

                                        var xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);

                                        xmlnsManager.AddNamespace("ar", EspacioNombres.ar);
                                        xmlnsManager.AddNamespace("cac", EspacioNombres.cac);
                                        xmlnsManager.AddNamespace("cbc", EspacioNombres.cbc);

                                        response.Urldescarga = xmlDoc.SelectSingleNode(EspacioNombres.nodoUrlDescarga, xmlnsManager)?.InnerText;
                                        response.CodigoRespuesta =
                                            xmlDoc.SelectSingleNode(EspacioNombres.nodoResponseCode,
                                                xmlnsManager)?.InnerText;

                                        response.MensajeRespuesta =
                                            xmlDoc.SelectSingleNode(EspacioNombres.nodoDescription,
                                                xmlnsManager)?.InnerText;

                                        response.NroTicketCdr =
                                            xmlDoc.SelectSingleNode(EspacioNombres.nodoId,
                                                xmlnsManager)?.InnerText;
                                        response.Exito = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        response.MensajeError = ex.Message;
                                        response.Pila = ex.StackTrace;
                                        response.Exito = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
                response.Exito = false;
            }
            return response;
        }

    }
}