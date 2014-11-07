﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Common;
using Ed.Entity;

namespace Ed.Web.Filters
{
    /// <summary>
    ///自动登录过滤器 2014-08-22 14:58:50 By 唐有炜
    /// </summary>
    public class AutoLoginAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //检查是否登录
            if (filterContext.HttpContext.Session[EdKeys.SESSION_USER_ID] != null && filterContext.HttpContext.Session[EdKeys.SESSION_USER_NAME]!=null)
            {
                //跳转到登录页面
                filterContext.HttpContext.Response.Redirect("/");
            }
        }
    }
}