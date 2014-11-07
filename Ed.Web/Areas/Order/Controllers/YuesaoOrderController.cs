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
    public class YuesaoOrderController : Controller
    {
        #region  依赖注入 2014-10-16 23:38:17 By 唐有炜

        public IYuesaoOrderService YuesaoOrderService { set; get; }
        public IClientInfoService ClientInfoService { set; get; }
        public IPregnanterInfoService PregnanterInfoService { set; get; }
        public IOrderService OrderService { set; get; }
        public ISysUserService SysUserService { set; get; }

        #endregion

        #region 页面展示  2014-10-11 10:21:24 By 唐有炜

        #region 添加、修改 、查看页面

        #region 添加订单第一步：添加客户

        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult AddClient()
        {
            return View("AddClient");
        }

        #endregion

        #region 添加订单第二步：选择月嫂

        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult AddYuesao()
        {
            var client_num = Request.QueryString["client_num"];
            var num_type = Request.QueryString["num_type"];
            if (null == client_num || null == num_type)
            {
                ViewData["Msg"] = "参数非法！";
                return View("_Error");
            }
            ViewBag.ClientNum = client_num;
            ViewBag.NumType = num_type;
            return View("AddYuesao");
        }

        #endregion

        #region 添加订单第三步：完善订单信息

        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult AddYuesaoInfo()
        {
            var client_num = Request.QueryString["client_num"].Trim();
            var num_type = Request.QueryString["num_type"].Trim();
            TClientInfo customer = null;
            if (num_type == "phone")
            {
                customer = ClientInfoService.GetClientInfo(c => c.ClientTel1 == client_num);
            }
            else if (num_type == "kh")
            {
                customer = ClientInfoService.GetClientInfo(c => c.ClientCode == client_num);
            }
            else if (num_type == "idcard")
            {
                customer = ClientInfoService.GetClientInfo(c => c.ClientIdcard == client_num);
            }


            //空值
            if (null == customer)
            {
                customer = new TClientInfo();
            }

            var ids = Request.QueryString["ids"].Trim().TrimStart(',');
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

            var count = 3;
            var orders = new Dictionary<string, EdEnums.OrderEmum>();
            orders.Add("Id", EdEnums.OrderEmum.Desc);
            var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
                p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

            ViewBag.Customer = customer;
            ViewBag.Yuesao = yuesao;

            var order_start = Request.QueryString["order_start"];
            var order_end = Request.QueryString["order_end"];
            ViewBag.OrderStart = order_start;
            ViewBag.OrderEnd = order_end;

            return View("AddYuesaoInfo");
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

                //获取月嫂信息
                object[] parms2 = {order_code};
                dynamic yuesaoIdsObj =
                    YuesaoOrderService.GetFields("NEW(YorderAlter)", "OrderCode=@0", parms2).FirstOrDefault();
                string ids = yuesaoIdsObj.YorderAlter.ToString();

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

                var count = 3;
                var orders = new Dictionary<string, EdEnums.OrderEmum>();
                orders.Add("Id", EdEnums.OrderEmum.Desc);
                var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
                    p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

                //当前订单信息
                TOrder order = OrderService.GetOrder(o => o.OrderCode == order_code);
                ViewBag.OrderCreaterName = SysUserService.GetFields("UserTname", "Id=" + order.OrderCreater).FirstOrDefault();

                //月嫂订单信息
                TYuesaoOrder yuesaoOrder = YuesaoOrderService.GetYuesaoOrder(y => y.OrderCode == order_code);

                //输出到页面
                ViewBag.Customer = customer;
                ViewBag.Yuesao = yuesao;
                ViewBag.Order = order;
                ViewBag.yuesaoOrder = yuesaoOrder;

                return View();
            }
            else
            {
                ViewData["Msg"] = "参数非法（actipnType不能为空）";
                return View("_Error");
            }
        }

        #endregion

        #region 查看月嫂

        //此页面用于查看月嫂
        //GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult ViewYuesao()
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
            object[] parms2 = { order_code };
            dynamic yuesaoIdsObj =
                YuesaoOrderService.GetFields("NEW(YorderAlter)", "OrderCode=@0", parms2).FirstOrDefault();
            string ids = yuesaoIdsObj.YorderAlter.ToString();

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


            var count = 3;
            var orders = new Dictionary<string, EdEnums.OrderEmum>();
            orders.Add("Id", EdEnums.OrderEmum.Desc);
            var yuesao = PregnanterInfoService.GetPregnanterInfoList(1, 10, out count,
                p => p.Id == ys1 || p.Id == ys2 || p.Id == ys3, orders).ToList();

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
          

            //月嫂订单信息
            TYuesaoOrder yuesaoOrder = YuesaoOrderService.GetYuesaoOrder(y => y.OrderCode == order_code);

            //确定月嫂
            TPregnanterInfo yorderYuesao=new TPregnanterInfo();
            if (yuesaoOrder.YorderYuesao!=null)
            {
                int yorderYuesaoId = int.Parse(yuesaoOrder.YorderYuesao);
                yorderYuesao = PregnanterInfoService.GetPregnanterInfo(p => p.Id == yorderYuesaoId);
            }

            //输出到页面
            ViewBag.Customer = customer;
            //（意向月嫂）
            ViewBag.Yuesao = yuesao;
            ViewBag.Order = order;
            ViewBag.yuesaoOrder = yuesaoOrder;
            //确定月嫂
            ViewBag.YorderYuesao = yorderYuesao;

           
            return View("ViewYuesao");
        }

        #endregion

        #endregion

        #endregion
    }
}