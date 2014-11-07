using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Finance.Controllers
{
    public class FinanceController : Controller
    {
        //
        // GET: /Finance/Finance/
        [UserAuthorize]
        public ActionResult List()
        {
            return View();
        }

    }
}
