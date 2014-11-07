using System.Web.Mvc;

namespace Ed.Web.Areas.Tools
{
    public class ToolsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tools";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tools_default",
                "Tools/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 null,
                new[] { "Ed.Web.Areas.Tools.Controllers" }
                );
        }
    }
}
