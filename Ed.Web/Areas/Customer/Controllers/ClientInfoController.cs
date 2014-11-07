using Ed.Common;
using Ed.Web.Filters;
// ReSharper disable All 禁止ReSharper显示警告
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Service;
using Ed.Entity;

namespace Ed.Web.Areas.Customer.Controllers
{
    public class ClientInfoController : Controller
    {
        #region  依赖注入 2014-10-12 11:32:47 By 唐有炜

        public ISysUserService SysUserService { set; get; }
        public IClientInfoService ClientInfoService { set; get; }

        #endregion

        #region 页面展示  2014-10-11 10:21:24 By 唐有炜

        #region 首页

        // GET: URL
        [HttpGet]
        [UserAuthorize]
        public ActionResult List()
        {
            return View("List");
        }

        #endregion

        #region 我的业务客户

        // GET: URL
        //[UserAuthorize]
        [HttpGet]
        [UserAuthorize]
        public ActionResult Business()
        {
            return View("Business");
        }

        #endregion

        #region 我的维护客户

        // GET: URL
        [HttpGet]
        [UserAuthorize]
        public ActionResult Maintenance()
        {
            return View("Maintenance");
        }

        #endregion

        #region 首页

        // GET: URL
        //[UserAuthorize]
        [HttpGet]
        [UserAuthorize]
        public ActionResult Trash()
        {
            return View("Trash");
        }

        #endregion

        #region 添加、修改页面  

        ////GET: URL
        ////actionType (Add,Edit)
        [UserAuthorize]
        [HttpGet]
        public ActionResult Edit(string actionType, int? id)
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
                var model = ClientInfoService.GetClientInfo(temp => temp.Id == id);
                //查询创建者的信息
                var sysUser =
                    SysUserService.GetFields("new(Id,UserLname,UserTname)",
                        String.Concat(new[] {"Id=", model.ClientCreater.ToString()})).ToList().FirstOrDefault();
                ViewBag.SysUser = sysUser;
                return View("Edit", model);
            }
                //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
                var clientInfo = new TClientInfo();
                //返回当前登录用户（同时也是创建者）
                ViewBag.SysUser = new TSysUser()
                {
                    Id = int.Parse(Session[EdKeys.SESSION_USER_ID].ToString()),
                    UserTname = Session[EdKeys.SESSION_USER_NAME].ToString()
                };
                return View("Edit", clientInfo);
            }
            else
            {
                ViewData["Msg"] = "参数非法（actipnType不能为空）";
                return View("_Error");
            }
        }

        #endregion

        #region   查看页面  141028 By 唐有炜

        //GET: URL   /Customer/ClientInfo/ShowTransferUsers/
        [UserAuthorize]
        [HttpGet]
        public ActionResult ShowTransferUsers()
        {
            return View();
        }

        #endregion

        #endregion
    }
}