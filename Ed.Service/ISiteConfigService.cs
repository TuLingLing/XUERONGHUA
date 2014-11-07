using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ed.Service
{

   public interface ISiteConfigService
   {

       /// <summary>
       /// 获取系统配置
       /// </summary>
       /// <returns></returns>
       Ed.Entity.SiteConfig LoadConfig(string configFilePath);
   }
}
