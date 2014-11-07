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
public  class SysPowerApiController:ApiController
    {
	#region  依赖注入(SysPowerApiController注入SysPowerService) 2014-10-15 22:11:19 By 唐有炜
	 /// <summary>
     /// 依赖注入(SysPowerApiController注入SysPowerService) 2014-10-15 22:11:19 By 唐有炜
     /// </summary>
	 private readonly ISysPowerService   SysPowerService = (ISysPowerService ) ContextRegistry.GetContext().GetObject("sysPowerService");
	#endregion

	    #region 逻辑实现 2014-10-15 22:11:19 By 唐有炜

		#region 列表 2014-10-15 22:11:19 By 唐有炜
      
	    #region 获取所有SysPower列表（默认前10条） 2014-10-15 22:11:195 By 唐有炜
		//GET /api/SysPowerApi/GetSysPowers?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
		/// <summary>
        /// 获取所有SysPower列表（默认前10条） 2014-10-15 22:11:195 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        ///<param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TSysPower&gt;.</returns>
        public IEnumerable<TSysPower> GetSysPowers(int? pageIndex, int? pageSize, string sortField, string sortType,string searchPhrase, string format)
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
            var models = SysPowerService.GetSysPowerList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
	    }
		
        #endregion

		 #region  获取Bootgrid SysPower列表 2014-10-15 22:11:19 By 唐有炜
	
        //POST /api/SysPowerApi/GetBootGridSysPowers?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid SysPower列表 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
		/// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
		/// <param name="sort[fieldName]">sort[fieldName]</param>
		/// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridSysPowers()
        {
            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数
            int current = int.Parse(request.Params.Get("current") ?? "1");
            int rowCount = int.Parse(request.Params.Get("rowCount") ?? "1");
			string sort = request.Params.AllKeys.SingleOrDefault(a => a.Contains("sort"));
            //string sortName = sort.Split('[')[0];
            string sortField = sort.Split('[')[1].TrimEnd(']').ToLower();
            string sortType = request.Params.GetValues(sort).SingleOrDefault().ToLower();
            string searchPhrase = request.Params.Get("searchPhrase");

            //数据总数
            int total = 0;

            //搜索
            string predicate = "true";

            //排序
            IDictionary<string, EdEnums.OrderEmum> dataOrders = new Dictionary<string, EdEnums.OrderEmum>();
            dataOrders.Add(sortType == EdEnums.OrderEmum.Desc.ToString().ToLower()
                ? new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Desc)
                : new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Asc));

			//获取数据
			IEnumerable<TSysPower> sysPowers =
               SysPowerService.GetSysPowerList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();

            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = sysPowers,
                total = total
            };
            //var timeConverter =
            //    new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(bootGrid, timeConverter);
			return bootGrid;
        }

        #endregion


		#region  获取ZTree树形节点 2014-10-15 22:11:19 By 唐有炜
		
        //POST /api/SysPowerApi/GetZTreeSysPowers/?id=0
        /// <summary>
        /// 获取SysPowerZTree树形节点 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreeSysPowers(int id=0)
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

           //var nodes =   SysPowerService.AsyncGetNodes(id);
		   var nodes =new Node(){id=0,name = "null"};
           return nodes;
        }
	
        #endregion



	   #endregion 


	   #region 查看 2014-10-15 22:11:19 By 唐有炜
	   	// GET /api/SysPowerApi/Get/1
        /// <summary>
        /// 获取一条SysPower数据 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TSysPower</returns>
       public TSysPower GetSysPower(int id)
	   {
	   	    var model =  SysPowerService.GetSysPower(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
			return model;
	   }

      #endregion

	  #region 添加、修改 2014-10-15 22:11:19 By 唐有炜
       // POST /api/SysPowerApi/AddSysPower
       /// <summary>
       ///添加信息 2014-10-15 22:11:19 By 唐有炜
       /// </summary>
       /// <param name="sysPower">sysPower</param>
       /// <returns>ResponseMessage</returns>
	    [HttpPost]
        public ResponseMessage AddSysPower([FromBody] TSysPower sysPower)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (SysPowerService.AddSysPower(sysPower))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }
		

       // POST /api/SysPowerApi/EditSysPower
       /// <summary>
       ///修改信息 2014-10-15 22:11:19 By 唐有炜
       /// </summary>
       /// <param name="sysPower">sysPower</param>
       /// <returns>ResponseMessage</returns>
		[HttpPut]
        public ResponseMessage EditSysPower([FromBody] TSysPower sysPower)
		{
		
	      	ResponseMessage rmsg = new ResponseMessage();
		     if ( SysPowerService.EditSysPower(sysPower))
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
		

		#region 删除    2014-10-15 22:11:19 By 唐有炜
		
        // GET /api/SysPowerApi/DeleteSysPower/5       
        /// <summary>
        ///删除    2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpDelete]
        public ResponseMessage DeleteSysPower(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (SysPowerService.DeleteSysPower(ids))
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


	    #region 修改状态 2014-10-15 22:11:19 By 唐有炜
//		
//        // GET /api/SysPowerApi/UpdateSysPowerStatus/5
//        /// <summary>
//        /// 修改状态 2014-10-15 22:11:19 By 唐有炜
//        /// </summary>
//        /// <param name="id">id</param>
//        /// <returns>ResponseMessage</returns>
//        [HttpPut]
//        public ResponseMessage UpdateSysPowerStatus(int ids)
//        {
//            ResponseMessage rmsg = new ResponseMessage();
//             if (SysPowerService.UpdateEntityStatus(null,null))
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


        #region 验证  2014-10-15 22:11:19 By 唐有炜
		
        /// <summary>
        /// 验证字段是否存在  2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <returns>验证状态</returns>
      public  ResponseMessage Validate(string fields)
		{
		 ResponseMessage rmsg = new ResponseMessage();
		 if (SysPowerService.Validate(null))
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
