using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Entity;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Settings.Controllers
{
    public class DataBackupController : Controller
    {
        #region 列表

        // GET: /Settings/DataBackup/
        [UserAuthorize]
        public ActionResult List()
        {
            return View();
        }

        #endregion


        
    }
}
