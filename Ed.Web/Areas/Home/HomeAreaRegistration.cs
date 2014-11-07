using System.Web.Mvc;

namespace Ed.Web.Areas.Home
{
    public class HomeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Home_default",
                "Home/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                    new[] { "Ed.Web.Areas.Home.Controllers" } //默认控制器的命名空间  
            );
        }
    }
}
