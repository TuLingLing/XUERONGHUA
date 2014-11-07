//ReSharper disable All 禁止ReSharper显示警告
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Ed.Common;
using Ed.Entity;
using Ed.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spring.Context.Support;

namespace Ed.Web
{
public  class SysLogApiController:ApiController
    {
	#region  依赖注入(SysLogApiController注入SysLogService) 2014-10-20 16:07:14 By 唐有炜
	 /// <summary>
     /// 依赖注入(SysLogApiController注入SysLogService) 2014-10-20 16:07:14 By 唐有炜
     /// </summary>
	 private readonly ISysLogService   SysLogService = (ISysLogService ) ContextRegistry.GetContext().GetObject("sysLogService");
	#endregion

	    #region 逻辑实现 2014-10-20 16:07:14 By 唐有炜

		#region 列表 2014-10-20 16:07:14 By 唐有炜
      
	    #region 获取所有SysLog列表（默认前10条） 2014-10-20 16:07:145 By 唐有炜
		//GET /api/SysLogApi/GetSysLogs?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
		/// <summary>
        /// 获取所有SysLog列表（默认前10条） 2014-10-20 16:07:145 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        ///<param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TSysLog&gt;.</returns>
        public IEnumerable<TSysLog> GetSysLogs(int? pageIndex, int? pageSize, string sortField, string sortType,string searchPhrase, string format)
        {
            int total = 0;
            //分页
            int current = pageIndex == null ? 1 : (int) pageIndex;
            int rowCount = pageSize == null ? 10 : (int) pageSize;
            //排序
            IDictionary<string, EdEnums.OrderEmum> dataOrders = new Dictionary<string, EdEnums.OrderEmum>();
            if (!String.IsNullOrEmpty(sortField) && !String.IsNullOrEmpty(sortType))
            {
                dataOrders.Add(sortType.ToLower() == EdEnums.OrderEmum.Desc.ToString().ToLower()
                    ? new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Desc)
                    : new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Asc));
            }
            else
            {
                dataOrders.Add(new KeyValuePair<string, EdEnums.OrderEmum>("Id", EdEnums.OrderEmum.Desc));
            }
            //搜索
            string predicate = "true";
            //这里是搜索条件组装(解密)
            if (!String.IsNullOrEmpty(searchPhrase) )
            {
                predicate = Base64.Base64StringToString(searchPhrase);
            }
            var models = SysLogService.GetSysLogList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
	    }
		
        #endregion

		 #region  获取Bootgrid SysLog列表 2014-10-20 16:07:14 By 唐有炜
	
        //POST /api/SysLogApi/GetBootGridSysLogs?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid SysLog列表 2014-10-20 16:07:14 By 唐有炜
        /// </summary>
		/// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
		/// <param name="sort[fieldName]">sort[fieldName]</param>
		/// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridSysLogs()
        {
		           var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数
            int current = int.Parse(request.Params.Get("current") ?? "1");
            int rowCount = int.Parse(request.Params.Get("rowCount") ?? "1");
            string sort = request.Params.AllKeys.SingleOrDefault(a => a.Contains("sort"));
            string sortField = sort.Split('[')[1].TrimEnd(']').ToLower();
            string sortType = request.Params.GetValues(sort).SingleOrDefault().ToLower();
            string searchPhrase = request.Params.Get("searchPhrase");

            //要查询的字段（控值所有）
            string selector = "";
         
		    //搜索
            string predicate = "true";
            //string predicate = "Id.Contains(@0)";

            //排序
            string ordering = String.Concat(new[] {sortField, " ", sortType});

            object[] values = {};
            //object[] values = {searchPhrase};
         
		    //数据总数
            int total = 0;

            //获取数据
            List<Dictionary<string, object>> sysLogs =
                SysLogService.GetSysLogList(current, rowCount, selector, predicate, ordering,
                    out total, values);
           
            //自定义格式化字段
            foreach (var sysLog in sysLogs)
            {

                sysLog["LogTime"] = DateTime.Parse(sysLog["LogTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            }

            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = sysLogs,
                total = total
            };
            return bootGrid;
        }

        #endregion


		#region  获取ZTree树形节点 2014-10-20 16:07:14 By 唐有炜
		
        //POST /api/SysLogApi/GetZTreeSysLogs/?id=0
        /// <summary>
        /// 获取SysLogZTree树形节点 2014-10-20 16:07:14 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreeSysLogs(int id=0)
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

           //var nodes =   SysLogService.AsyncGetNodes(id);
		   var nodes =new Node(){id=0,name = "null"};
           return nodes;
        }
	
        #endregion



	   #endregion 


	   #region 查看 2014-10-20 16:07:14 By 唐有炜
	   	// GET /api/SysLogApi/Get/1
        /// <summary>
        /// 获取一条SysLog数据 2014-10-20 16:07:14 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TSysLog</returns>
       public TSysLog GetSysLog(int id)
	   {
	   	    var model =  SysLogService.GetSysLog(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
			return model;
	   }

      #endregion

	  #region 添加、修改 2014-10-20 16:07:14 By 唐有炜
       // POST /api/SysLogApi/AddSysLog
       /// <summary>
       ///添加信息 2014-10-20 16:07:14 By 唐有炜
       /// </summary>
       /// <param name="sysLog">sysLog</param>
       /// <returns>ResponseMessage</returns>
	    [HttpPost]
        public ResponseMessage AddSysLog([FromBody] TSysLog sysLog)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (SysLogService.AddSysLog(sysLog))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }
		

       // POST /api/SysLogApi/EditSysLog
       /// <summary>
       ///修改信息 2014-10-20 16:07:14 By 唐有炜
       /// </summary>
       /// <param name="sysLog">sysLog</param>
       /// <returns>ResponseMessage</returns>
		[HttpPost]
        public ResponseMessage EditSysLog([FromBody] TSysLog sysLog)
		{
		
	      	ResponseMessage rmsg = new ResponseMessage();
		     if ( SysLogService.EditSysLog(sysLog))
			{
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
           
		}
		
		#endregion
		

		#region 删除    2014-10-20 16:07:14 By 唐有炜
		
        // GET /api/SysLogApi/DeleteSysLog/5       
        /// <summary>
        ///删除    2014-10-20 16:07:14 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpGet]
        public ResponseMessage DeleteSysLog(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (SysLogService.DeleteSysLog(ids))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }
	
        #endregion
		
		#endregion 


	    #region 修改状态 2014-10-20 16:07:14 By 唐有炜
//		
//        // GET /api/SysLogApi/UpdateSysLogStatus/5
//        /// <summary>
//        /// 修改状态 2014-10-20 16:07:14 By 唐有炜
//        /// </summary>
//        /// <param name="id">id</param>
//        /// <returns>ResponseMessage</returns>
//        [HttpPost]
//        public ResponseMessage UpdateSysLogStatus(int ids)
//        {
//            ResponseMessage rmsg = new ResponseMessage();
//             if (SysLogService.UpdateEntityStatus(null,null))
//                        {
//                            rmsg.Status = true;
//                        }
//                        else
//                        {
//                            rmsg.Status = false;
//                        }
//            
//            return rmsg;
//        }
//		
        #endregion


        #region 验证  2014-10-20 16:07:14 By 唐有炜
		
        /// <summary>
        /// 验证字段是否存在  2014-10-20 16:07:14 By 唐有炜
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <returns>验证状态</returns>
      public  ResponseMessage Validate(string fields)
		{
		 ResponseMessage rmsg = new ResponseMessage();
		 if (SysLogService.Validate(null))
                        {
                            rmsg.Status = true;
                        }
                        else
                        {
                            rmsg.Status = false;
                        }
						return rmsg;
		}
		
        #endregion
	}
}
