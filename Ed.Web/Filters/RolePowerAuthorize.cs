using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Entity;
using Ed.Service;
using Spring.Context.Support;

namespace Ed.Web.Filters
{
    /// <summary>
    /// 角色模块权限过滤器 14-10-24 by 唐有炜
    /// </summary>
    public class RolePowerAuthorize: FilterAttribute,IActionFilter
    {

        #region  依赖注入 2014-10-20 16:51:58 By 唐有炜
         private readonly ISysUserService SysUserService = (ISysUserService)ContextRegistry.GetContext().GetObject("sysUserService");
         private readonly ISysRoleService SysRoleService = (ISysRoleService)ContextRegistry.GetContext().GetObject("sysRoleService");
         private readonly ISysPowerService SysPowerService = (ISysPowerService)ContextRegistry.GetContext().GetObject("sysPowerService");
        #endregion

         #region   页面变量定义

         private List<Dictionary<string, object>> moudlsPowers = null;

         #endregion

        //获取当前角色的所有模块权限集合并存储到ViewBag
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int roleId = int.Parse(HttpContext.Current.Session[EdKeys.SESSION_ROLE_ID].ToString());
            moudlsPowers = SysPowerService.GetModulePowers("", "", new {});
            moudlsPowers = Ed.Web.Helpers.MyControllerHelper.FilterModulePowers(moudlsPowers, roleId);
            filterContext.Controller.ViewBag.Powers = moudlsPowers;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}