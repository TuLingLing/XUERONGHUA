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
public  class YuesaoOrderApiController:ApiController
    {

	#region  依赖注入(YuesaoOrderApiController注入YuesaoOrderService) 2014-10-16 23:38:14 By 唐有炜
	 /// <summary>
     /// 依赖注入(YuesaoOrderApiController注入YuesaoOrderService) 2014-10-16 23:38:14 By 唐有炜
     /// </summary>
	 private readonly IYuesaoOrderService   YuesaoOrderService = (IYuesaoOrderService ) ContextRegistry.GetContext().GetObject("yuesaoOrderService");
     private readonly IOrderService OrderService = (IOrderService)ContextRegistry.GetContext().GetObject("orderService");


     /// <summary>
     /// 依赖注入(ClientInfoApiController注入ClientInfoService) 2014-10-15 14:36:33 By 唐有炜
     /// </summary>
     private readonly IClientInfoService ClientInfoService =
         (IClientInfoService)ContextRegistry.GetContext().GetObject("clientInfoService");
	#endregion

	    #region 逻辑实现 2014-10-16 23:38:14 By 唐有炜

		#region 列表 2014-10-16 23:38:14 By 唐有炜
      
	    #region 获取所有YuesaoOrder列表（默认前10条） 2014-10-16 23:38:145 By 唐有炜
		//GET /api/YuesaoOrderApi/GetYuesaoOrders?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
		/// <summary>
        /// 获取所有YuesaoOrder列表（默认前10条） 2014-10-16 23:38:145 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        ///<param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TYuesaoOrder&gt;.</returns>
        public IEnumerable<TYuesaoOrder> GetYuesaoOrders(int? pageIndex, int? pageSize, string sortField, string sortType,string searchPhrase, string format)
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
            var models = YuesaoOrderService.GetYuesaoOrderList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
	    }
		
        #endregion

		 #region  获取Bootgrid YuesaoOrder列表 2014-10-16 23:38:14 By 唐有炜
	
        //POST /api/YuesaoOrderApi/GetBootGridYuesaoOrders?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid YuesaoOrder列表 2014-10-16 23:38:14 By 唐有炜
        /// </summary>
		/// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
		/// <param name="sort[fieldName]">sort[fieldName]</param>
		/// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridYuesaoOrders()
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
            List<Dictionary<string, object>> yuesaoOrders =
                YuesaoOrderService.GetYuesaoOrderList(current, rowCount, selector, predicate, ordering,
                    out total, values);

            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = yuesaoOrders,
                total = total
            };
            return bootGrid;
        }

        #endregion


		#region  获取ZTree树形节点 2014-10-16 23:38:14 By 唐有炜
		
        //POST /api/YuesaoOrderApi/GetZTreeYuesaoOrders/?id=0
        /// <summary>
        /// 获取YuesaoOrderZTree树形节点 2014-10-16 23:38:14 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreeYuesaoOrders(int id=0)
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

           //var nodes =   YuesaoOrderService.AsyncGetNodes(id);
		   var nodes =new Node(){id=0,name = "null"};
           return nodes;
        }
	
        #endregion



	   #endregion 


	   #region 查看 2014-10-16 23:38:14 By 唐有炜
	   	// GET /api/YuesaoOrderApi/Get/1
        /// <summary>
        /// 获取一条YuesaoOrder数据 2014-10-16 23:38:14 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TYuesaoOrder</returns>
       public TYuesaoOrder GetYuesaoOrder(int id)
	   {
	   	    var model =  YuesaoOrderService.GetYuesaoOrder(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
			return model;
	   }

      #endregion

	  #region 添加、修改 2014-10-16 23:38:14 By 唐有炜
       // POST /api/YuesaoOrderApi/AddYuesaoOrder
       /// <summary>
       ///添加信息 2014-10-16 23:38:14 By 唐有炜
       /// </summary>
       /// <param name="yuesaoOrder">yuesaoOrder</param>
       /// <returns>ResponseMessage</returns>
	    [HttpPost]
        public ResponseMessage AddYuesaoOrder([FromBody] TOrder order)
        {
            ResponseMessage rmsg = new ResponseMessage();
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象

            //订单
           order.OrderType = 1;//1. 月嫂订单 2.育婴师 3. 催乳 4.增值产品
           order.OrderSumMoney = 0.00;//默认总金额为0
           order.OrderDate = DateTime.Now;
           order.OrderStatus = 1;//1 待处理 2 待执行 3 待评价 4 待维护 0 回收站       
            //...

            //月嫂订单
            TYuesaoOrder yuesaoOrder=new TYuesaoOrder();
           yuesaoOrder.OrderCode =order.OrderCode;
            //预产期
           yuesaoOrder.YorderChildbirth = DateTime.Parse(request.Params.Get("YorderChildbirth"));
           yuesaoOrder.YorderCode = int.Parse(request.Params.Get("YorderCode"));//1. 一胎 2. 二胎 3 .三胎
            yuesaoOrder.YorderHospital=request.Params.Get("YorderHospital");//预产医院
            yuesaoOrder.YorderHaddress = request.Params.Get("YorderHaddress");//具体地址
            yuesaoOrder.YorderRequire = request.Params.Get("YorderRequire");//详细要求
            yuesaoOrder.YorderAlter = request.Params.Get("YorderAlter");//备选月嫂
            

            //...

            //财务信息
           TOrderFinance orderFinance=new TOrderFinance();
           orderFinance.OrderCode = order.OrderCode;
           orderFinance.FinType = 1;//预付定金 2.其他款
           orderFinance.FinPayment = order.OrderPrepaid;
           orderFinance.FinAddtime = DateTime.Now;
           orderFinance.FinCreater = int.Parse(request.Params.Get("OrderCreater").ToString());
           orderFinance.FinNote = request.Params.Get("FinNote");

            //...


            //使用事务提交
            if (YuesaoOrderService.AddYuesaoOrderTran(order,yuesaoOrder,orderFinance))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }
		

       // POST /api/YuesaoOrderApi/EditYuesaoOrder
       /// <summary>
       ///修改月嫂订单(即处理订单) 2014-10-16 23:38:14 By 唐有炜
       /// </summary>
       /// <param name="yuesaoOrder">yuesaoOrder</param>
       /// <returns>ResponseMessage</returns>
		[HttpPost]
        public ResponseMessage EditYuesaoOrder([FromBody] TOrder tempOrder)
		{
		
	      
            ResponseMessage rmsg = new ResponseMessage();
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象

           var order_code = request.QueryString["order_code"];

            //订单
           TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);
           order.OrderStatus = 2;
           order.OrderWeihu = int.Parse(request.Params.Get("OrderWeihu"));
           order.OrderProcesstime = DateTime.Parse(request.Params.Get("OrderProcesstime"));

          //讲当前订单对应的客户的维护人改为当前用户
           TClientInfo clientInfo = ClientInfoService.GetClientInfo(c => c.Id == order.ClientId);
           clientInfo.ClientWeihu =(int) order.OrderWeihu;
           ClientInfoService.EditClientInfo(clientInfo);


          //总金额
          order.OrderSumMoney =Double.Parse(request.Params.Get("OrderSumMoney"));//默认总金额为0
             

            //月嫂订单
           TYuesaoOrder yuesaoOrder = YuesaoOrderService.GetYuesaoOrder(y => y.OrderCode == order_code);
            //预产期
            yuesaoOrder.YorderChildbirth = DateTime.Parse(request.Params.Get("YorderChildbirth"));
            yuesaoOrder.YorderCode = int.Parse(request.Params.Get("YorderCode"));//1. 一胎 2. 二胎 3 .三胎
            yuesaoOrder.YorderHospital = request.Params.Get("YorderHospital");//预产医院
            yuesaoOrder.YorderHaddress = request.Params.Get("YorderHaddress");//具体地址
            yuesaoOrder.YorderRequire = request.Params.Get("YorderRequire");//详细要求
            yuesaoOrder.YorderYuesao = request.Params.Get("YorderYuesao");//备选月嫂
           


		     if ( YuesaoOrderService.EditYuesaoOrderTran(order,yuesaoOrder))
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
		

		#region 删除    2014-10-16 23:38:14 By 唐有炜
		
        // GET /api/YuesaoOrderApi/DeleteYuesaoOrder/5       
        /// <summary>
        ///删除    2014-10-16 23:38:14 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpGet]
        public ResponseMessage DeleteYuesaoOrder(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (YuesaoOrderService.DeleteYuesaoOrderTran(ids))
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


	    #region 修改状态 2014-10-16 23:38:14 By 唐有炜
//		
//        // GET /api/YuesaoOrderApi/UpdateYuesaoOrderStatus/5
//        /// <summary>
//        /// 修改状态 2014-10-16 23:38:14 By 唐有炜
//        /// </summary>
//        /// <param name="id">id</param>
//        /// <returns>ResponseMessage</returns>
//        [HttpPost]
//        public ResponseMessage UpdateYuesaoOrderStatus(int ids)
//        {
//            ResponseMessage rmsg = new ResponseMessage();
//             if (YuesaoOrderService.UpdateEntityStatus(null,null))
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


        #region 验证  2014-10-16 23:38:14 By 唐有炜
		
        /// <summary>
        /// 验证字段是否存在  2014-10-16 23:38:14 By 唐有炜
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <returns>验证状态</returns>
      public  ResponseMessage Validate(string fields)
		{
		 ResponseMessage rmsg = new ResponseMessage();
		 if (YuesaoOrderService.Validate(null))
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

