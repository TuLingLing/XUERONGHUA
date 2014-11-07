using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Common;
using Ed.Entity;

namespace Ed.Web.Filters
{
    /// <summary>
    ///账户授权过滤器 2014-08-22 14:58:50 By 唐有炜
    /// </summary>
    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //检查是否登录
            if (filterContext.HttpContext.Session[EdKeys.SESSION_USER_ID] == null ||
                filterContext.HttpContext.Session[EdKeys.SESSION_USER_NAME] == null ||
                filterContext.HttpContext.Session[EdKeys.SESSION_ROLE_ID] == null ||
                filterContext.HttpContext.Session[EdKeys.SESSION_ROLE_NAME] == null)
            {
                string redirectUrl = "/Home/Message/Timeout/";
                object area = "";
                filterContext.RouteData.DataTokens.TryGetValue("area", out area);
                var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var action = filterContext.ActionDescriptor.ActionName;
                //移除前缀
                //默认首页不现实登陆超时
                if (area.ToString() == "Home" && controller == "Default" && action == "Index")
                {
                    redirectUrl = "/Account/Login";
                }

                filterContext.Result = new HttpUnauthorizedResult(); //返回未授权Result
                //跳转到登录页面
                filterContext.HttpContext.Response.Redirect(redirectUrl);
            }
           
        }
    }
}