using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class TSysUserDaoTest
    {
        [TestMethod]
        public void ContextTest()
        {
            object target = ContextRegistry.GetContext().GetObject("sysUserDao");
            Assert.AreNotEqual(target, null);
        }
    }
}