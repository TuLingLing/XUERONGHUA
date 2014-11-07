using System.Collections.Generic;
using System.Web.Mvc;
using Ed.Entity;
using Ed.Service;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Customer.Controllers
{
    public class IndexController : Controller
    {
  
        
        #region 客户首页

        //
        // GET: /Customer/Index/
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Index()
        {
            //接收RolePowerAuthorize特性里面设置的模块权限集合
            var powers = (List<Dictionary<string, object>>)ViewBag.Powers;
            return View(powers);
        }

        #endregion


    }
}
