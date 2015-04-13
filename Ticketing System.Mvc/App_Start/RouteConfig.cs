using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ticketing_System.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Account", action = "login", id = UrlParameter.Optional });

            routes.MapRoute("LoginRoute", "Account/Login", new { controller = "Account", action = "Login" });
            routes.MapRoute("ProjectHomeRoute", "Projects/ListAll", new { controller = "Projects", action = "ListAll" });
            routes.MapRoute("TicketsHomeRoute", "Tickets/ListAll", new { controller = "Tickets", action = "ListAll",id=UrlParameter.Optional });
            routes.MapRoute("UsersHomeRoute", "Users/ListAll", new { controller = "Account", action = "ListAll", id = UrlParameter.Optional });
            routes.MapRoute("DashboardRoute", "Dashboard/Dashboard", new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional });
            routes.MapRoute("UserDashboardRoute", "UserDashboard/Dashboard", new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional });
            routes.MapRoute("ClientDashboardRoute", "ClientDashboard/Dashboard", new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional });
           
            routes.MapRoute("EditTicketRoute","Tickets/Edit",new {controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional });
            //edit project route
            routes.MapRoute("EditProjectRoute", "Projects/Edit", new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional });
            // reports routes
            routes.MapRoute("Reports_ProjectRoute", "ProjectReport/Home", new { controller = "ProjectReport", action = "Home", id = UrlParameter.Optional });
            routes.MapRoute("Reports_UserRoute", "UserReport/Home", new { controller = "UserReport", action = "Home", id = UrlParameter.Optional });
        }
    }
}
