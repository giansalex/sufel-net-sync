﻿using System.IO;
using System.Net;
using System.Text;

namespace Sufel.Sync.Utils
{
    /// <summary>
    /// Class HttpClient.
    /// </summary>
    internal static class HttpClient
    {
        /// <summary>
        /// POST the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="token">The token.</param>
        /// <returns>System.String.</returns>
        public static string Post(string url, string data, string token = "")
        {
            return Request("POST", url, data, token);
        }

        /// <summary>
        /// PATCH the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="token">The token.</param>
        /// <returns>System.String.</returns>
        public static string Patch(string url, string data, string token = "")
        {
            return Request("PATCH", url, data, token);
        }

        public static string Request(string method, string url, string data, string token = "")
        {
            var bytes = Encoding.UTF8.GetBytes(data); 
            var http = (HttpWebRequest)WebRequest.Create(url);
            http.Method = method;
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
