using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ed.Common;
using Ed.Dao;
using Ed.Dao.Impl;
using Ed.Entity;

namespace Ed.Service.Impl
{
    public class SiteConfigServiceImpl : ISiteConfigService
    {
        private ISiteConfigDao SiteConfigDao = new SiteConfigDaoImpl();


        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Ed.Entity.SiteConfig LoadConfig(string configFilePath)
        {
            Ed.Entity.SiteConfig model = CacheHelper.Get<Ed.Entity.SiteConfig>(EdKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(EdKeys.CACHE_SITE_CONFIG, SiteConfigDao.loadConfig(configFilePath), configFilePath);
                model = CacheHelper.Get<Ed.Entity.SiteConfig>(EdKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        /// 读取客户端站点配置信息
        /// </summary>
        public Ed.Entity.SiteConfig loadConfig(string configFilePath, bool isClient)
        {
            Ed.Entity.SiteConfig model = CacheHelper.Get<Ed.Entity.SiteConfig>(EdKeys.CACHE_SITE_CONFIG_CLIENT);
            if (model == null)
            {
                model = SiteConfigDao.loadConfig(configFilePath);
                model.templateskin = model.webpath + "Themes/" + model.templateskin;
                CacheHelper.Insert(EdKeys.CACHE_SITE_CONFIG_CLIENT, model, configFilePath);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Ed.Entity.SiteConfig saveConifg(Ed.Entity.SiteConfig model, string configFilePath)
        {
            return SiteConfigDao.saveConifg(model, configFilePath);
        }
    }
}