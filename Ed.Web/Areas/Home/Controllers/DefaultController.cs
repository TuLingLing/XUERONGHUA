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
    public class DefaultController : Controller
    {
        
        //
        // GET: /Home/Default/1
        //默认页
        [UserAuthorize]
        [RolePowerAuthorize]
        public ActionResult Index()
        {
            //接收RolePowerAuthorize特性里面设置的模块权限集合
            var powers =(List<Dictionary<string,object>>) ViewBag.Powers;
            return View(powers);
        }


      

    }
}