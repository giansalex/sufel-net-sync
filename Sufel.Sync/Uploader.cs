using System;
using Newtonsoft.Json.Linq;
using Sufel.Sync.Model;
using Sufel.Sync.Utils;

namespace Sufel.Sync
{
    public class Uploader
    {
        private static Jwt _jwt = new Jwt();

        public Jwt Token
        {
            get { return _jwt; }
        }

        public SufelSetting Setting { get; set; }

        /// <summary>
        /// Upload Xml and Pdf.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="pdf"></param>
        public void Upload(byte[] xml, byte[] pdf)
        {
            if (string.IsNullOrEmpty(_jwt.Token) || _jwt.Expire <= DateTime.Now)
            {
                Auth();
            }

            var obj = new JObject
            {
                {"xml", Convert.ToBase64String(xml) },
                {"pdf", Convert.ToBase64String(pdf) }
            };

            var url = Setting.Endpoint + "/api/company/add-document";
            HttpClient.Post(url, obj.ToString(), _jwt.Token);
        }

        /// <summary>
        /// Authentication in Sufel Api
        /// </summary>
        /// <returns></returns>
        public string Auth()
        {
            var url = Setting.Endpoint + "/api/company/auth";
            var obj = new JObject
            {
                {"ruc", Setting.Ruc}, {"password", Setting.Password}
            };
            var json = HttpClient.Post(url, obj.ToString());
            var jwt = JObject.Parse(json);
            _jwt.Token = (string)jwt["token"];
            _jwt.Expire = UnixTimeStampToDateTime((int)jwt["expire"]);

            return _jwt.Token;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
