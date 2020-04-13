using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Services;
using VehicleApp.Services.Common;

namespace VehicleApp.WebApi.DIConfiguration
{
    public class PresentationLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
        }
    }
}