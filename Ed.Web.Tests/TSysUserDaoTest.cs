using System;
using Ed.Dao;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Testing.NUnit;

namespace Ed.Web.Tests
{
    public class TSysUserDaoTest : AbstractDependencyInjectionSpringContextTests
    {
        // 注入的属性 
        public ITSysUserDao SysUserDao { set; get; }

        [Test]
        public void TSysUserDaoContextTest()
        {
            var sysUserDao = this.SysUserDao;
            //var users=sysUserDao.GetList(u => u.Id > 0);
            //Assert.IsNotNull(users);
            Assert.IsNotNull(sysUserDao);
        }

        /// <summary>
        /// 重载指明上下文
        /// </summary>
        protected override string[] ConfigLocations
        {
            get
            {
                return new String[] {"file:///D:/MyCode/Edelweiss/Ed.Web/Configs/Daos.xml"};
            }
        }
    }
}