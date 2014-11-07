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
public  class OrderApiController:ApiController
    {
	#region  依赖注入(OrderApiController注入OrderService) 2014-10-20 21:14:18 By 唐有炜
	 /// <summary>
     /// 依赖注入(OrderApiController注入OrderService) 2014-10-20 21:14:18 By 唐有炜
     /// </summary>
	 private readonly IOrderService   OrderService = (IOrderService ) ContextRegistry.GetContext().GetObject("orderService");
     private readonly IYuesaoOrderService YuesaoOrderService = (IYuesaoOrderService)ContextRegistry.GetContext().GetObject("yuesaoOrderService");
     /// <summary>
     /// 依赖注入(OrderFinanceApiController注入OrderFinanceService) 2014-10-28 10:55:32 By 唐有炜
     /// </summary>
     private readonly IOrderFinanceService OrderFinanceService = (IOrderFinanceService)ContextRegistry.GetContext().GetObject("orderFinanceService");
  
	#endregion

	    #region 逻辑实现 2014-10-20 21:14:18 By 唐有炜

		#region 列表 2014-10-20 21:14:18 By 唐有炜
      
	    #region 获取所有Order列表（默认前10条） 2014-10-20 21:14:185 By 唐有炜
		//GET /api/OrderApi/GetOrders?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
		/// <summary>
        /// 获取所有Order列表（默认前10条） 2014-10-20 21:14:185 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        ///<param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TOrder&gt;.</returns>
        public IEnumerable<TOrder> GetOrders(int? pageIndex, int? pageSize, string sortField, string sortType,string searchPhrase, string format)
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
            var models = OrderService.GetOrderList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
	    }
		
        #endregion

		 #region  获取Bootgrid Order列表 2014-10-20 21:14:18 By 唐有炜
	
        //POST /api/OrderApi/GetBootGridOrders?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid Order列表 2014-10-20 21:14:18 By 唐有炜
        /// </summary>
		/// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
		/// <param name="sort[fieldName]">sort[fieldName]</param>
		/// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridOrders()
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
            //string predicate = "true";
            string predicate = "OrderCode.Contains(@0)";

            //我的订单
            int creater = 0;
            if (null != request.Params.Get("OrderCreater"))
            {
                creater = int.Parse(request.Params.Get("OrderCreater").ToString());
                predicate += " AND (OrderCreater=@1 OR OrderWeihu=@1) ";
            }

            int orderStatus = -1; //1 正常 0 回收站
            if (null != request.Params.Get("OrderStatus"))
            {
                orderStatus = int.Parse(request.Params.Get("OrderStatus").ToString());
            }
            //回收站订单
            if (orderStatus == 0 || orderStatus==1)
            {
                predicate += "AND OrderStatus=@2 "; 
            }
             //全部订单
            else if (orderStatus == -1)
            {
                predicate += "AND OrderStatus<>0 ";
            }
                //其他状态
            else
            {
                predicate += "AND OrderStatus=@2 AND OrderStatus<>0"; 
            }

            //排序
            string ordering = String.Concat(new[] {sortField, " ", sortType});

            //object[] values = {};
            object[] values = { searchPhrase, creater, orderStatus };
         
		    //数据总数
            int total = 0;

            //获取数据
            List<Dictionary<string, object>> orders =
                OrderService.GetOrderList(current, rowCount, selector, predicate, ordering,
                    out total, values);

            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = orders,
                total = total
            };
            return bootGrid;
        }

        #endregion


		#region  获取ZTree树形节点 2014-10-20 21:14:18 By 唐有炜
		
        //POST /api/OrderApi/GetZTreeOrders/?id=0
        /// <summary>
        /// 获取OrderZTree树形节点 2014-10-20 21:14:18 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreeOrders(int id=0)
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

           //var nodes =   OrderService.AsyncGetNodes(id);
		   var nodes =new Node(){id=0,name = "null"};
           return nodes;
        }
	
        #endregion



	   #endregion 


	   #region 查看 2014-10-20 21:14:18 By 唐有炜
	   	// GET /api/OrderApi/Get/1
        /// <summary>
        /// 获取一条Order数据 2014-10-20 21:14:18 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TOrder</returns>
       public TOrder GetOrder(int id)
	   {
	   	    var model =  OrderService.GetOrder(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
			return model;
	   }

      #endregion

	  #region 添加、修改 2014-10-20 21:14:18 By 唐有炜
       // POST /api/OrderApi/AddOrder
       /// <summary>
       ///添加信息 2014-10-20 21:14:18 By 唐有炜
       /// </summary>
       /// <param name="order">order</param>
       /// <returns>ResponseMessage</returns>
	    [HttpPost]
        public ResponseMessage AddOrder([FromBody] TOrder order)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (OrderService.AddOrder(order))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }
		

       // POST /api/OrderApi/EditOrder
       /// <summary>
       ///修改信息 2014-10-20 21:14:18 By 唐有炜
       /// </summary>
       /// <param name="order">order</param>
       /// <returns>ResponseMessage</returns>
		[HttpPost]
        public ResponseMessage EditOrder([FromBody] TOrder order)
		{
		
	      	ResponseMessage rmsg = new ResponseMessage();
		     if ( OrderService.EditOrder(order))
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
		

		#region 删除    2014-10-20 21:14:18 By 唐有炜
		
        // GET /api/OrderApi/DeleteOrder/5       
        /// <summary>
        ///删除    2014-10-20 21:14:18 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpGet]
        public ResponseMessage DeleteOrder(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();


            if (OrderService.DeleteOrder(ids))
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


	    #region 修改状态 2014-10-20 21:14:18 By 唐有炜
		
        // GET /api/OrderApi/UpdateOrderStatus/5
        /// <summary>
        /// 修改状态 2014-10-20 21:14:18 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ResponseMessage</returns>
        [HttpGet]
        public ResponseMessage UpdateOrderStatus(string ids,int status)
        {
            ResponseMessage rmsg = new ResponseMessage();

            List<Field> fields = new List<Field>();

            var idArr = Utils.StringToObjectArray(ids, ',');
            object[] parms = new object[idArr.Length];
            for (int i = 0; i < idArr.Length; i++)
            {
                //字段及条件
                Field field = new Field()
                {
                    Id = RandomHelper.GetGuidNumber(),
                    Key = "OrderStatus",
                    Value = status,
                    Predicate = "Id=@" + i
                };
                fields.Add(field);

                //参数
                var id = int.Parse(idArr[i].ToString());
                parms[i] = id;
            }

            if (OrderService.UpdateEntityStatus(fields, parms))
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


        #region 验证  2014-10-20 21:14:18 By 唐有炜
		
        /// <summary>
        /// 验证字段是否存在  2014-10-20 21:14:18 By 唐有炜
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <returns>验证状态</returns>
      public  ResponseMessage Validate(string fields)
		{
		 ResponseMessage rmsg = new ResponseMessage();
		 if (OrderService.Validate(null))
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
