// ***********************************************************************
// Assembly         : Ed.Web
// Author           : Tangyouwei
// Created          : 2014-10-16 02:23:17
//
// Last Modified By : Tangyouwei
// Last Modified On : 2014-10-16 02:23:17
// ***********************************************************************
// <copyright file="DataBackupApiController.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Configuration;
using System.IO;
using System.Web;
using Ed.Common;
using Ed.Entity;
//ReSharper disable All 禁止ReSharper显示警告
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// The Controllers namespace.
/// </summary>

namespace Ed.Web.Areas.Settings.Controllers
{
    /// <summary>
    /// Class DataBackupApiController.
    /// 2014-10-16 02:23:17 修改 By 唐有炜
    /// </summary>
    public class DataBackupApiController : ApiController
    {
        #region 初始化

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string conntionString = ConfigurationManager.ConnectionStrings["EdSqlServer"].ConnectionString;

        private static Dictionary<string, object> names;
        //数据库备份主机、用户名、密码
        private static object hostName;
        private static object userName;
        private static object userPwd;
        private static object dbName;

        static DataBackupApiController()
        {
            names = Utils.StringToObjectDictionary(conntionString, ';', '=');
            names.TryGetValue("Data Source", out hostName);
            names.TryGetValue("Initial Catalog", out dbName);
            names.TryGetValue("User ID", out userName);
            names.TryGetValue("Password", out userPwd);
        }

        #endregion

        #region  获取Bootgrid Databackup列表 2014-10-15 22:07:40 By 唐有炜

        //POST /api/DatabackupApi/GetBootGridDatabackups?current=1&rowCount=10&sort[field_name]=DESC&searchPhrase=
        /// <summary>
        /// 获取Bootgrid Databackup列表 2014-10-15 22:07:40 By 唐有炜
        /// </summary>
        /// <param name="current">current</param>
        /// <param name="rowCount">rowCount</param>
        /// <param name="sort[fieldName]">sort[fieldName]</param>
        /// <param name="searchPhrase">searchPhrase(格式)</param>
        /// <returns>json</returns>
        [HttpPost]
        public BootGrid GetBootGridDatabackups()
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


            //获取数据
            DirectoryInfo TheFolder = new DirectoryInfo(String.Concat(new[] {HttpRuntime.AppDomainAppPath, "\\Backup"}));


            //遍历文件夹
            IEnumerable<FileInfo> FileInfo;
            if (sortType.ToLower() == "desc")
            {
                FileInfo = TheFolder.GetFiles().AsQueryable().OrderByDescending(f => f.Name);
            }
            else
            {
                FileInfo = TheFolder.GetFiles().AsQueryable().OrderBy(f => f.Name);
            }


            //组装输出
            var bootGrid = new BootGrid
            {
                current = current,
                rowCount = rowCount,
                rows = FileInfo,
                total = total
            };
            return bootGrid;
        }

        #endregion

        #region 数据库信息

        // GET api/databackupapi
        /// <summary>
        /// 获取数据库连接信息 2014-10-15 22:07:40 By 唐有炜
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public Dictionary<string, object> Get()
        {
            return names;
        }

        // GET api/databackupapi/5
        /// <summary>
        /// 数据库连接信息（长字符串） 2014-10-15 22:07:40 By 唐有炜
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        public string Get(int id)
        {
            return conntionString;
        }

        #endregion

        #region 处理数据库备份

        [HttpGet]
        public ResponseMessage Backup()
        {
            ResponseMessage rmsg = new ResponseMessage();
            bool back_status = false;
            string errMsg = "";

            //路径处理
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fullname = HttpRuntime.AppDomainAppPath + "Backup\\" + filename + ".bak"; //包含文件的完整目录
            string path = Path.GetDirectoryName(fullname); //文件夹目录

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                //引用SQLDMO.dll，SQLDMO由Microsoft SQL Server自带的SQLDMO.dll提供，SQLDMO.dll是一个COM对象
                SQLDMO.Backup backup = new SQLDMO.BackupClass();
                SQLDMO.SQLServer sqlserver = new SQLDMO.SQLServerClass();
                sqlserver.LoginSecure = false;
                sqlserver.Connect(hostName, userName, userPwd);
                backup.Action = SQLDMO.SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                backup.Database = dbName.ToString();
                backup.Files = "[" + fullname + "]";
                backup.BackupSetName = dbName.ToString();
                backup.BackupSetDescription = "数据库备份";
                backup.Initialize = true;
                backup.SQLBackup(sqlserver);
                back_status = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                back_status = false;
                LogHelper.Error("数据库备份失败", ex);
            }


            if (back_status)
            {
                rmsg.Status = true;
                rmsg.Msg = "数据库备份成功！数据库备份路径：" + fullname + "。";
            }
            else
            {
                rmsg.Status = false;
                rmsg.Msg = "数据库备份失败！错误信息：" + errMsg + "请检查路径" + fullname +
                           "。";
            }

            return rmsg;
        }

        #endregion

        #region 处理数据库还原
        [HttpGet]
        public ResponseMessage Restore(string filename)
        {
            ResponseMessage rmsg = new ResponseMessage();
            bool resore_status = false;
            string errMsg = "";


            //路径处理
            string fullname = HttpRuntime.AppDomainAppPath + "Backup\\" + filename; //包含文件的完整目录
            string path = Path.GetDirectoryName(fullname); //文件夹目录

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            try
            {
                resore_status = RestoreDB(dbName.ToString(), fullname, out errMsg);
                //Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                resore_status = false;
            }


            if (resore_status)
            {
                rmsg.Status = true;
                rmsg.Msg = "数据库还原成功！数据库从路径：" + fullname + "还原。";
            }
            else
            {
                rmsg.Status = false;
                rmsg.Msg = "数据库还原失败！错误信息：" + errMsg + "请检查路径" + fullname +
                                  "。";
            }
            return rmsg;
        }


        /// <summary>
        /// 恢复数据库，恢复前杀死所有与本数据库相关进程
        /// </summary>
        /// <param name="strDbName">数据库名</param>
        /// <param name="strFileName">存放路径</param>
        /// <returns></returns>
        public bool RestoreDB(string strDbName, string strFileName, out string errMsg)
        {
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(hostName, userName, userPwd);
                //取得所有的进程列表
                SQLDMO.QueryResults qr = svr.EnumProcesses(-1);
                int iColPIDNum = -1;
                int iColDbName = -1;
                //找到和要恢复数据库相关的进程
                for (int i = 1; i <= qr.Columns; i++)
                {
                    string strName = qr.get_ColumnName(i);
                    if (strName.ToUpper().Trim() == "SPID")
                    {
                        iColPIDNum = i;
                    }
                    else if (strName.ToUpper().Trim() == "DBNAME")
                    {
                        iColDbName = i;
                    }
                    if (iColPIDNum != -1 && iColDbName != -1)
                        break;
                }
                //将相关进程杀死
                for (int i = 1; i <= qr.Rows; i++)
                {
                    int lPID = qr.GetColumnLong(i, iColPIDNum);
                    string strDBName = qr.GetColumnString(i, iColDbName);
                    if (strDBName.ToUpper() == strDbName.ToUpper())
                        svr.KillProcess(lPID);
                }

                SQLDMO.Restore res = new SQLDMO.RestoreClass();

                res.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                res.Files = strFileName;

                res.Database = strDbName;
                res.FileNumber = 1;

                res.ReplaceDatabase = true;
                res.SQLRestore(svr);

                errMsg = "";
                return true;
            }
            catch (Exception err)
            {
                errMsg = err.Message;
                return false;
            }
            finally
            {
                svr.DisConnect();
            }
        }

        #endregion

        #region 删除文件 

        // DELETE api/databackupapi/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        public ResponseMessage Delete(string filenames)
        {
            ResponseMessage rmsg = new ResponseMessage();
            object[] filenameArray = Utils.StringToObjectArray(filenames, ',');
            foreach (var filename in filenameArray)
            {
                //路径处理
                string fullname = HttpRuntime.AppDomainAppPath + "Backup\\" + filename; //包
                if (File.Exists(fullname))
                {
                    try
                    {
                        File.Delete(fullname);
                        rmsg.Status = true;
                    }
                    catch (Exception ex)
                    {
                        rmsg.Status = false;
                        LogHelper.Error("文件删除失败", ex);
                    }
                }
            }
            return rmsg;
        }

        #endregion
    }
}