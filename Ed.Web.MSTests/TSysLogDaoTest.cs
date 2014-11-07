using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class TSysLogDaoTest
    {
        [TestMethod]
        public void TestContext()
        {

            object target = ContextRegistry.GetContext().GetObject("sysLogDao");
            Assert.AreNotEqual(target, null);

        }
    }
}
