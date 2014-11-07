// ***********************************************************************
// Assembly         : Ed.Dao
// Author           : tangyouwei
// Created          : 10-27-2014
//
// Last Modified By : tangyouwei
// Last Modified On : 10-28-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="ITOrderFinanceDao.cs" company="郑州优创软件科技有限公司">
//     Copyright (c) www.ucs123.com. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

/// <summary>
/// The Dao namespace.
/// </summary>
namespace Ed.Dao
{
    /// <summary>
    /// Interface ITOrderFinanceDao
    /// </summary>
public  interface ITOrderFinanceDao:ITableDao<TOrderFinance>
    {
        /// <summary>
        /// 使用LINQ批量更改TClientInfo状态 2014-09-05 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="fields">要更新的字段（支持批量更新）</param>
        /// <param name="values">The values.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateEntityStatus(List<Field> fields, params  object[] values);

        /// <summary>
        /// 月嫂收款 2014-10-28 10:55:32 By 唐有炜
        /// </summary>
        /// <param name="orderFinance">The order finance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool InsertEntityTran(TOrderFinance orderFinance);
	   }
	   }
