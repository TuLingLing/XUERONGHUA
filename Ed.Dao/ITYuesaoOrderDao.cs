// ***********************************************************************
// Assembly         : Ed.Dao
// Author           : tangyouwei
// Created          : 10-27-2014
//
// Last Modified By : tangyouwei
// Last Modified On : 10-26-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="ITYuesaoOrderDao.cs" company="郑州优创软件科技有限公司">
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
    /// Interface ITYuesaoOrderDao
    /// </summary>
public  interface ITYuesaoOrderDao:ITableDao<TYuesaoOrder>
    {
    #region 手写的扩展

        /// <summary>
        /// 添加实体(事务控制先添加订单表，在添加月嫂订单表) 14-10-19 By 唐友伟
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="yuesaoOrder">The yuesao order.</param>
        /// <param name="orderFinance">The order finance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    bool InsertEntityTran(TOrder order, TYuesaoOrder yuesaoOrder, TOrderFinance orderFinance);



    /// <summary>
    /// 添加实体(事务控制先添加订单表，在添加月嫂订单表) 14-10-19 By 唐友伟
    /// </summary>
    /// <param name="order">The order.</param>
    /// <param name="yuesaoOrder">The yuesao order.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    bool UpdateEntityTran(TOrder order, TYuesaoOrder yuesaoOrder);



        /// <summary>
        /// 级联删除实体 14-10-28 By 唐友伟
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteEntityTran(string ids);

        #endregion

    }
	   }

