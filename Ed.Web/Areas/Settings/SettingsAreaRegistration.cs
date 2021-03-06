﻿using System.Web.Mvc;

namespace Ed.Web.Areas.Settings
{
    public class SettingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Settings"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Settings_default",
                "Settings/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                null,
                new[] {"Ed.Web.Areas.Settings.Controllers"}
                );
        }
    }
}