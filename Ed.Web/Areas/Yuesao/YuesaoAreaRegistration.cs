using System.Web.Mvc;

namespace Ed.Web.Areas.Yuesao
{
    public class YuesaoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Yuesao"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Yuesao_default",
                "Yuesao/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
//                ,
//                null,
//                new[] { "Ed.Web.Areas.Yuesao.Controllers" }
                );
        }
    }
}