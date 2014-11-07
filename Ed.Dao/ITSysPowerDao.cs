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

        #region �Զ���ӿ�

        /// <summary>
        /// ��ģ��+Ȩ�ޣ��б�
        /// </summary>
        /// <returns></returns>
        List<Dictionary<string, object>> GetModulePowers(string selector, string predicate,
            params object[] values);

        #endregion


    }
}