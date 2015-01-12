// ReSharper disable All 禁止ReSharper显示警告

using Ed.Common;
using Ed.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Service;
using Ed.Entity;

namespace Ed.Web.Areas.Order.Controllers
{
    public class CorderController : Controller
    {
        #region  依赖注入 2014-10-16 23:38:17 By 唐有炜

        public IYuesaoOrderService YuesaoOrderService { set; get; }
        public IClientInfoService ClientInfoService { set; get; }
        public IPregnanterInfoService PregnanterInfoService { set; get; }
        public IOrderService OrderService { set; get; }
        public ISysUserService SysUserService { set; get; }

        #endregion

        #region 页面展示  2014-10-11 10:21:24 By 唐有炜


 

        #region 添加月子会所订单

        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult AddHuisuoInfo(int id)
        {
            //获取客户信息          
            var model = ClientInfoService.GetClientInfo(temp => temp.Id == id);
            ViewBag.Customer = model;           
            //var client_num = Request.QueryString["client_num"].Trim();
            //var num_type = Request.QueryString["num_type"].Trim();
            //TClientInfo customer = null;
            //if (num_type == "phone")
            //{
            //    customer = ClientInfoService.GetClientInfo(c => c.ClientTel1 == client_num);
            //}
            //else if (num_type == "kh")
            //{
            //    customer = ClientInfoService.GetClientInfo(c => c.ClientCode == client_num);
            //}
            //else if (num_type == "idcard")
            //{
            //    customer = ClientInfoService.GetClientInfo(c => c.ClientIdcard == client_num);
            //}
            
            //var order_start = Request.QueryString["order_start"];
            //var order_end = Request.QueryString["order_end"];
            //var price_type = Request.QueryString["price_type"];
            var ids = Request.QueryString["id"].Trim().TrimStart(',');
            object[] idArr = Utils.StringToObjectArray(ids, ',');
            int ys1 = int.Parse(idArr[0].ToString());
            int ys2 = 0;
            if (idArr.Length >= 2)
            {
                ys2 = int.Parse(idArr[1].ToString());
            }
            int ys3 = 0;
            if (idArr.Length == 3)
            {
                ys3 = int.Parse(idArr[2].ToString());
            }

            //var count = 3;
            //var orders = new Dictionary<string, EdEnums.OrderEmum>();
            //orders.Add("Id", EdEnums.OrderEmum.Desc);
            //var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
            //    p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

            
            //ViewBag.Yuesao = yuesao;

            //var order_start = Request.QueryString["order_start"];
            //var order_end = Request.QueryString["order_end"];
            //var price_type = Request.QueryString["price_type"];
            //ViewBag.OrderStart = order_start;
            //ViewBag.OrderEnd = order_end;
            //ViewBag.PriceType = price_type;
            return View("AddHuisuoInfo");
        }

        #endregion

        #region 添加增值产品订单

        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult AddZengzhiInfo(int id)
        {
            //获取客户信息          
            var model = ClientInfoService.GetClientInfo(temp => temp.Id == id);
            ViewBag.Customer = model;
           
            var ids = Request.QueryString["id"].Trim().TrimStart(',');
            object[] idArr = Utils.StringToObjectArray(ids, ',');
            int ys1 = int.Parse(idArr[0].ToString());
            int ys2 = 0;
            if (idArr.Length >= 2)
            {
                ys2 = int.Parse(idArr[1].ToString());
            }
            int ys3 = 0;
            if (idArr.Length == 3)
            {
                ys3 = int.Parse(idArr[2].ToString());
            }           
            return View("AddZengzhiInfo");
        }

        #endregion


        #region 处理订单页面

        ////GET: URL
        ////actionType (Do)
        [UserAuthorize]
        [HttpGet]
        public ActionResult Edit(string actionType, string order_code)
        {
            //修改
            if (actionType == EdEnums.ActionEnum.Do.ToString())
            {
                if (null == order_code)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }

                //获取客户信息
                object[] parms1 = {order_code};
                dynamic clientIdObj = OrderService.GetFields("NEW(ClientId)", "OrderCode=@0", parms1).FirstOrDefault();
                int clientId = int.Parse(clientIdObj.ClientId.ToString());
                var customer = ClientInfoService.GetClientInfo(c => c.Id == clientId);

                ////获取月嫂信息
                //object[] parms2 = {order_code};
                //dynamic yuesaoIdsObj =
                //    YuesaoOrderService.GetFields("NEW(YorderAlter)", "OrderCode=@0", parms2).FirstOrDefault();
                //string ids = yuesaoIdsObj.YorderAlter.ToString();

                //object[] idArr = Utils.StringToObjectArray(ids, ',');
                //int ys1 = int.Parse(idArr[0].ToString());
                //int ys2 = 0;
                //if (idArr.Length >= 2)
                //{
                //    ys2 = int.Parse(idArr[1].ToString());
                //}
                //int ys3 = 0;
                //if (idArr.Length == 3)
                //{
                //    ys3 = int.Parse(idArr[2].ToString());
                //}

                //var count = 3;
                //var orders = new Dictionary<string, EdEnums.OrderEmum>();
                //orders.Add("Id", EdEnums.OrderEmum.Desc);
                //var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
                //    p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

                //当前订单信息
                TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);              
                ViewBag.OrderCreaterName = SysUserService.GetFields("UserTname", "Id=" + order.OrderCreater).FirstOrDefault();

                ////月嫂订单信息
                //TYuesaoOrder yuesaoOrder = YuesaoOrderService.GetYuesaoOrder(y => y.OrderCode == order_code);

                //输出到页面
                ViewBag.Customer = customer;
                //ViewBag.Yuesao = yuesao;
                ViewBag.Order = order;
                //ViewBag.yuesaoOrder = yuesaoOrder;

                return View("Edit");
            }
            else
            {
                ViewData["Msg"] = "参数非法（actipnType不能为空）";
                return View("_Error");
            }
        }

        #endregion

        #region 查看月子会所订单和增值产品订单

        //此页面用于查看月嫂
        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult ViewHuisuo()
        {
            if (null == Request.QueryString["order_code"])
            {
                ViewData["Msg"] = "参数非法！";
                return View("_Error");
            }


            string order_code = Request.QueryString["order_code"];

            //获取客户信息
            object[] parms1 = { order_code };
            dynamic clientIdObj = OrderService.GetFields("NEW(ClientId)", "OrderCode=@0", parms1).FirstOrDefault();
            int clientId = int.Parse(clientIdObj.ClientId.ToString());
            var customer = ClientInfoService.GetClientInfo(c => c.Id == clientId);

            //获取月嫂信息
            //object[] parms2 = { order_code };
            //dynamic yuesaoIdsObj =
            //    YuesaoOrderService.GetFields("NEW(YorderAlter)", "OrderCode=@0", parms2).FirstOrDefault();
            //string ids = yuesaoIdsObj.YorderAlter.ToString();

            //object[] idArr = Utils.StringToObjectArray(ids, ',');
            //int ys1 = int.Parse(idArr[0].ToString());
            //int ys2 = 0;
            //if (idArr.Length >= 2)
            //{
            //    ys2 = int.Parse(idArr[1].ToString());
            //}
            //int ys3 = 0;
            //if (idArr.Length == 3)
            //{
            //    ys3 = int.Parse(idArr[2].ToString());
            //}


            //var count = 3;
            //var orders = new Dictionary<string, EdEnums.OrderEmum>();
            //orders.Add("Id", EdEnums.OrderEmum.Desc);
            //var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
            //    p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

            //当前订单信息
            TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);
            ViewBag.OrderCreaterName = SysUserService.GetFields("UserTname", "Id=" + order.OrderCreater).FirstOrDefault();
            try
            {
                ViewBag.OrderWeihuName = SysUserService.GetFields("UserTname", "Id=" + order.OrderWeihu).FirstOrDefault() ?? "";
            }
            catch
            {

                ViewBag.OrderWeihuName = "";
            }
          

            ////月嫂订单信息
            //TYuesaoOrder yuesaoOrder = YuesaoOrderService.GetYuesaoOrder(y => y.OrderCode == order_code);

            ////确定月嫂
            //TPregnanterInfo yorderYuesao=new TPregnanterInfo();
            //if (yuesaoOrder.YorderYuesao!=null)
            //{
            //    int yorderYuesaoId = int.Parse(yuesaoOrder.YorderYuesao);
            //    yorderYuesao = PregnanterInfoService.GetPregnanterInfo(p => p.Id == yorderYuesaoId);
            //}

            //输出到页面
            ViewBag.Customer = customer;
            //（意向育婴师）
            //ViewBag.Yuesao = yuesao;
            ViewBag.Order = order;
            //ViewBag.yuesaoOrder = yuesaoOrder;
            ////确定育婴师
            //ViewBag.YorderYuesao = yorderYuesao;

            return View("ViewHuisuo");
        }

        //此页面用于查看增值产品
        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult ViewZengzhi()
        {
            if (null == Request.QueryString["order_code"])
            {
                ViewData["Msg"] = "参数非法！";
                return View("_Error");
            }
            string order_code = Request.QueryString["order_code"];

            //获取客户信息
            object[] parms1 = { order_code };
            dynamic clientIdObj = OrderService.GetFields("NEW(ClientId)", "OrderCode=@0", parms1).FirstOrDefault();
            int clientId = int.Parse(clientIdObj.ClientId.ToString());
            var customer = ClientInfoService.GetClientInfo(c => c.Id == clientId);
            //当前订单信息
            TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);
            ViewBag.OrderCreaterName = SysUserService.GetFields("UserTname", "Id=" + order.OrderCreater).FirstOrDefault();
            try
            {
                ViewBag.OrderWeihuName = SysUserService.GetFields("UserTname", "Id=" + order.OrderWeihu).FirstOrDefault() ?? "";
            }
            catch
            {

                ViewBag.OrderWeihuName = "";
            }
            //输出到页面
            ViewBag.Customer = customer;        
            ViewBag.Order = order;         
            return View("ViewZengzhi");
        }

        #endregion
      


    }
}
        #endregion