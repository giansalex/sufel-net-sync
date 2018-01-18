using System;
using Newtonsoft.Json.Linq;
using Sufel.Sync.Model;
using Sufel.Sync.Utils;

namespace Sufel.Sync
{
    /// <summary>
    /// Class Uploader.
    /// </summary>
    public class Uploader
    {
        private static Jwt _jwt = new Jwt();

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        public Jwt Token
        {
            get { return _jwt; }
        }

        /// <summary>
        /// Gets or sets the setting.
        /// </summary>
        /// <value>The setting.</value>
        public SufelSetting Setting { get; set; }

        /// <summary>
        /// Upload Xml and Pdf.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="pdf"></param>
        /// <returns>xml and pdf links</returns>
        public string[] Upload(byte[] xml, byte[] pdf)
        {
            VerifyAuth();

            var obj = new JObject
            {
                {"xml", Convert.ToBase64String(xml) },
                {"pdf", Convert.ToBase64String(pdf) }
            };

            var url = CreateUrl("/api/company/add-document");
            var json = HttpClient.Post(url, obj.ToString(), _jwt.Token);

            var res = JObject.Parse(json);

            return new []{ (string)res["xml"], (string)res["pdf"]};
        }

        /// <summary>
        /// Anula document in server.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="serie"></param>
        /// <param name="correlativo"></param>
        public void Anular(string tipo, string serie, string correlativo)
        {
            VerifyAuth();

            var obj = new JObject
            {
                {"tipo", tipo },
                {"serie", serie },
                {"correlativo", correlativo }
            };

            var url = CreateUrl("/api/company/cancel-document");
            HttpClient.Post(url, obj.ToString(), _jwt.Token);
        }

        /// <summary>
        /// Authentication in Sufel Api
        /// </summary>
        /// <returns></returns>
        public string Auth()
        {
            var url = CreateUrl("/api/company/auth");
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

        private void VerifyAuth()
        {
            if (string.IsNullOrEmpty(_jwt.Token) || _jwt.Expire <= DateTime.Now)
            {
                Auth();
            }
        }

        private string CreateUrl(string path)
        {
            return Setting.Endpoint + path;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
