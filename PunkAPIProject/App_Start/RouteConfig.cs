using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PunkAPIProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Rating Query",
                url: "{action}",
                defaults: new { controller = "Beer", action = "GetRatings" }
            );

            routes.MapRoute(
                name: "Add Rating",
                url: "{action}/{id}",
                defaults: new { controller = "Beer", action = "PostRating", id = UrlParameter.Optional }
            );
        }
    }
}
