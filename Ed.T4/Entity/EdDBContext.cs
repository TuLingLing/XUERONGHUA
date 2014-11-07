
/*
 * ========================================================================
 * Copyright(c) 2013-2014 郑州优创科技有限公司, All Rights Reserved.
 * ========================================================================
 *  
 * 【Ed数据库操作上下文】
 *  
 *  
 * 作者：唐有炜   时间：2014-10-29 22:01:50
 * 文件名：@dbContextName
 * 版本：V1.0.0
 * 
 * 修改者：唐有炜           时间：2014-10-29 22:01:50            
 * 修改说明：修改说明
 * ========================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
using Ed.Entity;
using NLite.Reflection;
namespace Ed.DBContext
{
	public partial class EdDBContext:DbContext
	{
	    #region 初始化上下文
		//连接字符串名称：基于Config文件中连接字符串的配置
        const string connectionStringName = "EdSqlServer";

        //构造dbConfiguration 对象
        static DbConfiguration dbConfiguration;

		static EdDBContext()
		{
			 dbConfiguration = DbConfiguration
                  .Configure(connectionStringName)
                  .SetSqlLogger(() =>SqlLog.Debug)
				  .AddFromAssemblyOf<EdDBContext>(t=>t.HasAttribute<TableAttribute>(false))
				  ;
		}

		public EdDBContext():base(dbConfiguration){}
		#endregion

		#region 数据集关联
		public IDbSet<TClientInfo> TClientInfos { get; private set; }
		public IDbSet<TClientMaintenance> TClientMaintenances { get; private set; }
		public IDbSet<TOrder> TOrders { get; private set; }
		public IDbSet<TOrderFinance> TOrderFinances { get; private set; }
		public IDbSet<TPregnanterEvaluation> TPregnanterEvaluations { get; private set; }
		public IDbSet<TPregnanterInfo> TPregnanterInfos { get; private set; }
		public IDbSet<TPregnanterMaintenance> TPregnanterMaintenances { get; private set; }
		public IDbSet<TPregnanterServiceRecord> TPregnanterServiceRecords { get; private set; }
		public IDbSet<TSysDepartment> TSysDepartments { get; private set; }
		public IDbSet<TSysLog> TSysLogs { get; private set; }
		public IDbSet<TSysModule> TSysModules { get; private set; }
		public IDbSet<TSysPower> TSysPowers { get; private set; }
		public IDbSet<TSysRole> TSysRoles { get; private set; }
		public IDbSet<TSysUser> TSysUsers { get; private set; }
		public IDbSet<TYuesaoOrder> TYuesaoOrders { get; private set; }
        #endregion
	}
	

  
}




