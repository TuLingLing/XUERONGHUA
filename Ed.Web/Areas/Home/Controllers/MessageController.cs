using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ed.Web.Areas.Home.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Home/Message/Timeout/

        #region 超时
        //
        // GGET: /Home/Message/Timeout/
        public ActionResult TimeOut()
        {
            return View("_TimeOut");
        }
        #endregion

        public ActionResult Yuangong()
        {
            Response.Redirect("Home/Main/Yuangong");
            return View();
        }

        public ActionResult Yingshi()
        {
            Response.Redirect("Yingshi/Index/Index");
            return View();
        }  
    }
}
