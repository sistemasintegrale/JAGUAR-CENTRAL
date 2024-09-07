using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SGE.Common
{
    public static class API_DNI
    {
        public static async Task<DataResponse> ConsultaDni(string dni)
        {
            DataResponse response = new DataResponse();
            try
            {
                string apiUrl = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InJhZmhhZWxwaWxsYWNhMjAwMUBnbWFpbC5jb20ifQ.GuLc-hX5YfWY21wARZNJs86878m9CMxZ-vSZDKfci7w";
                using (HttpClient httpClient = new HttpClient())
                {
                    // Realizar la solicitud HTTP GET
                    HttpResponseMessage resp = await httpClient.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa (código 200 OK)
                    if (resp.IsSuccessStatusCode)
                    {
                        // Leer y mostrar la respuesta
                        string responseData = await resp.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<DataResponse>(responseData);
                    }
                    else
                    {
                        // Si la solicitud no fue exitosa, mostrar el código de estado
                        Console.WriteLine($"Error en la solicitud. Código de estado: {resp.StatusCode}");
                    }

                }
            }
            catch
            {

                return response;
            }

            return response;
        }
    }

    public class DataResponse
    {
        public bool Success { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CodVerifica { get; set; }
    }
}
