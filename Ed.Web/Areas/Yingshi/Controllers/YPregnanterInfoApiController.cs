using System.Net.Http;
using System.Net.NetworkInformation;
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
    public class YPregnanterInfoApiController : ApiController
    {
        #region  依赖注入(PregnanterInfoApiController注入PregnanterInfoService) 2014-10-15 22:11:19 By 唐有炜

        /// <summary>
        /// 依赖注入(PregnanterInfoApiController注入PregnanterInfoService) 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        private readonly IPregnanterInfoService PregnanterInfoService =
            (IPregnanterInfoService) ContextRegistry.GetContext().GetObject("pregnanterInfoService");
        private readonly IPregnanterMaintenanceService PregnanterMaintenanceService = (IPregnanterMaintenanceService)ContextRegistry.GetContext().GetObject("pregnanterMaintenanceService");
        private readonly IPregnanterServiceRecordService PregnanterServiceRecordService = (IPregnanterServiceRecordService)ContextRegistry.GetContext().GetObject("pregnanterServiceRecordService");
        private readonly ISysUserService SysUserService = (ISysUserService)ContextRegistry.GetContext().GetObject("sysUserService");
        private readonly ISysDepartmentService SysDepartmentService = (ISysDepartmentService)ContextRegistry.GetContext().GetObject("sysDepartmentService");
	
        #endregion

        #region 逻辑实现 2014-10-15 22:11:19 By 唐有炜

        #region 列表 2014-10-17 14:25:14 By 唐有炜

        #region 获取所有PregnanterInfo列表（默认前10条） 2014-10-17 14:25:145 By 唐有炜
        //GET /api/PregnanterInfoApi/GetPregnanterInfos?pageIndex={pageIndex}&pageSize={pageSize}&sortField={sortField}&sortType={sortType}&searchPhrase={searchPhrase}&format={format}
        /// <summary>
        /// 获取所有PregnanterInfo列表（默认前10条） 2014-10-17 14:25:145 By 唐有炜
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数.</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortType">排序类型</param>
        ///<param name="searchPhrase">searchPhrase(格式：加密的条件)</param>
        /// <param name="format">返回格式（json：返回json数据，xml：返回xml数据）</param>
        /// <returns>IEnumerable&lt;TPregnanterInfo&gt;.</returns>
        public IEnumerable<TPregnanterInfo> GetPregnanterInfos(int? pageIndex, int? pageSize, string sortField, string sortType, string searchPhrase, string format)
        {
            int total = 0;
            //分页
            int current = pageIndex == null ? 1 : (int)pageIndex;
            int rowCount = pageSize == null ? 10 : (int)pageSize;
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
            var models = PregnanterInfoService.GetPregnanterInfoList(current, rowCount, out total, predicate, dataOrders).AsEnumerable();
            return models;
        }

        #endregion

        #region  获取Bootgrid PregnanterInfo列表 2014-10-17 14:25:14 By 唐有炜

        //POST /api/PregnanterInfoApi/GetBootGridPregnanterInfos?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid PregnanterInfo列表 2014-10-17 14:25:14 By 唐有炜
        /// </summary>
        /// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
        /// <param name="sort[fieldName]">sort[fieldName]</param>
        /// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridPregnanterInfos()
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数
            int current = int.Parse(request.Params.Get("current") ?? "1");
            int rowCount = int.Parse(request.Params.Get("rowCount") ?? "1");
            string sort = request.Params.AllKeys.SingleOrDefault(a => a.Contains("sort"));
            string sortField = sort.Split('[')[1].TrimEnd(']').ToLower();
            string sortType = request.Params.GetValues(sort).SingleOrDefault().ToLower();
            string searchPhrase = HttpUtility.UrlDecode(request.Params.Get("searchPhrase"));
            //1 数值 2 搜索条件字符串
            string searchType = request.Params.Get("searchType")??"1";

            //要查询的字段（控值所有）
            string selector = "";
            //搜索
            string predicate = "true";
            object[] parms;
            //单个模糊搜索
            if (searchType == "1")
            {
                parms = new object[2];
                predicate =
                    "(PInfoCode.Contains(@0) OR PInfoName.Contains(@0) OR PInfoTel.Contains(@0) OR PInfoHobby.Contains(@0)) ";
               
                //int preStatus = -1; //1 正常 0 回收站
                //int preFenlei =1; //0 月嫂  1 育婴师 2月嫂和育婴师
                //if (null != request.Params.Get("PInfoState") && null != request.Params.Get("PInfoFenlei"))
                //{
                //    preStatus = int.Parse(request.Params.Get("PInfoState").ToString());
                //    preFenlei = int.Parse(request.Params.Get("PInfoFenlei").ToString());
                //}
                            
                ////回收站订单
                //if ((preStatus == 0 || preStatus == 2) && preFenlei == 1)
                //{
                //    predicate += "AND PInfoState=@0";
                //    predicate += "AND(PInfoFenlei<>0)";
                //}
                ////全部订单
                //else if (preStatus == -1&&preFenlei==1)
                //{
                //    predicate += "AND (PInfoState<>0 AND PInfoState<>2) ";
                //    predicate += "AND(PInfoFenlei<>0)";
                //}
                ////其他状态
                //else if (preFenlei == 1)
                //{
                //    predicate += "AND PInfoState=@2 AND (PInfoState<>0 AND PInfoState<>1)  ";
                //    predicate += "AND(PInfoFenlei<>0)";
                //}               

                int preStatus = -1; //1 正常 0 回收站
                int preFenlei = 1; //0 月嫂  1 育婴师 2月嫂和育婴师
                if (null != request.Params.Get("PInfoState") )
                {
                    preStatus = int.Parse(request.Params.Get("PInfoState").ToString());
                    preFenlei = 1;
                }
                //回收站订单
                if ((preStatus == 0 || preStatus == 2) && preFenlei == 1)
                {
                    predicate += "AND PInfoState=@1 ";
                    predicate += "AND(PInfoFenlei<>0)";
                }
                //全部订单
                else if (preStatus == -1 && preFenlei == 1)
                {
                    predicate += "AND (PInfoState<>0 AND PInfoState<>2) ";
                    predicate += "AND(PInfoFenlei<>0)";
                }
                //其他状态
                else if (preFenlei == 1 && preFenlei == 0)
                {
                    predicate += "AND PInfoState=@1 AND (PInfoState<>0 AND PInfoState<>2)  ";
                    predicate += "AND(PInfoFenlei<>0)";

                }
                parms[0] = searchPhrase;
                parms[1] = preStatus;
            }
                //多条件搜索（订单搜索页面使用）
            else
            {
                var searchArr = Utils.StringToObjectArray(searchPhrase, '|');
                var fields = searchArr[0].ToString();
                var eqs = searchArr[1].ToString();
                var values = searchArr[2].ToString();
                var ops = searchArr[3].ToString();
                predicate = Utils.BuildPredicate(fields, eqs, values, ops, out parms);

                //只筛选开启状态的
                if (String.IsNullOrEmpty(predicate))
                {
                    predicate = "true";
                }
                predicate = String.Concat(new[] { "(", predicate, ")", " AND PInfoState=1" });
                LogHelper.Debug("添加订单第二部，搜索月嫂（排除回收站和已关闭）：" + predicate);

                //只筛选月嫂
                if (String.IsNullOrEmpty(predicate))
                {
                    predicate = "true";
                }
                predicate = String.Concat(new[] { "(", predicate, ")", " AND (PInfoFenlei=1 ||PInfoFenlei=2) " });
                LogHelper.Debug("添加订单第二部，搜索月嫂（排除回收站和已关闭）：" + predicate);

                //控制判断
                if (String.IsNullOrEmpty(predicate))
                {
                    predicate = "true";
                }
              
            }

            /***************************************************/
            if (!String.IsNullOrEmpty(request.Params.Get("OrderStart"))&&!String.IsNullOrEmpty(request.Params.Get("OrderEnd")))
            {
                //时间特殊处理
                DateTime orderStart = DateTime.Parse(request.Params.Get("OrderStart"));
                DateTime orderEnd = DateTime.Parse(request.Params.Get("OrderEnd"));
                string tpred = "PServiceBegin<=@0 AND PServiceEnd>=@1";
                object[] tvals = new object[2] { orderStart, orderEnd };

                dynamic objServiceRecordInfo = PregnanterServiceRecordService.GetFields("NEW(PInfoCode,PServiceBegin,PServiceEnd)", tpred, tvals).AsEnumerable();
                if (objServiceRecordInfo != null)//有正在服务的月嫂，排除
                {
                    //predicate = "false";
                    foreach (var obj in objServiceRecordInfo) {
                        var code = int.Parse(obj.PInfoCode.ToString());//注意：这里必须强制类型转换
                        var begin = obj.PServiceBegin;
                        var end = obj.PServiceEnd;

                        var plist = parms.ToList();
                        predicate = String.Concat(new []{predicate," AND Id <> @"+plist.Count});
                        plist.Add(code);
                       parms= plist.ToArray();
                    }
                }   
            }
            /****************************************************/

            //排序
            string ordering = String.Concat(new[] { sortField, " ", sortType });

            //数据总数
            int total = 0;

            //获取数据
            LogHelper.Debug("predicate:" + predicate);
            List<Dictionary<string, object>> pregnanterInfos =
                PregnanterInfoService.GetPregnanterInfoList(current, rowCount, selector, predicate, ordering,
                    out total, parms);
            //空值处理
            if (null == pregnanterInfos)
            {
                pregnanterInfos=new List<Dictionary<string, object>>();
            }


            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = pregnanterInfos,
                total = total
            };
            return bootGrid;
        }

        #endregion


        #region  获取ZTree树形节点 2014-10-17 14:25:14 By 唐有炜

        //POST /api/PregnanterInfoApi/GetZTreePregnanterInfos/?id=0
        /// <summary>
        /// 获取PregnanterInfoZTree树形节点 2014-10-17 14:25:14 By 唐有炜
        /// </summary>
        /// <returns>json</returns>
        [HttpPost]
        public Node GetZTreePregnanterInfos(int id = 0)
        {
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //基本参数

            //var nodes =   PregnanterInfoService.AsyncGetNodes(id);
            var nodes = new Node() { id = 0, name = "null" };
            return nodes;
        }

        #endregion

        #endregion 


        #region 查看 2014-10-15 22:11:19 By 唐有炜

        // GET /api/PregnanterInfoApi/Get/1
        /// <summary>
        /// 获取一条PregnanterInfo数据 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TPregnanterInfo</returns>
        public TPregnanterInfo GetPregnanterInfo(int id)
        {
            var model = PregnanterInfoService.GetPregnanterInfo(temp => temp.Id == id);
            //var timeConverter =new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = "yyyy-MM-dd";
            //return JsonConvert.SerializeObject(model, timeConverter); 
            return model;
        }

        #endregion

        #region 添加、修改 2014-10-15 22:11:19 By 唐有炜

        // POST /api/PregnanterInfoApi/AddPregnanterInfo
        /// <summary>
        ///添加信息 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="pregnanterInfo">pregnanterInfo</param>
        /// <returns>ResponseMessage</returns>
        [HttpPost]
        public ResponseMessage AddPregnanterInfo([FromBody] TPregnanterInfo pregnanterInfo)
        {
            ResponseMessage rmsg = new ResponseMessage();
           
            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            //月嫂编号
             var userId = HttpContext.Current.Session[EdKeys.SESSION_USER_ID].ToString();
                       
            dynamic depObj = SysUserService.GetFields("NEW(DepId)", String.Concat(new[] { "Id=", userId })).FirstOrDefault();
            var depId = depObj.DepId.ToString();

            dynamic depObj2 = SysDepartmentService.GetFields("NEW(DepCode)", String.Concat(new[] { "Id=", depId })).FirstOrDefault();
            
            var depCode = depObj2.DepCode.ToString();

            //dynamic maxIdObj = PregnanterInfoService.GetFields("NEW((MAX(Id))","TRUE").FirstOrDefault();
            var maxId = PregnanterInfoService.GetMaxId();           
            pregnanterInfo.PInfoCode = RandomHelper.GetYuesaoNumber(depCode, maxId);
         
            pregnanterInfo.PInfoFenlei = 1;
   
            //生活照片
            string PInfoLifeimg = request.Params.Get("PInfoLifeimg") ?? "";
            //0|/Upload/201410/18/201410180324093440.jpg|/Upload/201410/18/small_201410180324093440.jpg,0|/Upload/201410/18/201410180324094720.jpg|/Upload/201410/18/small_201410180324094720.jpg,0|/Upload/201410/18/201410180324096350.jpg|/Upload/201410/18/small_201410180324096350.jpg,0|/Upload/201410/18/201410180324098391.jpg|/Upload/201410/18/small_201410180324098391.jpg
            string PInfoLifeimgRemark = request.Params.Get("PInfoLifeimgRemark") ?? "";
            PInfoLifeimg = Utils.UnionStringsBySplit(PInfoLifeimg, PInfoLifeimgRemark, ',');
            pregnanterInfo.PInfoLifeimg = PInfoLifeimg;

            //证件照片
            string PInfoCertimg = request.Params.Get("PInfoCertimg") ?? "";
            string PInfoCertimgRemark = request.Params.Get("PInfoCertimgRemark") ?? "";
            PInfoCertimg = Utils.UnionStringsBySplit(PInfoCertimg, PInfoCertimgRemark, ',');
            pregnanterInfo.PInfoCertimg = PInfoCertimg;

            //省市
            string prov = request.Params.Get("YuesaoProv");
            string city = request.Params.Get("YuesaoArea");
            string region = request.Params.Get("YuesaoRegion");
            pregnanterInfo.PInfoCity = String.Concat(new[] { prov, ",", city, ",", region });
           
            //创建时间
            pregnanterInfo.PInfoCreatedate = DateTime.Now;
           
            //最后跟踪
            pregnanterInfo.PInfoLastdate = DateTime.Now;

            if (PregnanterInfoService.AddPregnanterInfo(pregnanterInfo))
            {

                TPregnanterMaintenance pregnanterMaintenance =new TPregnanterMaintenance()
                {
                 PInfoCode = pregnanterInfo.Id,
                 PMainType = 1,
                 PMainContent = "添加了月嫂【"+pregnanterInfo.PInfoName+"】",
                 PMainCreater =  (int)pregnanterInfo.PInfoCreateperson,
                 PMainCreatTime =DateTime.Now
                 
                };
                try
                {
                    PregnanterMaintenanceService.AddPregnanterMaintenance(pregnanterMaintenance);
                    rmsg.Status = true;
                }
                catch (Exception)
                {
                    rmsg.Status = false;
                }

                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }


        // POST /api/PregnanterInfoApi/EditPregnanterInfo
        /// <summary>
        ///修改信息 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="pregnanterInfo">pregnanterInfo</param>
        /// <returns>ResponseMessage</returns>
        [HttpPost]
        public ResponseMessage EditPregnanterInfo([FromBody] TPregnanterInfo pregnanterInfo)
        {
            ResponseMessage rmsg = new ResponseMessage();

            var context = (HttpContextBase) Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象          
            pregnanterInfo.PInfoFenlei = 1;
                      
            //生活照片
            string PInfoLifeimg = request.Params.Get("PInfoLifeimg")??"";
            //0|/Upload/201410/18/201410180324093440.jpg|/Upload/201410/18/small_201410180324093440.jpg,0|/Upload/201410/18/201410180324094720.jpg|/Upload/201410/18/small_201410180324094720.jpg,0|/Upload/201410/18/201410180324096350.jpg|/Upload/201410/18/small_201410180324096350.jpg,0|/Upload/201410/18/201410180324098391.jpg|/Upload/201410/18/small_201410180324098391.jpg
            string PInfoLifeimgRemark = request.Params.Get("PInfoLifeimgRemark")??"";
           PInfoLifeimg = Utils.UnionStringsBySplit(PInfoLifeimg, PInfoLifeimgRemark, ',');
           pregnanterInfo.PInfoLifeimg = PInfoLifeimg;
           
            //证件照片
            string PInfoCertimg = request.Params.Get("PInfoCertimg")??"";
            string PInfoCertimgRemark = request.Params.Get("PInfoCertimgRemark")??"";
            PInfoCertimg = Utils.UnionStringsBySplit(PInfoCertimg, PInfoCertimgRemark, ',');
            pregnanterInfo.PInfoCertimg = PInfoCertimg;

            //省市
            string prov = request.Params.Get("YuesaoProv");
            string city = request.Params.Get("YuesaoArea");
            string region = request.Params.Get("YuesaoRegion");
            pregnanterInfo.PInfoCity = String.Concat(new[] { prov, ",", city, ",", region });

            //最后跟踪
            pregnanterInfo.PInfoLastdate = DateTime.Now;

            if (PregnanterInfoService.EditPregnanterInfo(pregnanterInfo))
            {
                TPregnanterMaintenance pregnanterMaintenance = new TPregnanterMaintenance()
                {
                    PInfoCode = pregnanterInfo.Id,
                    PMainType = 1,
                    PMainContent = "修改了月嫂【" + pregnanterInfo.PInfoName + "】",
                    PMainCreater = (int)pregnanterInfo.PInfoCreateperson,
                    PMainCreatTime = DateTime.Now

                };
                try
                {
                    PregnanterMaintenanceService.AddPregnanterMaintenance(pregnanterMaintenance);
                    rmsg.Status = true;
                }
                catch (Exception)
                {
                    rmsg.Status = false;
                }

                rmsg.Status = true;
                rmsg.Status = true;
            }
            else
            {
                rmsg.Status = false;
            }


            return rmsg;
        }

        // POST /api/PregnanterInfoApi/EditPregnanterInfo
        /// <summary>
        ///修改信息 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="pregnanterInfo">pregnanterInfo</param>
        /// <returns>ResponseMessage</returns>
        [HttpPost]
        public ResponseMessage EditPregnanterInfos([FromBody] TPregnanterInfo pregnanterInfo)
        {
            ResponseMessage rmsg = new ResponseMessage();

            var context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象          
            pregnanterInfo.PInfoFenlei =2;

            //生活照片
            string PInfoLifeimg = request.Params.Get("PInfoLifeimg") ?? "";
            //0|/Upload/201410/18/201410180324093440.jpg|/Upload/201410/18/small_201410180324093440.jpg,0|/Upload/201410/18/201410180324094720.jpg|/Upload/201410/18/small_201410180324094720.jpg,0|/Upload/201410/18/201410180324096350.jpg|/Upload/201410/18/small_201410180324096350.jpg,0|/Upload/201410/18/201410180324098391.jpg|/Upload/201410/18/small_201410180324098391.jpg
            string PInfoLifeimgRemark = request.Params.Get("PInfoLifeimgRemark") ?? "";
            PInfoLifeimg = Utils.UnionStringsBySplit(PInfoLifeimg, PInfoLifeimgRemark, ',');
            pregnanterInfo.PInfoLifeimg = PInfoLifeimg;

            //证件照片
            string PInfoCertimg = request.Params.Get("PInfoCertimg") ?? "";
            string PInfoCertimgRemark = request.Params.Get("PInfoCertimgRemark") ?? "";
            PInfoCertimg = Utils.UnionStringsBySplit(PInfoCertimg, PInfoCertimgRemark, ',');
            pregnanterInfo.PInfoCertimg = PInfoCertimg;

            //省市
            string prov = request.Params.Get("YuesaoProv");
            string city = request.Params.Get("YuesaoArea");
            string region = request.Params.Get("YuesaoRegion");
            pregnanterInfo.PInfoCity = String.Concat(new[] { prov, ",", city, ",", region });

            //最后跟踪
            pregnanterInfo.PInfoLastdate = DateTime.Now;

            if (PregnanterInfoService.EditPregnanterInfo(pregnanterInfo))
            {
                TPregnanterMaintenance pregnanterMaintenance = new TPregnanterMaintenance()
                {
                    PInfoCode = pregnanterInfo.Id,
                    PMainType = 1,
                    PMainContent = "修改了月嫂【" + pregnanterInfo.PInfoName + "】",
                    PMainCreater = (int)pregnanterInfo.PInfoCreateperson,
                    PMainCreatTime = DateTime.Now

                };
                try
                {
                    PregnanterMaintenanceService.AddPregnanterMaintenance(pregnanterMaintenance);
                    rmsg.Status = true;
                }
                catch (Exception)
                {
                    rmsg.Status = false;
                }

                rmsg.Status = true;
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

        // GET /api/PregnanterInfoApi/DeletePregnanterInfo/5       
        /// <summary>
        ///删除    2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="ids">要删除的ids集合</param>
        /// <returns>ResponseMessage.</returns>
        [HttpGet]
        public ResponseMessage DeletePregnanterInfo(string ids)
        {
            ResponseMessage rmsg = new ResponseMessage();
            if (PregnanterInfoService.DeletePregnanterInfo(ids))
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

        // GET /api/PregnanterInfoApi/UpdatePregnanterInfoStatus/5
        /// <summary>
        /// 修改状态 2014-10-15 22:11:19 By 唐有炜
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ResponseMessage</returns>
        [HttpGet]
        public ResponseMessage UpdatePregnanterInfoStatus(string ids,int status)
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
                    Key = "PInfoState",
                    Value = status,
                    Predicate = "Id=@" + i
                };
                fields.Add(field);

                //参数
                var id = int.Parse(idArr[i].ToString());
                parms[i] = id;
            }

            if (PregnanterInfoService.UpdateEntityStatus(fields, parms))
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






        #region 验证  2014-10-16 17:25:33 By 唐有炜

        // /api/PregnanterInfoApi/Validate?fields=1&eqs=&values=&operations=2
        /// <summary>
        /// 验证字段是否存在  2014-10-15 14:36:33 By 唐有炜(注意：字段必须与条件一一对应)
        /// </summary>
        /// <param name="fields">字段集合(id,name)</param>
        /// <param name="eqs">The eqs.</param>
        /// <param name="values">The values.</param>
        /// <param name="operations">条件集合（or,and）</param>
        /// <param name="existReturn">存在时的返回结果（默认true）</param>
        /// <returns>验证状态</returns>
        [HttpGet]
        public HttpResponseMessage Validate(string fields, string eqs, string values, string operations,bool existReturn=true)
        {
            //构造参数
            var Status = false;
            object[] parms = null;
            string predicate = Utils.BuildPredicate(fields, eqs, values, operations, out parms);

            //记录日志
            LogHelper.Debug("验证存在predicate：存在（true），不存在（false）" + predicate+ "【参数】" + String.Concat(parms));

            //发送请求
            if (PregnanterInfoService.Validate(predicate, parms))
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

            //输出结果
            var rmsg = new HttpResponseMessage
            {
                Content = new StringContent(Status.ToString().ToLower(), System.Text.Encoding.UTF8, "text/plain")
            };
            return rmsg;
        }

        #endregion












    }
}