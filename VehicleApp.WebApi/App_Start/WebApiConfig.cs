using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VehicleApp.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.Add(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.MaxDepth = 1;
            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "VehicleMakeRoute",
            //    routeTemplate: "api/make/{controller}/{action}/{id}",
            //    defaults: new { controller = "VehicleMake", id = RouteParameter.Optional }
            //    );

            config.Routes.MapHttpRoute(
                name: "VehicleMake",
                routeTemplate: "api/vehicle-make/{id}",
                defaults: new { controller = "vehiclemake", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VehicleModel",
                routeTemplate: "api/vehicle-model/{id}",
                defaults: new { controller = "vehiclemodel", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            

        }
    }
}
