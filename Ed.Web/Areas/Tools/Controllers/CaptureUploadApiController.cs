using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ed.Common;
using Ed.Entity;
using Ed.Service;
using Ed.Service.Impl;

namespace Ed.Web.Areas.Tools.Controllers
{
    public class CaptureUploadApiController : Controller
    {
        /// <summary>
        /// 站点配置信息
        /// </summary>
        private Ed.Entity.SiteConfig siteConfig;

        public CaptureUploadApiController()
        {
            //加载配置信息
            ISiteConfigService SiteConfig = new SiteConfigServiceImpl();
            siteConfig = SiteConfig.LoadConfig(Utils.GetXmlMapPath("siteConfigpath"));
        }

        //
        // GET: /Tools/CaptureUploadApi/

        public string Index()
        {
            //图1
            try
            {
                String pic = Request.Form["pic1"];

                byte[] bytes = Convert.FromBase64String(pic); //将2进制编码转换为8位无符号整数数组.

                string fileName = Request.QueryString["filename"] ; //随机文件名
                string dirPath = GetUpLoadPath(); //上传目录相对路径

                //获得要保存的文件路径
                string serverFileName = dirPath + fileName;
                //返回上蹿路径
                //string returnFileName = serverFileName;
                //物理完整路径                    
                string toFileFullPath = Utils.GetMapPath(dirPath);
                //检查有该路径是否就创建
                if (!Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }

                FileStream fs1 = new FileStream(System.Web.HttpContext.Current.Server.MapPath(serverFileName),
                    System.IO.FileMode.Create);
                fs1.Write(bytes, 0, bytes.Length);
                fs1.Close();
                return "{\"status\":1}";
            }
            catch(Exception ex)
            {
                LogHelper.Error("上传错误：",ex);
                return "{\"status\":-2}";
            }
        }

        #region 私有方法

        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <param name="fileName">上传文件名</param>
        private string GetUpLoadPath()
        {
            string path = siteConfig.webpath + siteConfig.attachpath + "/"; //站点目录+上传目录
            switch (this.siteConfig.attachsave)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        #endregion
    }
}