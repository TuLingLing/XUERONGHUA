using System.Web.Mvc;
using System.Web.Routing;
using Ed.Web.Handlers;

namespace Ed.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.Add("ImagesRoute",
            //      new Route("images/{filename}", new ImageRouteHandler()));
        
            routes.MapRoute(
                "Default", // 路由名称  
                "{controller}/{action}/{id}", // 带有参数的 URL  
                new {controller = "Default", action = "Index", id = UrlParameter.Optional}, // 参数默认值  
                new[] {"Ed.Web.Areas.Home"} //默认控制器的命名空间  
                ).DataTokens.Add("area", "Home"); //默认area 的控制器名称  
        }
    }
}