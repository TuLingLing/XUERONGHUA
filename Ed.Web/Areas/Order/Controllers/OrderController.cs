
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

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult My1()
        {
            return View("My1");
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

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult Edit(string order_code)
        {

            if (null == order_code)
            {
                ViewData["Msg"] = "参数非法（id不能为空）";
                return View("_Error");
            }
            else
            {
                TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);
                ViewBag.Order = order;
                return View("Edit");
            }

        }

        #region   查看页面  141225 By 屠玲玲

        //GET: URL   /Order/Order/ShowTransferUsers/
        [UserAuthorize]
        [HttpGet]
        public ActionResult ShowTransferUsers()
        {
            return View("ShowTransferUsers");
        }

        #endregion
     #endregion
    }
}
