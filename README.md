雪绒花项目
=========

内部系统。使用Spring .NET、ASP .NET MVC、ELinq作为基本项目架构，同时使用Unit Test单元测试、Log4Net日志记录、SqlServer作为数据库、T4模板代码自动生成，Bootstrape作为前端框架。

版本信息
-------
>版本: V1.0     
>开发工具：Microsoft Visual Studio 2013      
>作者: Terwer,Huang   
>作者邮箱: cbgtyw@gmail.com   

项目简介
-------
>内部系统。

技术架构
-------
>1、本程序基于Asp .Net 4.0和.NET MVC4，编译版本为VS2013，最低运行版本为.Net 4.0，IIS 6.0+；  
>2、支持MySQL、SQLServer数据库切换，使用 ELinq作为ORM框架。    
>3、兼容IE6、7、8+、Firefox、Chrome、Safari             

更新历史
======
2014-11-19  
----------   
>1、实现月嫂手动评分并自动计算总分
>2、数据库字段添加(p_info_scert,p_info_sedu,p_info_squality,p_info_sknowledge,p_info_sservice,P_info_spicture,P_info_scomment,p_info_stotal)

关键点
=====
1、订单确认后，订单的维护人和客户的维护人都要变成自己    


程序发布
=============================
>1、.NET MVC项目Session需要额外配置        

<system.web>  
  <sessionState mode="StateServer" stateConnectionString="tcpip=127.0.0.1:42424" sqlConnectionString="data source=127.0.0.1;Trusted_Connection=yes" cookieless="false" timeout="720" />   
  </system.web>  
 `         
2、Log4Net发布到IIS后需要对User用户加上写入权限，必须在Common类的AssemblyInfo.cs里面加上     
 `
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", ConfigFileExtension = "config", Watch = true)]   
 `  ,Web根目录加上 log4net.config 属性改成复制到输出目录           
3、统计代码在_Layout.cshtml里面     
=============================

参考资料
=======
1、全文检索  
http://www.cnblogs.com/0000/archive/2009/07/27/1531707.html     
2、带扩展名的路由失效问题    
http://www.cnblogs.com/lori/p/4013135.html    
3、webapi支持session    
http://www.cnblogs.com/renzhendewo/archive/2013/04/08/3008389.html     

改进方案
========        
1 数据字典存储到配置文件（数据库）         
2                                                                                                                  





  

