using System.Web.Mvc;

namespace Ed.Web.Areas.Yingshi
{
    public class YingshiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Yingshi"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Yingshi_default",
                "Yingshi/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
                //,
                //null,
                //new[] { "Ed.Web.Areas.Yingshi.Controllers" }
                );
        }
    }
}