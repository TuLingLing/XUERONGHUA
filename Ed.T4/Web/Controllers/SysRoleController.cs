
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

namespace Ed.Web
{
public  class SysRoleController: Controller
    {
	
	#region  依赖注入 2014-10-28 11:02:26 By 唐有炜
	public ISysRoleService  SysRoleService{set;get;}
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
		    //actionType
            ViewBag.ActionType = actionType;

            //修改
            if (actionType == EdEnums.ActionEnum.Edit.ToString())
            {
			   if (null == id)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }
                var model = SysRoleService.GetSysRole(temp=> temp.Id == id);
                return View("Edit", model);
            }
            //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
		      	var  sysrole=new TSysRole();
                return View("Edit",sysrole );
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
