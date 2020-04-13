using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VehicleApp.WebApi.App_Start;
using VehicleApp.WebApi.AutoMapperConfiguration;

namespace VehicleApp.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            MappingConfiguration.IncludeAllMappingProfiles();
            DIContainerConfig.RegisterComponents();

    //        //Temporary code
    //        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    //.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //        GlobalConfiguration.Configuration.Formatters
    //            .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
    //        //

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
 