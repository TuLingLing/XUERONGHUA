using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

namespace Ed.Dao
{
    public interface ITSysPowerDao : ITableDao<TSysPower>
    {

        #region 自定义接口

        /// <summary>
        /// （模块+权限）列表
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, object>> GetModulePowers(string selector, string predicate,
            params object[] values);

        #endregion


    }
}