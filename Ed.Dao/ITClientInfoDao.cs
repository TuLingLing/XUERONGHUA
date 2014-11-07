
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

namespace Ed.Dao
{
public  interface ITClientInfoDao:ITableDao<TClientInfo>
{

    /// <summary>
    /// 使用LINQ批量更改TClientInfo状态 2014-09-05 14:58:50 By 唐有炜
    /// </summary>
    /// <param name="fields">要更新的字段（支持批量更新）</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    bool UpdateEntityStatus(List<Field> fields, params  object[] values);


    /// <summary>
    /// 获取最大Id
    /// </summary>
    /// <returns></returns>
    int GetMaxId();
}
	   }
