
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

namespace Ed.Web.Areas.Yuesao.Controllers
{
public  class PregnanterServiceRecordController: Controller
    {
	
	#region  依赖注入 2014-10-16 17:25:28 By 唐有炜
    public ISysUserService SysUserService { set; get; }
	public IPregnanterServiceRecordService  PregnanterServiceRecordService{set;get;}
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
        //id 服务记录id  pid 月嫂id(添加月嫂时不显示服务日程)
        [HttpGet]
        public ActionResult Edit(string actionType, int? id,int? pid)
        {
            

            //修改
            if (actionType == EdEnums.ActionEnum.Edit.ToString())
            {
			   if (null == id)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }
                var model = PregnanterServiceRecordService.GetPregnanterServiceRecord(temp=> temp.Id == id);
                //月嫂id
                ViewBag.Pid = pid;
               //查询创建者的信息
                var sysUser = SysUserService.GetFields("new(Id,UserLname,UserTname)", String.Concat(new[] { "Id=", model.PServiceCreater.ToString() })).ToList().FirstOrDefault();
                ViewBag.SysUser = sysUser;
                return View("Edit", model);
            }
            //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
		      	var  pregnanterservicerecord=new TPregnanterServiceRecord();
                //月嫂id
                ViewBag.Pid = pid;
                //默认类型为日程
                pregnanterservicerecord.PServiceType = 1;
                //默认服务类型为处理中
                pregnanterservicerecord.PServiceState = 1;
                //服务号码
                pregnanterservicerecord.PServiceCode = "FW"+RandomHelper.GetRamCode();
                //返回当前登录用户（同时也是创建者）
                ViewBag.SysUser = new TSysUser() { Id = int.Parse(Session[EdKeys.SESSION_USER_ID].ToString()), UserTname = Session[EdKeys.SESSION_USER_NAME].ToString() };
                return View("Edit",pregnanterservicerecord );
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
