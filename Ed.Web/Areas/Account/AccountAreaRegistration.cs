using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Ed.Web.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                new[] { "Ed.Web.Areas.Account.Controllers" }
            );
        }
    }
}
