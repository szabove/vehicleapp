using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;

namespace VehicleApp.Services.DIConfiguration
{
    public class ServiceLayerDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>().InstancePerRequest();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>().InstancePerRequest();
        }
    }
}
