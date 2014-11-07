﻿
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NLite.Data;
using NLite.Data.CodeGeneration;
using NLite.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ed.Common;
using Ed.DBContext;
using Ed.Entity;
using System.Linq.Dynamic;

namespace  Ed.Dao.Impl
{

    /// <summary>
    /// 自动生成的实现ITSysDepartmentDao接口的Dao类。 2014-10-20 04:45:28 By 唐有炜
    /// </summary>
 public class TSysDepartmentDaoImpl:ITSysDepartmentDao
    {

	  #region 读操作


	   /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <returns>返回所有数据总数</returns>
        public int GetCount() 
        {
          using (EdDBContext db=new EdDBContext())
            {
             var models= db.TSysDepartments;
			 var sqlText = models.GetProperty("SqlText");
             LogHelper.Debug(sqlText.ToString());
			 return models.Count();
            }
        }

		
             /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <param name="predicate">Lamda表达式</param>
        /// <returns>返回所有数据总数</returns>
       public int GetCount(Expression<Func<TSysDepartment, bool>> predicate)
        {
             using (EdDBContext db=new EdDBContext())
            {
             var models= db.TSysDepartments.Where<TSysDepartment>(predicate);
			 var sqlText = models.GetProperty("SqlText");
             LogHelper.Debug(sqlText.ToString());
			 return models.Count();
            }
        }




	    /// <summary>
        /// 获取所有的数据
	    /// </summary>
	    /// <returns>返回所有数据列表</returns>
        public List<TSysDepartment> GetList() 
        {
          using (EdDBContext db=new EdDBContext())
            {
             var models= db.TSysDepartments;
			  var sqlText = models.GetProperty("SqlText");
             LogHelper.Debug(sqlText.ToString());
			 return models.ToList();
            }
        }

		
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="predicate">Lamda表达式</param>
        /// <returns>返回所有数据列表</returns>
       public List<TSysDepartment> GetList(Expression<Func<TSysDepartment, bool>> predicate)
        {
             using (EdDBContext db=new EdDBContext())
            {
             var models= db.TSysDepartments.Where<TSysDepartment>(predicate);
			   var sqlText = models.GetProperty("SqlText");
             LogHelper.Debug(sqlText.ToString());
			 return models.ToList();
            }
        }

		/// <summary>
        /// 获取指定的单个实体
        /// 如果不存在则返回null
        /// 如果存在多个则抛异常
        /// </summary>
        /// <param name="predicate">Lamda表达式</param>
        /// <returns>Entity</returns>
        public TSysDepartment GetEntity(Expression<Func<TSysDepartment, bool>> predicate) 
        {
            using (EdDBContext db=new EdDBContext())
            {
                var model =db.TSysDepartments.Where<TSysDepartment>(predicate);
			    var sqlText = model.GetProperty("SqlText");
                LogHelper.Debug(sqlText.ToString());
                return model.SingleOrDefault();
		    }
        }

		
		/// <summary>
        /// 返回单个实体（带扩展字段）
        /// </summary>
        /// <param name="selector">要查询的字段</param>
        /// <param name="expFields">存储扩展字段值的字段</param>
        /// <param name="expSelector">要查询的扩展字段里面的字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>Dictionary&lt;String, Object&gt;.</returns>
        public Dictionary<string, object> GetEntityWithExp(string selector, string expFields, string expSelector,
            string predicate,
            params object[] values)
			{
		    using (EdDBContext db=new EdDBContext())
            {
                IQueryable<object> model=null;
                if (!String.IsNullOrEmpty(selector))
                {
                    model =
                        ((IQueryable<object>) db.TSysDepartments.Where(predicate, values).Select(selector, values));
                }
                else
                {
                    model = ((IQueryable<object>)db.TSysDepartments.Where(predicate, values));
                }
              
                object temp = model.FirstOrDefault();
                var sqlText = model.GetProperty("SqlText");
                 LogHelper.Debug("ELINQ Paging:<br/>" + sqlText.ToString());

                Dictionary<string, object> result = new Dictionary<string, object>();
                PropertyInfo[] propertyInfos = temp.GetType().GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    var name = propertyInfo.Name; //输入的selector中的字段名
                    var value = propertyInfo.GetValue(temp, null); //输入的selector中的字段值
                    if (name == expFields)
                    {
                        IDictionary<string, JToken> cusFields =
                            (JObject) JsonConvert.DeserializeObject(value.ToString());
                        //循环添加新字段
                        foreach (var field in cusFields)
                        {
                            //只输出已选择的扩展字段(不输出直接留空)
                            if (!String.IsNullOrEmpty(expSelector))
                            {
                                object[] exps = Utils.StringToObjectArray(expSelector, ',');
                                var fieldKey = NamingConversion.Default.PropertyName(field.Key);
                                var fieldvalue = field.Value;
                                if (exps.Contains(fieldKey)) //只查询选择的扩展字段
                                {
                                    result.Add(fieldKey, fieldvalue);
                                }
                            }
                        }
                    }
                    else
                    {
                        result.Add(name, value);
                    }
                }

                return result;
            }
			
			}


        /// <summary>
        /// 根据条件查询某些字段(LINQ 动态查询)
        /// </summary>
        /// <param name="selector">要查询的字段（格式：new(ID,Name)）</param>
        /// <param name="predicate">筛选条件（id=0）</param>
        /// <returns></returns>
        public IQueryable<Object> GetFields(string selector, string predicate, params object[] values)
        {
            using (EdDBContext db = new EdDBContext())
            {
                var model = db.TSysDepartments.Where(predicate, values).Select(selector);
                var sqlText = model.GetProperty("SqlText");
                LogHelper.Debug(sqlText.ToString());
                return (IQueryable<object>)model;
            }
        }


		   /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <returns></returns>
       public   bool ExistsEntity(Expression<Func<TSysDepartment , bool>> predicate)
	   {
            using (EdDBContext db=new EdDBContext())
            {
                try
                {
                    bool status = db.TSysDepartments.Any(predicate);
                    return status;
                }
                catch (Exception ex)
                {
                    LogHelper.Error("判断存在异常：",ex);
                    return false;
                }
             
              
            }
        }


       /// <summary>
       /// 是否存在该记录
       /// </summary>
       /// <returns></returns>
       public bool ExistsEntity(string predicate, params object[] values)
       {
           using (EdDBContext db = new EdDBContext())
           {
             
               try
               {
                   bool status = db.TSysDepartments.Where(predicate, values).Any();
                   return status;
               }
               catch (Exception ex)
               {
                   LogHelper.Error("判断存在异常：", ex);
                   return false;
               }
             
           }
       }

		
	   //查询分页
        public IPagination<TSysDepartment> GetListByPage(int pageIndex, int pageSize, out int rowCount,
            IDictionary<string, Ed.Entity.EdEnums.OrderEmum> orders,
            Expression<Func<TSysDepartment, bool>> predicate)
        {
            using (EdDBContext db = new EdDBContext())
            {
                var roles = db.TSysDepartments.Where(predicate);
                rowCount = roles.Count();
                var prevCount = (pageIndex - 1)*pageSize;
                var models = roles
                    .Skip(prevCount)
                    .Take(pageSize);
                foreach (var order in orders)
                {
                    models = models.OrderBy(String.Format("{0} {1}", order.Key, order.Value));
                }
                var sqlText = models.GetProperty("SqlText");
                LogHelper.Debug("ELINQ Paging:<br/>" + sqlText.ToString());
                return models.ToPagination(pageSize, pageSize, rowCount);
            }
        }


		 //查询分页(LINQ动态)
        public IPagination<TSysDepartment> GetListByPage(int pageIndex, int pageSize, out int rowCount,
            IDictionary<string, Ed.Entity.EdEnums.OrderEmum> orders,
            string predicate)
        {
            using (EdDBContext db = new EdDBContext())
            {
                var roles = db.TSysDepartments.Where(predicate);
                rowCount = roles.Count();
                var prevCount = (pageIndex - 1)*pageSize;
                var models = roles
                    .Skip(prevCount)
                    .Take(pageSize);
                foreach (var order in orders)
                {
                    models = models.OrderBy(String.Format("{0} {1}", order.Key, order.Value));
                }
                var sqlText = models.GetProperty("SqlText");
                LogHelper.Debug("ELINQ Paging:<br/>" + sqlText.ToString());
                return models.ToPagination(pageSize, pageSize, rowCount);
            }
        }




		  /// <summary>
        /// 查询分页（可以自定义添加属性，不包括扩展字段） 2014-10-15 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="selector">要查询的字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="recordCount">记录结果数</param>
        /// <param name="values">参数</param>
        /// <returns>查询分页（不包括扩展字段）</returns>
   public     List<Dictionary<string, object>> GetListByPage(int pageIndex, int pageSize, string selector,string predicate, string ordering,
            out int recordCount, params object[] values)
			{
			
		  using (EdDBContext db=new EdDBContext())
            {
                //获取查询结果
                //加上扩展字段值
                var temps = db.TSysDepartments;

                IQueryable<object> models = null;
                if (!String.IsNullOrEmpty(selector))
                {
                    models = (IQueryable<object>) (temps
                        .Where(predicate, values)
                        .Select(selector, values)
                        .OrderBy(ordering));
                }
                else
                {
                    models = (IQueryable<object>)(temps
                        .Where(predicate, values)
                        .OrderBy(ordering));
                }
               

                var sqlText = models.GetProperty("SqlText");
                LogHelper.Debug("ELINQ Dynamic Paging:<br/>" + sqlText.ToString());

				//计算总数
				recordCount = models.Count();

                //转换为分页
				var prevCount = (pageIndex - 1)*pageSize;
				models=models.Skip(prevCount).Take(pageSize);
                var pages = models.ToPagination(pageIndex, pageSize, recordCount);

                //组装输出
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
                foreach (var page in pages)
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    PropertyInfo[] propertyInfos = page.GetType().GetProperties();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var name = propertyInfo.Name; //输入的selector中的字段名
                        var value = propertyInfo.GetValue(page, null); //输入的selector中的字段值
                        switch (name)
                        {
                            case "ParentId":
                                int ParentId = int.Parse(value.ToString());
                                object objParentId =
                                    db.TSysDepartments.Where(p=>p.Id==ParentId)
                                        .Select(p=>p.DepName)
                                        .FirstOrDefault();
                                var ParentIdName = "";
                                if (null != objParentId)
                                {
                                    ParentIdName = objParentId.ToString();
                                    result.Add("ParentName", ParentIdName);
                                }
                                if (ParentId==0)
                                {
                                    result.Add("ParentName", "顶级分类");  
                                }
                                result.Add("ParentId", value);
                                break;
                              default: //添加默认字段
                                result.Add(name, value);
                                continue;
                        }
                  
                    }
                    results.Add(result);
                }

                return results;
            }
			
			}





	    /// <summary>
        /// 查询分页（包括扩展字段） 2014-08-29 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="selector">要查询的字段</param>
        /// <param name="expFields">存储扩展字段值的字段</param>
        /// <param name="expSelector">要查询的扩展字段里面的字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="recordCount">记录结果数</param>
        /// <param name="values">参数</param>
        /// <returns>查询分页（包括扩展字段）</returns>
   public     List<Dictionary<string, object>> GetListWithExpByPage(int pageIndex, int pageSize, string selector,
            string expFields, string expSelector,
            string predicate, string ordering,
            out int recordCount, params object[] values)
			{
			
		  using (EdDBContext db=new EdDBContext())
            {
                //获取查询结果
                //加上扩展字段值
                var temps = db.TSysDepartments;
                recordCount = temps.Count();
                var prevCount = (pageIndex - 1)*pageSize;

                IQueryable<object> models = null;
                if (!String.IsNullOrEmpty(selector))
                {
                    models = (IQueryable<object>) (temps
                        .Skip(prevCount)
                        .Take(pageSize)
                        .Where(predicate, values)
                        .Select(selector, values)
                        .OrderBy(ordering));
                }
                else
                {
                    models = (IQueryable<object>)(temps
                        .Skip(prevCount)
                        .Take(pageSize)
                        .Where(predicate, values)
                        .OrderBy(ordering));
                }
               

                var sqlText = models.GetProperty("SqlText");
                LogHelper.Debug("ELINQ Dynamic Paging:<br/>" + sqlText.ToString());

                //转换为分页
                var pages = models.ToPagination(pageIndex, pageSize, recordCount);

                //组装输出
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
                foreach (var page in pages)
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    PropertyInfo[] propertyInfos = page.GetType().GetProperties();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var name = propertyInfo.Name; //输入的selector中的字段名
                        var value = propertyInfo.GetValue(page, null); //输入的selector中的字段值
                        if (name == expFields)
                        {
                            IDictionary<string, JToken> cusFields =
                                (JObject) JsonConvert.DeserializeObject(value.ToString());
                            //循环添加新字段
                            foreach (var field in cusFields)
                            {
                                //只输出已选择的扩展字段(不输出直接留空)
                                if (!String.IsNullOrEmpty(expSelector))
                                {
                                    object[] exps = Utils.StringToObjectArray(expSelector, ',');
                                    var fieldKey = NamingConversion.Default.PropertyName(field.Key);
                                    var fieldvalue = field.Value;
                                    if (exps.Contains(fieldKey)) //只查询选择的扩展字段
                                    {
                                        result.Add(fieldKey, fieldvalue);
                                    }
                                }
                            }
                        }
                        else
                        {
                            result.Add(name, value);
                        }
                    }
                    results.Add(result);
                }


                return results;
            }
			
			}




	  //以下是原生Sql方法==============================================================
	  //===========================================================================
	   /// <summary>
        /// 用SQL语句查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="namedParameters">sql参数</param>
        /// <returns>集合</returns>
        public DataTable GetListBySql(string sql, dynamic namedParameters)
        {
          using (EdDBContext db=new EdDBContext())
            {
               return db.DbHelper.ExecuteDataTable(sql,namedParameters).ToList<TSysDepartment>();
            }
          
        }
  #endregion


   #region 写操作
		  /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public bool InsertEntity(TSysDepartment entity)
        {
            using (EdDBContext db=new EdDBContext())
            {
              int rows=  db.TSysDepartments.Insert(entity);
				 if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
       /// <summary>
        /// 删除实体
        /// </summary>
         /// <param name="predicate">Lamda表达式</param>
        public bool DeleteEntity(Expression<Func<TSysDepartment , bool>> predicate) 
        {
            using (EdDBContext db=new EdDBContext())
            {
                TSysDepartment  entity = db.TSysDepartments.Where(predicate).First();
                int rows=db.TSysDepartments.Delete(entity);
				 if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
		
		
        /// <summary>
        /// 批量删除（实体集合）
        /// </summary>
        /// <param name="predicate">动态LINQ</param>
        public bool DeletesEntity(List<string> predicates)
        {
            using (EdDBContext db=new EdDBContext())
            {
             
                if (db.Connection.State != ConnectionState.Open)
                {
                    db.Connection.Open();
                }
                var tran = db.Connection.BeginTransaction();
                try
                {
				     foreach (var predicate in predicates)
                     {
                        var item = db.TSysDepartments.Where(predicate).FirstOrDefault();
                        if (null != item)
                         {
                            db.TSysDepartments.Delete(item);
                         }
                    }
                    tran.Commit();
                    LogHelper.Debug("批量删除成功！");
                    return true;
                }
              catch (Exception ex)
                {
                    tran.Rollback();
                    LogHelper.Error("批量删除异常：", ex);
                    return false;
                }
                finally
                {
                    if (db.Connection.State != ConnectionState.Closed)
                    {
                        db.Connection.Close();
                    }
                }
            }
        }




         /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public bool UpdateEntity(TSysDepartment entity)
        {
            using (EdDBContext db=new EdDBContext())
            {
               int rows= db.TSysDepartments.Update(entity);
			   if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


     

        /// <summary>
        /// 使用LINQ批量更改TSysDepartment状态 2014-09-05 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="fields">要更新的字段（支持批量更新）</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
       public bool UpdateEntityStatus(Dictionary<string, object> fields,
            string predicate, params  object[] values)
			{
			

		 using (EdDBContext db=new EdDBContext())
            {
                if (db.Connection.State != ConnectionState.Open)
                {
                    db.Connection.Open();
                }
                var tran = db.Connection.BeginTransaction();
                try
                {
                    foreach (var field in fields)
                    {
                     var entity = db.TSysDepartments.Where(predicate,values).FirstOrDefault();
                        var propertyInfos = entity.GetType().GetProperties();
                        foreach (var p in propertyInfos)
                        {
                            if (p.Name == field.Key)
                            {
                                p.SetValue(entity, field.Value, null); //给对应属性赋值
                            }
                        }
                        db.TSysDepartments.Update(entity);
                    }


                    tran.Commit();
                    LogHelper.Debug("TSysDepartment字段批量更新成功。");
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    LogHelper.Error("TSysDepartment字段批量更新异常：", ex);
                    return false;
                }
                finally
                {
                    if (db.Connection.State != ConnectionState.Closed)
                    {
                        db.Connection.Close();
                    }
                }
            }







			}


    





		
		/// <summary>
	     /// 执行Sql
	     /// </summary>
	     /// <param name="sql">Sql语句</param>
	     /// <param name="namedParameters">查询字符串</param>
	     /// <returns></returns>
		public bool ExecuteSql(string sql, dynamic namedParameters = null)
		{
	         using (EdDBContext db = new EdDBContext())
	         {
	             var rows = db.DbHelper.ExecuteNonQuery(sql, namedParameters);
	             if (rows > 0)
	             {
	                 return true;
	             }
	             else
	             {
	                 return false;
	             }
	         }
		}

		  #endregion


	   }
	   }
