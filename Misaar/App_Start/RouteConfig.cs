using Misaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Misaar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("ProductDetails", new SeoFriendlyRoute("products/{id}",
            new RouteValueDictionary(new { controller = "Product", action = "GetItem" }),
            new MvcRouteHandler()));
           
            routes.MapRoute(
                name:"about",
                url: "about",
                defaults: new { controller = "Home", action = "About"}
                );

            routes.MapRoute(
                name: "contacts",
                url: "contact-info",
                defaults: new { controller = "Home", action = "Contact" }
                );

            routes.MapRoute(
                name: "delivery",
                url: "dostavka",
                defaults: new { controller = "Home", action = "Delivery" }
                );

            routes.MapRoute(
                name: "feedback",
                url: "feedback",
                defaults: new { controller = "Home", action = "Feedback" }
                );

            routes.MapRoute(
                name: "cooperation",
                url: "cooperation",
                defaults: new { controller = "Home", action = "Cooperation" }
                );

            routes.MapRoute(
                name: "home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(
                name: "discount",
                url: "discount",
                defaults: new { controller = "Discount", action = "List" }
                );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
