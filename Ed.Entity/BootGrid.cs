﻿// ***********************************************************************
// Assembly         : Ed.T4
// Author           : tangyouwei
// Created          : 10-10-2014
//
// Last Modified By : tangyouwei
// Last Modified On : 10-10-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="BootGrid.cs" company="郑州优创软件科技有限公司">
//     Copyright (c) 优创科技. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// BootGrid返回结果封装类
/// </summary>

namespace Ed.Entity
{
    /// <summary>
    /// BootGrid返回结果封装类 14-09-28 By 唐有炜
    /// </summary>
    public class BootGrid
    {
        /// <summary>
        /// 页码
        /// </summary>
        /// <value>The current.</value>
        public int current { set; get; }

        /// <summary>
        /// 页数
        /// </summary>
        /// <value>The row count.</value>
        public int rowCount { set; get; }

        /// <summary>
        /// 记录
        /// </summary>
        /// <value>The rows.</value>
        public IEnumerable<object> rows { set; get; }

        /// <summary>
        /// 总数
        /// </summary>
        /// <value>The total.</value>
        public int total { set; get; }
    }
}