using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class TClientInfoDaoTest
    {
        [TestMethod]
        public void ContextTest()
        {
            object target = ContextRegistry.GetContext().GetObject("clientInfoService");
            Assert.AreNotEqual(target, null);
        }
    }
}