using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.App_Start
{
    public static class DIContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            

            //builder.RegisterType<VehicleMakeService>().As<Services.Common.IVehicleMakeService>();

            

            return builder.Build();
        }
    }
}