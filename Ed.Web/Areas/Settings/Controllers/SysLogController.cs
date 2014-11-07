using Ed.Web.Filters;
// ReSharper disable All 禁止ReSharper显示警告
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Service;
using Ed.Entity;

namespace Ed.Web.Areas.Settings.Controllers
{
public  class SysLogController: Controller
    {
	
	#region  依赖注入 2014-10-13 18:51:08 By 唐有炜
	public ISysLogService  SysLogService{set;get;}
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

        #region 添加、修改页面  

        ////GET: URL
		////actionType (Add,Edit)
        [UserAuthorize]
        [HttpGet]
        public ActionResult Edit(string actionType, int? id )
        {
            //修改
            if (actionType == EdEnums.ActionEnum.Edit.ToString())
            {
			   if (null == id)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }
                var model = SysLogService.GetSysLog(temp=> temp.Id == id);
                return View("Edit", model);
            }
            //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
                return View("Edit", new TSysLog());
            }
            else
            {
		        ViewData["Msg"] = "参数非法（actipnType不能为空）";
                 return View("_Error");
            }
        }

        #endregion

        //#region   查看页面  
        ////GET: URL
        ////[UserAuthorize]
        //[HttpGet]
        //public ActionResult Show(int? id)
        //{
		//if (null== id)
        //   {
		//     ViewData["Msg"] = "参数非法！";
        //       return View("_Error");
        //   }
        //var model = SysDepartmentService.GetSysDepartment(d => d.Id == id);
        //return View("Show", model);
        //}
        //#endregion

        #endregion

	}
}
