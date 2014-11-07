using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class SysDepartmentServiceTest
    {
        [TestMethod]
        public void ContextTest()
        {

            object target = ContextRegistry.GetContext().GetObject("sysDepartmentService");
            Assert.AreNotEqual(target, null);


        }
    }
}
