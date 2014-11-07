using System.Web.Mvc;

namespace Ed.Web.Areas.Order
{
    public class OrderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Order";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Order_default",
                "Order/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                  null,
                new[] { "Ed.Web.Areas.Order.Controllers" }
            );
        }
    }
}
