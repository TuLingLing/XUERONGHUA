using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace Ed.Web.Tests
{
    [TestClass]
    public class TSysDepartmentDaoTest
    {
        [TestMethod]
        public void ContextTest()
        {
            object target = ContextRegistry.GetContext().GetObject("sysDepartmentDao");
            Assert.AreNotEqual(target, null);
        }
    }
}