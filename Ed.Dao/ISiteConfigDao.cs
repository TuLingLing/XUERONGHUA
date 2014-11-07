using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ed.Dao
{
  public  interface ISiteConfigDao
  {
      /// <summary>
      ///  读取站点配置文件
      /// </summary>
      Ed.Entity.SiteConfig loadConfig(string configFilePath);


      /// <summary>
      /// 写入站点配置文件
      /// </summary>
      Ed.Entity.SiteConfig saveConifg(Ed.Entity.SiteConfig model, string configFilePath);
  }
}
