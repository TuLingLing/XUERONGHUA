using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Order.Controllers
{
    public class IndexController : Controller
    {
        #region 订单首页

        //
        // GET: /Order/Index/
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
