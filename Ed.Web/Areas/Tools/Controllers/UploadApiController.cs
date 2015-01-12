using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Ed.Common;
using Ed.Entity;
using Ed.Service;
using Ed.Service.Impl;
using Newtonsoft.Json;


namespace Ed.Web.Areas.Tools.Controllers
{

        public class UploadApiController : Controller
        {


            //站点配置信息
            protected SiteConfig siteConfig;
            //上传逻辑
            IUploadService upFiles = new UploadServiceImpl();

            //
            // GET: /Tools/UploadApi/

            public JsonResult Index()
            {
                //取得处事类型
                string action = EdRequest.GetQueryString("action");

                string msg = "";
                switch (action)
                {
                    case "SingleFile": //单文件
                        msg = SingleFile();
                        break;
                    case "MultipleFile": //多文件
                        msg = MultipleFile();
                        break;
                    case "AttachFile": //附件
                        msg = AttachFile();
                        break;
                    case "EditorFile": //编辑器文件
                        msg = EditorFile();
                        break;
                    case "ManagerFile": //管理文件
                        msg = ManagerFile();
                        break;
                    default: //普通上传
                       msg= UpLoadFile();
                        break;
                }
                return Json(msg);
            }


            #region 普通上传文件处理===================================
            private string UpLoadFile()
            {
                string _delfile = EdRequest.GetString("DelFilePath");
                HttpPostedFileBase _upfile = Request.Files["Filedata"];
                bool _iswater = true; //默认不打水印
                bool _isthumbnail = false; //默认不生成缩略图

                if (EdRequest.GetQueryString("IsWater") == "1")
                    _iswater = true;
                if (EdRequest.GetQueryString("IsThumbnail") == "1")
                    _isthumbnail = true;
                if (_upfile == null)
                {
                    return "{\"status\": 0, \"msg\": \"请选择要上传文件！\"}";
                }
                string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
                //删除已存在的旧文件
                if (!string.IsNullOrEmpty(_delfile))
                {
                    Utils.DeleteUpFile(_delfile);
                }
                //返回成功信息
                return msg;
            }
            #endregion

       #region 上传单文件处理===================================
            private string SingleFile()
            {

                string _refilepath = EdRequest.GetQueryString("ReFilePath"); //取得返回的对象名称
                string _upfilepath = EdRequest.GetQueryString("UpFilePath"); //取得上传的对象名称
                string _delfile = EdRequest.GetString(_refilepath);
                HttpPostedFileBase _upfile = Request.Files[_upfilepath];
                bool _iswater = true; //默认不打水印
                bool _isthumbnail = false; //默认不生成缩略图
                bool _isimage = false; //默认不限制图片上传

                if (EdRequest.GetQueryString("IsWater") == "1")
                    _iswater = true;
                if (EdRequest.GetQueryString("IsThumbnail") == "1")
                    _isthumbnail = true;
                if (EdRequest.GetQueryString("IsImage") == "1")
                    _isimage = true;

                if (_upfile == null)
                {
                    return "{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}";
                }
                IUploadService upFiles = new UploadServiceImpl();
                string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, _isimage);
                //删除已存在的旧文件
                Utils.DeleteUpFile(_delfile);
                //返回成功信息
                return msg;
            }
            #endregion

            #region 上传多文件处理===================================
            private string MultipleFile()
            {
                string _upfilepath = Request.QueryString["UpFilePath"]; //取得上传的对象名称
                HttpPostedFileBase _upfile = Request.Files[_upfilepath];
                bool _iswater = true; //默认不打水印
                bool _isthumbnail = false; //默认不生成缩略图

                if (Request.QueryString["IsWater"] == "1")
                    _iswater = true;
                if (Request.QueryString["IsThumbnail"] == "1")
                    _isthumbnail = true;

                if (_upfile == null)
                {
                    return "{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}";
                }
                IUploadService upFiles = new UploadServiceImpl();
                string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
                //返回成功信息
                return msg;
            }
            #endregion

            #region 上传附件处理=====================================
            private string AttachFile()
            {
                string _upfilepath = Request.QueryString["UpFilePath"]; //取得上传的对象名称
                HttpPostedFileBase _upfile = Request.Files[_upfilepath];
                bool _iswater = true; //默认不打水印
                bool _isthumbnail = false; //默认不生成缩略图

                if (Request.QueryString["IsWater"] == "1")
                    _iswater = true;
                if (Request.QueryString["IsThumbnail"] == "1")
                    _isthumbnail = true;

                if (_upfile == null)
                {
                    return "{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}";
                }

                string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false, true);
                //返回成功信息
                return msg;
            }
            #endregion

            #region 编辑器上传处理===================================
            private string EditorFile()
            {
                bool _iswater = true; //默认不打水印
                if (Request.QueryString["IsWater"] == "1")
                    _iswater = true;
                HttpPostedFileBase imgFile = Request.Files["imgFile"];
                if (imgFile == null)
                {
                    showError("请选择要上传文件！");
                }
                IUploadService upFiles = new UploadServiceImpl();
                string remsg = upFiles.fileSaveAs(imgFile, false, _iswater);
                //string pattern = @"^{\s*msg:\s*(.*)\s*,\s*msgbox:\s*\""(.*)\""\s*}$"; //键名前和键值前后都允许出现空白字符
                //Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
                //Match m = r.Match(remsg); //搜索匹配项
                //string msg = m.Groups[1].Value; //msg的值，正则表达式中第1个圆括号捕获的值
                //string msgbox = m.Groups[2].Value; //msgbox的值，正则表达式中第2个圆括号捕获的值 

                /*************************下面是使用Litjson,暂时注释
                JsonData jd = JsonMapper.ToObject(remsg);
                string msg = jd["msg"].ToString();
                string msgbox = jd["msgbox"].ToString();
                if (msg == "0")
                {
                    showError(context, msgbox);
                    return;
                }
                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = msgbox;
                Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                Response.Write(JsonMapper.ToJson(hash));
                Response.End();
                 */
                return remsg;
            }
            //显示错误
            private void showError(string message)
            {
                Hashtable hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = message;
                Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                Response.Write(JsonConvert.SerializeObject(hash));
                Response.End();
            }
            #endregion

            #region 浏览文件处理=====================================
            private string ManagerFile()
            {
                //加载配置信息
                ISiteConfigService SiteConfig = new SiteConfigServiceImpl();
                siteConfig = SiteConfig.LoadConfig(Utils.GetXmlMapPath("siteConfigpath"));
                //String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

                //根目录路径，相对路径
                String rootPath = siteConfig.webpath + siteConfig.attachpath + "/"; //站点目录+上传目录
                //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
                String rootUrl = siteConfig.webpath + siteConfig.attachpath + "/";
                //图片扩展名
                String fileTypes = "gif,jpg,jpeg,png,bmp";

                String currentPath = "";
                String currentUrl = "";
                String currentDirPath = "";
                String moveupDirPath = "";

                String dirPath = Utils.GetMapPath(rootPath);
                String dirName = Request.QueryString["dir"];
                //if (!String.IsNullOrEmpty(dirName))
                //{
                //    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                //    {
                //        Response.Write("Invalid Directory name.");
                //        Response.End();
                //    }
                //    dirPath += dirName + "/";
                //    rootUrl += dirName + "/";
                //    if (!Directory.Exists(dirPath))
                //    {
                //        Directory.CreateDirectory(dirPath);
                //    }
                //}

                //根据path参数，设置各路径和URL
                String path = Request.QueryString["path"];
                path = String.IsNullOrEmpty(path) ? "" : path;
                if (path == "")
                {
                    currentPath = dirPath;
                    currentUrl = rootUrl;
                    currentDirPath = "";
                    moveupDirPath = "";
                }
                else
                {
                    currentPath = dirPath + path;
                    currentUrl = rootUrl + path;
                    currentDirPath = path;
                    moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
                }

                //排序形式，name or size or type
                String order = Request.QueryString["order"];
                order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

                //不允许使用..移动到上一级目录
                if (Regex.IsMatch(path, @"\.\."))
                {
                    Response.Write("Access is not allowed.");
                    Response.End();
                }
                //最后一个字符不是/
                if (path != "" && !path.EndsWith("/"))
                {
                    Response.Write("Parameter is not valid.");
                    Response.End();
                }
                //目录不存在或不是目录
                if (!Directory.Exists(currentPath))
                {
                    Response.Write("Directory does not exist.");
                    Response.End();
                }

                //遍历目录取得文件信息
                string[] dirList = Directory.GetDirectories(currentPath);
                string[] fileList = Directory.GetFiles(currentPath);

                switch (order)
                {
                    case "size":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new SizeSorter());
                        break;
                    case "type":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new TypeSorter());
                        break;
                    case "name":
                    default:
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new NameSorter());
                        break;
                }

                Hashtable result = new Hashtable();
                result["moveup_dir_path"] = moveupDirPath;
                result["current_dir_path"] = currentDirPath;
                result["current_url"] = currentUrl;
                result["total_count"] = dirList.Length + fileList.Length;
                List<Hashtable> dirFileList = new List<Hashtable>();
                result["file_list"] = dirFileList;
                for (int i = 0; i < dirList.Length; i++)
                {
                    DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                    Hashtable hash = new Hashtable();
                    hash["is_dir"] = true;
                    hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                    hash["filesize"] = 0;
                    hash["is_photo"] = false;
                    hash["filetype"] = "";
                    hash["filename"] = dir.Name;
                    hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dirFileList.Add(hash);
                }
                for (int i = 0; i < fileList.Length; i++)
                {
                    FileInfo file = new FileInfo(fileList[i]);
                    Hashtable hash = new Hashtable();
                    hash["is_dir"] = false;
                    hash["has_file"] = false;
                    hash["filesize"] = file.Length;
                    hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                    hash["filetype"] = file.Extension.Substring(1);
                    hash["filename"] = file.Name;
                    hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dirFileList.Add(hash);
                }
                //Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
                //Response.Write(JsonConvert.SerializeObject(result));
                //Response.End();
                string rmsg = JsonConvert.SerializeObject(result);
                return rmsg;
            }

            #region Helper
            public class NameSorter : IComparer
            {
                public int Compare(object x, object y)
                {
                    if (x == null && y == null)
                    {
                        return 0;
                    }
                    if (x == null)
                    {
                        return -1;
                    }
                    if (y == null)
                    {
                        return 1;
                    }
                    FileInfo xInfo = new FileInfo(x.ToString());
                    FileInfo yInfo = new FileInfo(y.ToString());

                    return xInfo.FullName.CompareTo(yInfo.FullName);
                }
            }

            public class SizeSorter : IComparer
            {
                public int Compare(object x, object y)
                {
                    if (x == null && y == null)
                    {
                        return 0;
                    }
                    if (x == null)
                    {
                        return -1;
                    }
                    if (y == null)
                    {
                        return 1;
                    }
                    FileInfo xInfo = new FileInfo(x.ToString());
                    FileInfo yInfo = new FileInfo(y.ToString());

                    return xInfo.Length.CompareTo(yInfo.Length);
                }
            }

            public class TypeSorter : IComparer
            {
                public int Compare(object x, object y)
                {
                    if (x == null && y == null)
                    {
                        return 0;
                    }
                    if (x == null)
                    {
                        return -1;
                    }
                    if (y == null)
                    {
                        return 1;
                    }
                    FileInfo xInfo = new FileInfo(x.ToString());
                    FileInfo yInfo = new FileInfo(y.ToString());

                    return xInfo.Extension.CompareTo(yInfo.Extension);
                }
            }
            #endregion
            #endregion


        }


}
