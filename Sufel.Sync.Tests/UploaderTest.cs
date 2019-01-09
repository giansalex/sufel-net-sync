using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace Sufel.Sync.Tests
{
    [TestFixture]
    public class UploaderTest
    {
        [Test]
        public void UploadTest()
        {
            var uploader = new Uploader
            {
                Setting = new Model.SufelSetting
                {
                    Ruc = "20123456789",
                    Password = "123456",
                    Endpoint = "http://localhost:8090"
                }
            };

            var xml = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "20123456789-01-F001-123.xml"));
            var pdf = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "20123456789-01-F001-123.pdf"));

            try
            {
                var result = uploader.Upload(xml, pdf);

                Assert.AreEqual(2, result.Length);
            }
            catch (WebException ex)
            {
                var response = ex.Response?.GetResponseStream();
                if (response != null)
                {
                    var resp = new StreamReader(response).ReadToEnd();
                    TestContext.Out.WriteLine(resp);
                }
            }
            catch (Exception e)
            {
                TestContext.Out.WriteLine(e);
            }
        }
    }
}
