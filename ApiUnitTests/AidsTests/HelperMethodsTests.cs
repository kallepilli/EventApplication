using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.Aids;

namespace ApiUnitTests.AidsTests
{
    [TestClass]
    public class HelperMethodsTests
    {
        private HelperMethods helper;

        [TestInitialize]
        public void TestInitialie()
        {
            helper = new HelperMethods();
        }

        [TestMethod]
        public void ValidateIdCode_ReturnsTrue()
        {
            List<string> validIdCodes = new List<string>()
            {
                "39208014225",
                "49208120218",
                "50212140157",
                "60706150163"
            };
            foreach (string validIdCode in validIdCodes)
            {
                var result = helper.ValidateIdCode(validIdCode);
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void ValidateIdCode_InvalidIdCode_ReturnsFalse()
        {
            List<string> inValidIdCodes = new List<string>()
            {
                "39208014226",
                "49208120219",
                "50212140158",
                "60706150164"
            };
            foreach (string inValidIdCode in inValidIdCodes)
            {
                var result = helper.ValidateIdCode(inValidIdCode);
                Assert.IsFalse(result);
            }
        }
    }
}
