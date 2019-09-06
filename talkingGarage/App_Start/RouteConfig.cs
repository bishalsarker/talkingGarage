using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace talkingGarage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            */

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Login",
                url: "signin",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
                name: "Athorize",
                url: "api/athorize",
                defaults: new { controller = "Home", action = "Athorize" }
            );

            routes.MapRoute(
                name: "Maps Page",
                url: "maps/",
                defaults: new { controller = "Home", action = "Map" }
            );

            routes.MapRoute(
                name: "Direction",
                url: "direction/{book_id}",
                defaults: new { controller = "Home", action = "Direction" }
            );

            routes.MapRoute(
                name: "Checkpoint",
                url: "checkpoint/{log_id}",
                defaults: new { controller = "Home", action = "Checkpoint" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "signout",
                defaults: new { controller = "Home", action = "Logout" }
            );

            routes.MapRoute(
                name: "Hold",
                url: "hold/{lot_id}",
                defaults: new { controller = "Booking", action = "Hold" }
            );

            routes.MapRoute(
                name: "Get Time",
                url: "api/getRemaining/{book_id}",
                defaults: new { controller = "Booking", action = "getTimeRemaining" }
            );

            routes.MapRoute(
                name: "CheckIfOut",
                url: "api/checkifout/{log_id}",
                defaults: new { controller = "Log", action = "checkIfOut" }
            );

            routes.MapRoute(
                name: "Billing",
                url: "billing",
                defaults: new { controller = "Billing", action = "Index" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "payment",
                defaults: new { controller = "Billing", action = "Payments" }
            );

            routes.MapRoute(
                name: "Check In",
                url: "checkin/{lot_id}/{card_number}",
                defaults: new { controller = "Parkinglot", action = "CheckIn" }
            );

            routes.MapRoute(
                name: "Check Out",
                url: "checkout/{lot_id}/{card_number}",
                defaults: new { controller = "Parkinglot", action = "CheckOut" }
            );

            routes.MapRoute(
                name: "All Lots",
                url: "api/getLots",
                defaults: new { controller = "Parkinglot", action = "getLots" }
            );

            routes.MapRoute(
                name: "All Lot",
                url: "api/getLot/{lot_id}",
                defaults: new { controller = "Parkinglot", action = "getLot" }
            );

        }
    }
}
