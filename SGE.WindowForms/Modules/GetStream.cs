using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SGE.WindowForms.Modules
{
    public static class GetStream
    {
        public async static Task<Stream> Get(string file)
        {
            var docFromRequest = Convert.ToBase64String(File.ReadAllBytes(file));
            byte[] docStringToBase64 = Convert.FromBase64String(docFromRequest);
            StreamContent streamContent = new StreamContent(new MemoryStream(docStringToBase64));
            return await streamContent.ReadAsStreamAsync();
        }
    }
}
