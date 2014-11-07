using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ed.Common;

namespace Ed.Dao.Impl
{
  public  class SiteConfigDaoImpl:ISiteConfigDao
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Ed.Entity.SiteConfig loadConfig(string configFilePath)
        {
            return (Ed.Entity.SiteConfig)SerializationHelper.Load(typeof(Ed.Entity.SiteConfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Ed.Entity.SiteConfig saveConifg(Ed.Entity.SiteConfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }
    }
}
