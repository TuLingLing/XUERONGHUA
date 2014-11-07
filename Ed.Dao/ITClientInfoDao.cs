
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
    /// ʹ��LINQ��������TClientInfo״̬ 2014-09-05 14:58:50 By �����
    /// </summary>
    /// <param name="fields">Ҫ���µ��ֶΣ�֧���������£�</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    bool UpdateEntityStatus(List<Field> fields, params  object[] values);


    /// <summary>
    /// ��ȡ���Id
    /// </summary>
    /// <returns></returns>
    int GetMaxId();
}
	   }
