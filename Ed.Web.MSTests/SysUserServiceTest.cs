using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class SysUserServiceTest
    {
        [TestMethod]
        public void ContextTest()
        {

            object target = ContextRegistry.GetContext().GetObject("sysUserService");
            Assert.AreNotEqual(target, null);


        }
    }
}
