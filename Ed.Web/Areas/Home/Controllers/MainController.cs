using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Entity;
using Ed.Service;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Home.Controllers
{
    public class MainController : Controller
    {

        //
        // GET: /Home/Main/Index/1
        //1 客户管理 2 订单管理 3 月嫂管理 4 财务信息 5 系统设置
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Index(int? id)
        {
            //接收RolePowerAuthorize特性里面设置的模块权限集合
            var powers = (List<Dictionary<string, object>>)ViewBag.Powers;
            ViewBag.MoudleId = id;
            return View(powers);
        }
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Customer(int? id)
        {
            var customer = (List<Dictionary<string, object>>)ViewBag.Powers;
            ViewBag.MoudleId = id;
            return View(customer);

        }
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Order(int? id)
        {
            var order = (List<Dictionary<string, object>>)ViewBag.Powers;
            ViewBag.MoudleId = id;
            return View(order);

        }
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Yuesao()
        {
            var powers = (List<Dictionary<string, object>>)ViewBag.Powers;
            return View(powers);

        }
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Finance(int? id)
        {
            var finance = (List<Dictionary<string, object>>)ViewBag.Powers;
            ViewBag.MoudleId = id;
            return View(finance);

        }
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Seting(int? id)
        {
            var seting = (List<Dictionary<string, object>>)ViewBag.Powers;
            ViewBag.MoudleId = id;
            return View(seting);

        }
    }
}
