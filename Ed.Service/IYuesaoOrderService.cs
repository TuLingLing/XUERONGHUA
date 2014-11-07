﻿// ReSharper disable All 禁止ReSharper显示警告
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using NLite.Data;
using Ed.DBContext;
using Ed.Entity;

namespace Ed.Service
{
public  interface IYuesaoOrderService
    {
	    #region 逻辑接口 2014-10-15 22:02:50 By 唐有炜

		 /// <summary>
        /// 获取信息分页列表（IEnumerable<Model>） 2014-10-15 22:02:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="recordCount">总数（用out返回便于后面使用）</param>
        /// <param name="orders">排序字段（字段字典<字段、排序类型>）</param>
        /// <param name="predicate">筛选条件</param>
         IEnumerable<TYuesaoOrder> GetYuesaoOrderList( int pageIndex, int pageSize, out int recordCount,
            Expression<Func<TYuesaoOrder, bool>> predicate,IDictionary<string, Ed.Entity.EdEnums.OrderEmum> orders
            );
			/// <summary>
        /// 获取信息分页列表（IEnumerable<Model>，动态LINQ） 2014-10-15 22:02:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="recordCount">总数（用out返回便于后面使用）</param>
        /// <param name="orders">排序字段（字段字典<字段、排序类型>）</param>
        /// <param name="predicate">筛选条件</param>
         IEnumerable<TYuesaoOrder> GetYuesaoOrderList( int pageIndex, int pageSize, out int recordCount,
           string predicate,IDictionary<string, Ed.Entity.EdEnums.OrderEmum> orders
            );

        /// <summary>
        /// 获取信息分页列表(DataTable) 2014-10-15 22:02:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
		/// <param name="selectFields">选择的字段（格式：new string[]{"field1,field2"}，field1如果是主键必须要有）</param>
        /// <param name="strWhere">筛选条件（字段名="值",字段名 in (值1,值2)）</param>
        /// <param name="filedOrder">排序字段（字段名）</param>
        /// <param name="recordCount">总数</param>
        /// <returns>DataTable</returns>
        DataTable GetYuesaoOrderList(string[] selectFields, int pageIndex, int pageSize, out int recordCount,
            string strWhere, string filedOrder);



			
		  /// <summary>
        /// 查询分页（可以自定义添加属性，不包括扩展字段） 2014-10-15 22:02:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="selector">要查询的字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="recordCount">记录结果数</param>
        /// <param name="values">参数</param>
        /// <returns>查询分页（不包括扩展字段）</returns>
        List<Dictionary<string, object>>  GetYuesaoOrderList(int pageIndex, int pageSize, string selector,string predicate, string ordering,
            out int recordCount, params object[] values);


        /// <summary>
        /// 获取信息分页列表List<Dictionary<string, object>> 2014-10-15 22:02:50 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的数目</param>
        /// <param name="selector">要查询的字段</param>
        /// <param name="expFields">存储扩展字段值的字段</param>
        /// <param name="expSelector">要查询的扩展字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="recordCount">记录结果数</param>
        /// <param name="values">参数</param>
        /// <returns>List<Dictionary<string, object>></returns>
        List<Dictionary<string, object>> GetYuesaoOrderList( string selector, string expFields, string expSelector,int pageIndex, int pageSize, out int recordCount,string predicate, string ordering,params object[] values);


		  /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>TYuesaoOrder</returns>
       TYuesaoOrder GetYuesaoOrder(Expression<Func<TYuesaoOrder, bool>> predicate);

      /// <summary>
        /// 获取一条信息
        /// </summary>
       /// <param name="selector">要查询的字段</param>
        /// <param name="expFields">存储扩展字段值的字段</param>
        /// <param name="expSelector">要查询的扩展字段</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>Dictionary<string, object></returns>
        Dictionary<string, object> GetYuesaoOrder(string selector, string expFields, string expSelector, string predicate,
            params object[] values);

    
    /// <summary>
    /// 根据条件查询某些字段(LINQ 动态查询)
    /// </summary>
    /// <param name="selector">要查询的字段（格式：new(ID,Name)）</param>
    /// <param name="predicate">筛选条件（u=>u.id==0）</param>
    /// <returns></returns>
    IQueryable<Object> GetFields(string selector, string predicate,params object[] values);

        /// <summary>
        /// 添加信息 2014-08-30 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="TYuesaoOrder">TYuesaoOrder</param>
        /// <returns>添加状态</returns>
        bool AddYuesaoOrder(TYuesaoOrder YuesaoOrder);

        /// <summary>
        /// 添加实体(事务控制先添加订单表，在添加月嫂订单表,最后添加财务表) 14-10-19 By 唐友伟
        /// </summary>
        /// <param name="entity">实体对象</param>
        bool AddYuesaoOrderTran(TOrder order, TYuesaoOrder yuesaoOrder,TOrderFinance orderFinance);



        /// <summary>
        /// 修改实体(事务控制先修改订单表，在修改月嫂订单表) 14-10-25 By 唐友伟
        /// </summary>
        /// <param name="entity">实体对象</param>
        bool EditYuesaoOrderTran(TOrder order, TYuesaoOrder yuesaoOrder);
        /// <summary>
        /// 修改信息 2014-08-30 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
         /// <param name="TYuesaoOrder">TYuesaoOrder</param>
        /// <returns>添加状态</returns>
        bool EditYuesaoOrder(TYuesaoOrder YuesaoOrder);
//
//  
//
//        /// <summary>
//        /// 使用LINQ批量更改T状态 2014-09-05 14:58:50 By 唐有炜
//        /// </summary>
//        /// <param name="fields">要更新的字段（支持批量更新）</param>
//        /// <param name="predicate">条件</param>
//        /// <returns></returns>
//        bool UpdateEntityStatus(Dictionary<string, object> fields,
//          string predicate);


		/// <summary>
        /// 删除信息 2014-08-30 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>删除状态</returns>
        bool DeleteYuesaoOrder(string ids);


        /// <summary>
        /// 级联删除信息（使用事务） 2014-08-30 14:58:50 By 唐有炜
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>删除状态</returns>
        bool DeleteYuesaoOrderTran(string ids);
        #endregion


        #region 验证接口

      /// <summary>
        /// 验证字段是否存在
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <returns>验证状态</returns>
        bool Validate(string fields);

        #endregion
	}
}

