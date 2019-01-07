using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sufel.Sync.Tests
{
    [TestClass]
    public class UploaderTest
    {
        [TestMethod]
        public void UploadTest()
        {
            var uploader = new Uploader
            {
                Setting = new Model.SufelSetting
                {
                    Ruc = "20123456789",
                    Password = "",
                    Endpoint = ""
                }
            };
        }
    }
}
