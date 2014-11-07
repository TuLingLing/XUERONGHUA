
// ReSharper disable All 禁止ReSharper显示警告
using Ed.Common;
using Ed.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Service;
using Ed.Entity;

namespace Ed.Web.Areas.Order.Controllers
{
public  class OrderController: Controller
    {
	
	#region  依赖注入 2014-10-20 21:14:14 By 唐有炜
	public IOrderService  OrderService{set;get;}
	#endregion

     #region 页面展示  2014-10-11 10:21:24 By 唐有炜

        #region 首页

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult List()
        {
            return View("List");
        }

        #endregion

        #region 我的订单

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult My()
        {
            return View("My");
        }

        #endregion



        #region 待处理订单

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult Do()
        {
            return View("do");
        }

        #endregion


        #region 已作废订单

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult Out()
        {
            return View("Out");
        }

        #endregion



     #endregion
    }
}
