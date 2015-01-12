using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Entity;
using Ed.Service;
using Ed.Web.Filters;

namespace Ed.Web.Areas.Yingshi.Controllers
{
    public class YPregnanterEvaluationController : Controller
    {
        //
        // GET: /Yingshi/YPregnanterEvaluationApi/

        #region  依赖注入 2014-10-16 17:25:28 By 唐有炜
        public ISysUserService SysUserService { set; get; }
        public IPregnanterEvaluationService PregnanterEvaluationService { set; get; }
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
        public ActionResult Edit(string actionType, int? id, int? pid)
        {
            //修改
            if (actionType == EdEnums.ActionEnum.Edit.ToString())
            {
                if (null == id)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }
                var model = PregnanterEvaluationService.GetPregnanterEvaluation(temp => temp.Id == id);
                //月嫂id
                ViewBag.Pid = pid;
                //查询创建者的信息
                var sysUser = SysUserService.GetFields("new(Id,UserLname,UserTname)", String.Concat(new[] { "Id=", model.PEvaCreater.ToString() })).ToList().FirstOrDefault();
                ViewBag.SysUser = sysUser;
                return View("Edit", model);
            }
            //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
                var pregnanterevaluation = new TPregnanterEvaluation();
                //默认一星级
                pregnanterevaluation.PEvaLevel = 1;
                //月嫂id
                ViewBag.Pid = pid;
                //返回当前登录用户（同时也是创建者）
                ViewBag.SysUser = new TSysUser() { Id = int.Parse(Session[EdKeys.SESSION_USER_ID].ToString()), UserTname = Session[EdKeys.SESSION_USER_NAME].ToString() };
                return View("Edit", pregnanterevaluation);
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
