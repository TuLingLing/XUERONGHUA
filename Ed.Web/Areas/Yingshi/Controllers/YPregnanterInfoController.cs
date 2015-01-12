using Ed.Common;
using Ed.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Service;
using Ed.Entity;


namespace Ed.Web.Areas.Yingshi.Controllers
{
    /// <summary>
    /// Class  YPregnanterInfoController.
    /// </summary>
    public class YPregnanterInfoController : Controller
    {
        #region  依赖注入 2014-10-16 15:05:36 By 唐有炜
        public ISysUserService SysUserService { set; get; }
        public ISysDepartmentService SysDepartmentService { set; get; }
        public IPregnanterInfoService PregnanterInfoService { set; get; }
        public IPregnanterServiceRecordService PregnanterServiceRecordService { set; get; }
        public IPregnanterEvaluationService PregnanterEvaluationService { set; get; }
        #endregion

        #region 页面展示  2014-10-11 10:21:24 By 唐有炜

        #region 首页
        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult List()
        {
            return View("List");
        }

        #endregion


        #region 回收站月嫂

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult Trash()
        {
            return View("Trash");
        }

        #endregion


        #region 回收站月嫂

        // GET: URL
        [UserAuthorize]
        [HttpGet]
        public ActionResult Closed()
        {
            return View("Closed");
        }

        #endregion


        #region 添加、修改页面

        ////GET: URL
        ////actionType (Add,Edit)
        [UserAuthorize]
        [HttpGet]
        public ActionResult Edit(string actionType, int? id)
        {
            //actionType
            ViewBag.ActionType = actionType;

            //修改
            if (actionType == EdEnums.ActionEnum.Edit.ToString())
            {
                if (null == id)
                {
                    ViewData["Msg"] = "参数非法（id不能为空）";
                    return View("_Error");
                }      
                var model = PregnanterInfoService.GetPregnanterInfo(temp => temp.Id == id);   
                var sysUser = SysUserService.GetFields("new(Id,UserLname,UserTname)", String.Concat(new[] { "Id=", model.PInfoCreateperson.ToString() })).ToList().FirstOrDefault();
                ViewBag.SysUser = sysUser;
                return View("Edit", model);
            }
            //添加
            else if (actionType == EdEnums.ActionEnum.Add.ToString())
            {
                var pregnanterinfo = new TPregnanterInfo();
                // var userId = Session[EdKeys.SESSION_USER_ID].ToString();
                //dynamic depObj = SysUserService.GetFields("NEW(DepId)", String.Concat(new[] { "Id=", userId })).FirstOrDefault();

                //var depId = depObj.DepId.ToString();
                // dynamic depObj2 = SysDepartmentService.GetFields("NEW(DepCode)", String.Concat(new[] { "Id=", depId })).FirstOrDefault();

                //var depCode = depObj2.DepCode.ToString();

                //pregnanterinfo.PInfoCode = RandomHelper.GetYuesaoNumber(depCode);
                ViewBag.SysUser = new TSysUser() { Id = int.Parse(Session[EdKeys.SESSION_USER_ID].ToString()), UserTname = Session[EdKeys.SESSION_USER_NAME].ToString() };
                return View("Edit", pregnanterinfo);
            }
            else
            {
                ViewData["Msg"] = "参数非法（actipnType不能为空）";
                return View("_Error");
            }
        }

        #endregion

        #region   查看页面
        //GET: URL
        /// <summary>
        /// Views the specified identifier.
        /// </summary>
        /// <param name="id">月嫂id</param>
        /// <returns>ActionResult.</returns>
        [UserAuthorize]
        [HttpGet]
        public ActionResult View(int? id)
        {
            if (null == id)
            {
                ViewData["Msg"] = "参数非法！";
                return View("_Error");
            }

            var count = 0;
            var orders1 = new Dictionary<string, EdEnums.OrderEmum>();
            orders1.Add("Id", EdEnums.OrderEmum.Desc);
            var orders2 = new Dictionary<string, EdEnums.OrderEmum>();
            orders2.Add("Id", EdEnums.OrderEmum.Desc);
            var servieRecord = PregnanterServiceRecordService.GetPregnanterServiceRecordList(1, 10, out count, s => s.PInfoCode == id, orders1).ToList();
            var eval = PregnanterEvaluationService.GetPregnanterEvaluationList(1, 10, "", "PInfoCode=@0", "ID desc", out count, new object[] { id });
                  
            ViewBag.ServiceRecord = servieRecord;
            ViewBag.Eval = eval;
            var model = PregnanterInfoService.GetPregnanterInfo(p => p.Id == id);
            return View("View", model);
        }
        #endregion

        #endregion
    }
}
