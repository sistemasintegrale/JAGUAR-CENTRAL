using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SGE.BusinessLogic
{
    public class BFirebaseStorage
    {
        public async Task<string> SubirStorage(Stream archivo, string nombreArchivo, string nombreCarpeta)
        {
            string email = "codigo@gmail.com";
            string clave = "codigo123";
            string ruta = "docs-gc.appspot.com";
            string api_key = "AIzaSyCOQH7SHIPQKPE99_OhiBuRuzyXvfno9qc";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
            .Child(nombreCarpeta)
            .Child(nombreArchivo.Trim().Replace("  ", "").ToString())
            .PutAsync(archivo, cancellation.Token);

            var dowloadURL = await task;

            return dowloadURL;

        }

        public async Task<Tuple<bool, string>> EliminarStorage(string nombre, string nombreCarpeta)
        {
            try
            {
                string email = "codigo@gmail.com";
                string clave = "codigo123";
                string ruta = "corefirebase-12055.appspot.com";
                string api_key = "AIzaSyCFZlHzrtHBVyXQgyxs82BE4wgJkw_0KIc";

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                .Child(nombreCarpeta)
                .Child(nombre)
                .DeleteAsync();
                return new Tuple<bool, string>(true, "succes");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public async Task<string> Documentos_Guadar(string file, string carpeta)
        {
            try
            {
                string[] nombre = file.Split('\\');
                var docFromRequest = Convert.ToBase64String(File.ReadAllBytes(file));
                byte[] docStringToBase64 = Convert.FromBase64String(docFromRequest);
                StreamContent streamContent = new StreamContent(new MemoryStream(docStringToBase64));
                Stream stream = await streamContent.ReadAsStreamAsync();
                return await new BFirebaseStorage().SubirStorage(stream, nombre[nombre.Count() - 1], carpeta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> Documentos_Modificar(string fileName, string carpeta, string fileAnterior)
        {
            try
            {
                await new BFirebaseStorage().EliminarStorage(fileAnterior, carpeta);
                return await Documentos_Guadar(fileName, carpeta);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
