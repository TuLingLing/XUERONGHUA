
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

namespace Ed.Dao
{
    public interface ITOrderDao : ITableDao<TOrder>
    {
        /// <summary>
        /// 使用LINQ批量更改TClientInfo状态 2014-09-05 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="fields">要更新的字段（支持批量更新）</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateEntityStatus(List<Field> fields, params  object[] values);

        /// <summary>
        /// 添加实体(事务控制先添加订单表，在添加月嫂订单表) 14-10-19 By 唐友伟
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="yuesaoOrder">The yuesao order.</param>
        /// <param name="orderFinance">The order finance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool InsertEntityTran(TOrder order, TOrderFinance orderFinance);
    }
}