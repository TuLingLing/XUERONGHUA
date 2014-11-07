// ***********************************************************************
// Assembly         : Ed.Web
// Author           : Tangyouwei
// Created          : 2014-10-13 00:20:22
//
// Last Modified By : Tangyouwei
// Last Modified On : 2014-10-13 03:04:00
// ***********************************************************************
// <copyright file="MyHtmlHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Text;
using System.Web;
using Ed.Common;
using Ed.Entity;
using Newtonsoft.Json;
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="MyHtmlHelper.cs" company="优创科技">
//     Copyright (c) 优创科技. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Spring.Context;
using Spring.Context.Support;

/// <summary>
/// The Helpers namespace.
/// </summary>

namespace Ed.Web.Helpers
{
    /// <summary>
    /// Html格式化输出辅助类
    /// 2014-10-13 02:50:51 修改 By 唐有炜
    /// </summary>
    public static class MyHtmlHelper
    {
        #region 空值判断

        /// <summary>
        /// 自定义字符串长度 2014-05-25 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString IsNull(this HtmlHelper htmlHelper, string value, string replaceValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return MvcHtmlString.Create("");
            }
            else
            {
                return MvcHtmlString.Create(replaceValue);
            }
        }

        #endregion

        #region 加载默认值

        /// <summary>
        /// 加载默认图片 2014-05-25 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="src">The source.</param>
        /// <param name="imgUrl">The img URL.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString LoadDefaultPic(this HtmlHelper htmlHelper, string src, string imgUrl)
        {
            if (String.IsNullOrEmpty(src))
            {
                return MvcHtmlString.Create(imgUrl);
            }
            return MvcHtmlString.Create(src);
        }

        /// <summary>
        /// Loads the default value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString LoadDefaultValue(this HtmlHelper htmlHelper, string value, string replaceValue)
        {
            if (String.IsNullOrEmpty(value) || value == "0001-01-01"||value=="0")
            {
                return MvcHtmlString.Create(replaceValue);
            }
            return MvcHtmlString.Create(value);
        }



        /// <summary>
        /// Loads the default value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString LoadDefaultValue(this HtmlHelper htmlHelper, int? value, string replaceValue)
        {
            if (null == value || value == 0)
            {
                return MvcHtmlString.Create(replaceValue);
            }
            return MvcHtmlString.Create(value.ToString());
        }



        /// <summary>
        /// Loads the default value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString LoadDefaultValue(this HtmlHelper htmlHelper, double? value, string replaceValue)
        {
            if (null == value || value == 0.0)
            {
                return MvcHtmlString.Create(replaceValue);
            }
            return MvcHtmlString.Create(value.ToString());
        }

        /// <summary>
        /// Loads the default value.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString LoadDefaultValue(this HtmlHelper htmlHelper, dynamic obj, object value,
            string replaceValue)
        {
            if (null == obj || null == value)
            {
                return MvcHtmlString.Create(replaceValue);
            }
            return MvcHtmlString.Create(value.ToString());
        }

        #endregion

        #region 加载Null默认

        /// <summary>
        /// 加载默认 2014-05-25 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="field">The field.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString NullPalse(this HtmlHelper htmlHelper, object field)
        {
            if (null == field)
            {
                return MvcHtmlString.Create("");
            }
            return MvcHtmlString.Create(field.ToString());
        }

        /// <summary>
        /// 加载默认时间 2014-05-25 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="dt">The dt.</param>
        /// <returns>MvcHtmlString.</returns>
        public static DateTime DateTimeNullPalse(this HtmlHelper htmlHelper, DateTime? dt)
        {
            if (null == dt)
            {
                  return  DateTime.Parse("0001-01-01");
                //return DateTime.Now;
            }
            else if (dt.ToString() == "" )
            {
                return  DateTime.Parse("0001-01-01");
                //return DateTime.Now;
            }
            return DateTime.Parse(dt.ToString());
        }

        #endregion

        #region 下拉框初始化

        /// <summary>
        /// Builds the select list. 14-10-14 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="lists">下拉框集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="showLabel">是否显示"请选择",默认显示</param>
        /// <returns>IEnumerable&lt;SelectListItem&gt;.</returns>
        public static IEnumerable<SelectListItem> BuildSelectList(this HtmlHelper htmlHelper, string lists,
            string defaultValue,bool showLabel=true)
        {
            var items = new List<SelectListItem>();
            items.Clear();
            if (showLabel)
            {
                items.Add(new SelectListItem() { Text = "请选择", Value = "" });
            }
            

            if (String.IsNullOrEmpty(lists))
            {
                return items;
            }
            var tempItems =new object[]{lists};
            //修复只有一个选项的bug
            if (Utils.StringToObjectArray(lists, ',').Length>1)
            {
                tempItems = Utils.StringToObjectArray(lists, ',');
            }

            foreach (var tempItem in tempItems)
            {
                var itemArray = tempItem.ToString().Split('|');
                bool isSelected = false;
                if (itemArray[0] == defaultValue)
                {
                    isSelected = true;
                }
                items.Add(new SelectListItem() {Text = itemArray[1], Value = itemArray[0], Selected = isSelected});
            }

           var iremsJson= JsonConvert.SerializeObject(items);
           LogHelper.Debug(iremsJson);

            return items;
        }

        #endregion

        #region 下拉框输出

        /// <summary>
        /// 整型多选项输出 2014-07-29 By 唐有炜
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">当前值</param>
        /// <param name="options">选项1|a,2|b</param>
        /// <param name="replaceValue">The replace value.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString OptionsPalse(this HtmlHelper htmlHelper, int? value, string options,
            string replaceValue = "")
        {
            if (value != null)
            {
                string result = "";
                string[] str_arr = options.Split(',');
                foreach (var str in str_arr)
                {
                    if (value == int.Parse(str.Split('|')[0].ToString()))
                    {
                        result = str.Split('|')[1];
                    }
                }
                return MvcHtmlString.Create(result);
            }
            else
            {
                return MvcHtmlString.Create(replaceValue);
            }
        }

        #endregion

        #region 根据key获取Dictionary的值

        public static object GetValue(this HtmlHelper htmlHelper, Dictionary<string, object> dic,string key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return "";

            }
        }
        #endregion


        #region  模块权限判断
        /// <summary>
        /// 根据模块别名判断是否有权限
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="models"></param>
        /// <param name="mid">模块id</param>
        /// <param name="paction">操作（默认查看）</param>
        /// <returns></returns>
        public static bool LoadModulePowers(this HtmlHelper htmlHelper, List<Dictionary<string, object>> models, string mcode,string paction="view")
        {
            var roleId = int.Parse(HttpContext.Current.Session[EdKeys.SESSION_ROLE_ID].ToString());
            if (roleId==1)
            {
                return true;
            }

            if (null == models) {
                return false;
            }

            foreach (var model in models)
            {
                object mc = "";
                object pa = "";
                model.TryGetValue("MCode", out mc);
                model.TryGetValue("PowerAction", out pa);
                if (mc.ToString() == mcode&&pa.ToString()==paction)
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        #endregion


        #region 加密身份证

        public static MvcHtmlString EncodeIdcard(this HtmlHelper htmlHelper, string value)
        {
            var result = value;
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                var ch = value[i];
                if (i > 2 && i < value.Length - 3)
                {
                    sb.Append("*");
                }
                else
                {
                    sb.Append(value[i]);
                }
            }
            result = sb.ToString();
            return MvcHtmlString.Create(result);
        }

        #endregion


        
        #region 加密身份证

        public static MvcHtmlString EncodeName(this HtmlHelper htmlHelper, string value)
        {
            var result = value;
            if (String.IsNullOrEmpty(value))
            {
                return MvcHtmlString.Create("");
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                var ch = value[i];
                if (i >0)
                {
                    sb.Append("*");
                }
                else
                {
                    sb.Append(value[i]);
                }
            }
            result = sb.ToString();
            return MvcHtmlString.Create(result);
           
        }
        #endregion

    }
}