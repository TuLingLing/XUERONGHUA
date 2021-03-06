﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ed.Entity;


namespace Ed.Service
{
    /// <summary>
    /// 账户操作接口。
    /// </summary>
 public   interface IAccountService
    {
        /// <summary>
        /// 账户验证 2014/8/21 9:04:10   By 唐有炜
        /// </summary>
        /// <param name="type">注册或登录方式（normal,qrcode,usb,footprint）</param>
        /// <param name="accountType">账号类型（username,email,phone）</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// <returns>ResponseMessage</returns>
         ResponseMessage ValidateAccount(string type,string accountType,string userName,string userPassword=null);


        /// <summary>
        /// 登录验证并写入登录日志 2014-08-21 07:58:50 By 唐有炜
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="type">注册或登录方式（normal,qrcode,usb,footprint）</param>
        /// <param name="accountType">账号类型（username,email,phone）</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// <param name="remember">记住密码</param>
        /// <param name="clientIp">客户端ip地址</param>
        /// <param name="clientPlace">客户端地址</param>
        /// <param name="clientTime">客户端登录时间</param>
        /// <returns>ResponseMessage</returns>
        ResponseMessage Login(HttpContext httpContext, string type, string accountType, string userName, string userPassword, string remember, string clientIp, string clientPlace, string clientTime);

      
 }
}
