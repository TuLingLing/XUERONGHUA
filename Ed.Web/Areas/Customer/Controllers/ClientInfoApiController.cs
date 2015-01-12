// ***********************************************************************
// Assembly         : Ed.Web
// Author           : tangyouwei
// Created          : 10-27-2014
//
// Last Modified By : tangyouwei
// Last Modified On : 10-25-2014
// ReSharper disable All 禁止ReSharper显示警告
// ***********************************************************************
// <copyright file="ClientInfoApiController.cs" company="郑州优创软件科技有限公司">
//     Copyright (c) www.ucs123.com. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections;
using System.Net.Http;
using System.Text;
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

/// <summary>
/// The Web namespace.
/// </summary>
namespace Ed.Web
{
    /// <summary>
    /// Class ClientInfoApiController.
    /// </summary>
    public class ClientInfoApiController : ApiController
    {
        #region  依赖注入(ClientInfoApiController注入ClientInfoService) 2014-10-15 14:36:33 By 唐有炜

        /// <summary>
        /// 依赖注入(ClientInfoApiController注入ClientInfoService) 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        private readonly IClientInfoService ClientInfoService =
            (IClientInfoService) ContextRegistry.GetContext().GetObject("clientInfoService");

        /// <summary>
        /// The system user service
        /// </summary>
        private readonly ISysUserService SysUserService =
            (ISysUserService) ContextRegistry.GetContext().GetObject("sysUserService");

        /// <summary>
        /// The client maintenance service
        /// </summary>
        private readonly IClientMaintenanceService ClientMaintenanceService =
            (IClientMaintenanceService) ContextRegistry.GetContext().GetObject("clientMaintenanceService");
        /// <summary>
        /// The system department service
        /// </summary>
        private readonly ISysDepartmentService SysDepartmentService = (ISysDepartmentService)ContextRegistry.GetContext().GetObject("sysDepartmentService");
        #endregion

        #region 逻辑实现 2014-10-15 14:36:33 By 唐有炜

        #region 列表 2014-10-15 14:36:33 By 唐有炜

        #region 获取所有ClientInfo列表（默认前10条） 2014-10-15 14:36:335 By 唐有炜

        //GET /api/ClientInfoApi/GetClientInfos?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
        /// <summary>
        /// 获取所有ClientInfo列表（默认前10条） 2014-10-15 14:36:335 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        /// <param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TClientInfo&gt;.</returns>
        public IEnumerable<TClientInfo> GetClientInfos(int? pageIndex, int? pageSize, string sortField, string sortType,
            string searchPhrase, string format)
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
            if (!String.IsNullOrEmpty(searchPhrase))
            {
                predicate = Base64.Base64StringToString(searchPhrase);
            }
            var models =
                ClientInfoService.GetClientInfoList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
        }

        #endregion

        #region  获取Bootgrid ClientInfo列表 2014-10-15 14:36:33 By 唐有炜

        //POST /api/ClientInfoApi/GetBootGridClientInfos?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid ClientInfo列表 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridClientInfos()
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

            //要查询的字段（控值所有）
            string selector = "";

            //搜索
            //string predicate = "true";
            string predicate = "(ClientName.Contains(@0) OR ClientCode.Contains(@0) OR ClientTel1.Contains(@0)) ";

            //筛选当前客户
            int creater = 0;
            if (null != request.Params.Get("ClientCreater"))
            {
                creater = int.Parse(request.Params.Get("ClientCreater").ToString());
                predicate += " AND (ClientCreater=@1 OR CLientWeihu=@1) ";
            }

            int clientStatus = 1; //1 正常 0 回收站
            if (null != request.Params.Get("ClientStatus"))
            {
                clientStatus = int.Parse(request.Params.Get("ClientStatus").ToString());
            }
            predicate += "AND ClientStatus=@2";

            //正确的方式是构建 LINQ 提供程序 ，请参见 LINQ: Building an IQueryable Provider
            //这里那里在对IQueryable<T> 扩展，请仔细看清，在说。
            //这里是对Dynamic.cs里面支持的.Where("ShipCity.Contains(@0) ","test");转化为个人比较习惯的.Where("ShipCity like @0 ","test")形式。

            //排序=========================================================
            //IDictionary<string, EdEnums.OrderEmum> dataOrders = new SortedDictionary<string, EdEnums.OrderEmum>();
            //dataOrders.Add(sortType == EdEnums.OrderEmum.Desc.ToString().ToLower()
            //   ? new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Desc)
            //   : new KeyValuePair<string, EdEnums.OrderEmum>(sortField, EdEnums.OrderEmum.Asc));
            string ordering = String.Concat(new[] {sortField, " ", sortType});
            //string ordering = "Id DESC";

            object[] values = {searchPhrase, creater, clientStatus};

            //数据总数
            int total = 0;

            //获取数据
            //IEnumerable<TClientInfo> clientInfos =
            //   ClientInfoService.GetClientInfoList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            List<Dictionary<string, object>> clientInfos =
                ClientInfoService.GetClientInfoList(current, rowCount, selector, predicate, ordering, out total, values);


            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = clientInfos,
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

        #region  获取ZTree树形节点 2014-10-15 14:36:33 By 唐有炜

        //POST /api/ClientInfoApi/GetZTreeClientInfos/?id=0
        /// <summary>
        /// 获取ClientInfoZTree树形节点 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreeClientInfos(int id = 0)
        {
            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

            //var nodes =   ClientInfoService.AsyncGetNodes(id);
            var nodes = new Node() {id = 0, name = "null"};
            return nodes;
        }

        #endregion

        #endregion

        #region 查看 2014-10-15 14:36:33 By 唐有炜

        // GET /api/ClientInfoApi/Get/1
        /// <summary>
        /// 获取一条ClientInfo数据 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TClientInfo</returns>
        public TClientInfo GetClientInfo(int id)
        {
            var model = ClientInfoService.GetClientInfo(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
            return model;
        }

        #endregion

        #region 添加、修改 2014-10-15 14:36:33 By 唐有炜

        // POST /api/ClientInfoApi/AddClientInfo
        /// <summary>
        /// 添加信息 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="clientInfo">clientInfo</param>
        /// <returns>ResponseMessage</returns>
        [HttpPost]
        public ResponseMessage AddClientInfo([FromBody] TClientInfo clientInfo)
        {
            ResponseMessage rmsg = new ResponseMessage();

            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象

            //客户编号
            var userId = HttpContext.Current.Session[EdKeys.SESSION_USER_ID].ToString();

            dynamic depObj = SysUserService.GetFields("NEW(DepId)", String.Concat(new[] { "Id=", userId })).FirstOrDefault();
            var depId = depObj.DepId.ToString();

            dynamic depObj2 = SysDepartmentService.GetFields("NEW(DepCode)", String.Concat(new[] { "Id=", depId })).FirstOrDefault();

            var depCode = depObj2.DepCode.ToString();

            //dynamic maxIdObj = PregnanterInfoService.GetFields("NEW((MAX(Id))","TRUE").FirstOrDefault();
            var maxId = ClientInfoService.GetMaxId();

            clientInfo.ClientCode = RandomHelper.GetCustomerNumber(depCode, maxId);

            //省市
            string prov = request.Params.Get("ClientProv");
            string city = request.Params.Get("ClientArea");
            string region = request.Params.Get("ClientRegion");
            clientInfo.ClientCity = String.Concat(new[] {prov, ",", city, ",", region});
            

            //默认正常 0 回收站
            clientInfo.ClientStatus = 1;

            if (ClientInfoService.AddClientInfo(clientInfo))
            {
                TClientMaintenance clientMaintenance = new TClientMaintenance()
                {
                    ClientId = clientInfo.Id,
                    CMainType = "1", //1. 信息维护 2. 跟踪记录 3. 纪念日 4.其他
                    CMainContent = "添加了客户【" + clientInfo.ClientName + "】。",
                    CMainTime = DateTime.Now,
                    CMainCreater = clientInfo.ClientCreater.ToString()
                };
                try
                {
                    ClientMaintenanceService.AddClientMaintenance(clientMaintenance);
                    rmsg.Status = true;
                }
                catch (Exception)
                {
                    rmsg.Status = false;
                }
            }
            else
            {
                rmsg.Status = false;
            }
            return rmsg;
        }


        // POST /api/ClientInfoApi/EditClientInfo
        /// <summary>
        /// 修改信息 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="clientInfo">clientInfo</param>
        /// <returns>ResponseMessage</returns>
        [HttpPost]
        public ResponseMessage EditClientInfo([FromBody] TClientInfo clientInfo)
        {
            ResponseMessage rmsg = new ResponseMessage();

            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            string tel=request.Params.Get("ClientTel1");        
        

            //省市
            string prov = request.Params.Get("ClientProv");
            string city = request.Params.Get("ClientArea");
            string region = request.Params.Get("ClientRegion");
            clientInfo.ClientCity = String.Concat(new[] {prov, ",", city, ",", region});


            //默认正常 0 回收站
            clientInfo.ClientStatus = 1;

            if (ClientInfoService.EditClientInfo(clientInfo))
            {
                TClientMaintenance clientMaintenance = new TClientMaintenance()
                {
                    ClientId = clientInfo.Id,
                    CMainType = "1", //1. 信息维护 2. 跟踪记录 3. 纪念日 4.其他
                    CMainContent = "修改了客户【" + clientInfo.ClientName + "】。",
                    CMainTime = DateTime.Now,
                    CMainCreater = clientInfo.ClientCreater.ToString()
                };
                try
                {
                    ClientMaintenanceService.AddClientMaintenance(clientMaintenance);
                    rmsg.Status = true;
                }
                catch (Exception)
                {
                    rmsg.Status = false;
                }
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }

        #endregion

        #region 删除    2014-10-15 14:36:33 By 唐有炜

        // GET /api/ClientInfoApi/DeleteClientInfo/5       
        /// <summary>
        /// 删除    2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpGet]
        public ResponseMessage DeleteClientInfo(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (ClientInfoService.DeleteClientInfo(ids))
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

        #region 修改状态 2014-10-15 14:36:33 By 唐有炜

        // GET /api/ClientInfoApi/UpdateClientInfoStatus/5
        /// <summary>
        /// 修改状态 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="status">The status.</param>
        /// <returns>ResponseMessage</returns>
        [HttpGet]
        public ResponseMessage UpdateClientInfoStatus(string ids, int status)
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
                    Key = "ClientStatus",
                    Value = 0,
                    Predicate = "Id=@" + i
                };
                fields.Add(field);

                //参数
                var id = int.Parse(idArr[i].ToString());
                parms[i] = id;
            }

            if (ClientInfoService.UpdateEntityStatus(fields, parms))
            {
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }

            return rmsg;
        }


        // GET /api/ClientInfoApi/UpdateClientInfoWeihu/5
        /// <summary>
        /// 修改状态 2014-10-15 14:36:33 By 唐有炜
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="status">The status.</param>
        /// <returns>ResponseMessage</returns>
        [HttpGet]
        public ResponseMessage UpdateClientInfoWeihu(string ids, int userId)
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
                    Key = "ClientWeihu",
                    Value = userId,
                    Predicate = "Id=@" + i
                };
                fields.Add(field);

                //参数
                var id = int.Parse(idArr[i].ToString());
                parms[i] = id;
            }

            if (ClientInfoService.UpdateEntityStatus(fields, parms))
            {
                LogHelper.Debug("客户转移成功！");
                rmsg.Status = true;
            }
            else
            {
                LogHelper.Debug("客户转移失败！");
                rmsg.Status = false;
            }

            return rmsg;
        }


        #endregion

        #region 验证  2014-10-15 14:36:33 By 唐有炜

        //  /api/ClientInfoApi/Validate?fields=ClientCode&eqs==&values=KH702548095&operations=2
        /// <summary>
        /// 验证字段是否存在  2014-10-15 14:36:33 By 唐有炜(注意：字段必须与条件一一对应)
        /// </summary>
        /// <param name="fields">字段集合(id,name)</param>
        /// <param name="eqs">The eqs.</param>
        /// <param name="values">The values.</param>
        /// <param name="operations">条件集合（or,and）</param>
        /// <returns>验证状态</returns>
        [HttpGet]
        public HttpResponseMessage Validate(string fields, string eqs, string values, string operations, bool existReturn = true)
        {
            var Status = false;
            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象

            object[] parms = null;
            string predicate = Utils.BuildPredicate(fields, eqs, values, operations, out parms);


            //排除回收站
            predicate = String.Concat(new[] {"(",predicate,")"," AND ClientStatus<>0 "});
            LogHelper.Debug("验证客户是否存在（排除回收站）：" + predicate);
            ////排除当前登录人的手机号
            //predicate = String.Concat(new[] { "(", predicate, ")", " AND ClientCode<>1 " });
            //LogHelper.Debug("验证客户是否当前登录客户：" + predicate);

            if (ClientInfoService.Validate(predicate, parms))
            {
                Status = true;
            }
            else
            {
                Status = false;
            }


            //状态判断
            if (!existReturn)
            {
                Status = !Status;
            }

            var rmsg = new HttpResponseMessage
            {
                Content = new StringContent(Status.ToString().ToLower(), System.Text.Encoding.UTF8, "text/plain")
            };
            return rmsg;
        }

        #endregion
    }
}