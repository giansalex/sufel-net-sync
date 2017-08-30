using System.IO;
using System.Net;
using System.Text;

namespace Sufel.Sync.Utils
{
    internal static class HttpClient
    {
        public static string Post(string url, string data, string token = "")
        {
            var bytes = Encoding.UTF8.GetBytes(data); 
            var http = (HttpWebRequest)WebRequest.Create("");
            http.Method = "POST";
            http.ContentType = "application/json";
            http.ContentLength = bytes.Length;
            if (!string.IsNullOrEmpty(token))
            {
                http.Headers.Add("Authorization", "Bearer " + token);
            }

            using (var wr = http.GetRequestStream())
            {
                wr.Write(bytes, 0, bytes.Length);
            }
            var resp = (HttpWebResponse)http.GetResponse();

            using (var rd = new StreamReader(resp.GetResponseStream()))
            {
                return rd.ReadToEnd();
            }
        }
    }
}
