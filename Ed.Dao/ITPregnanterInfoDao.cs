
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

namespace Ed.Dao
{
public  interface ITPregnanterInfoDao:ITableDao<TPregnanterInfo>
{
    /// <summary>
    /// 使用LINQ批量更改T状态 2014-09-05 14:58:50 By 唐有炜
    /// </summary>
    /// <param name="fields">要更新的字段（支持批量更新）</param>
    /// <param name="predicate">条件</param>
    /// <returns></returns>
    bool UpdateEntityStatus(List<Field> fields,
        params object[] values);



    /// <summary>
    /// 获取最大Id
    /// </summary>
    /// <returns></returns>
    int GetMaxId();
}
	   }
