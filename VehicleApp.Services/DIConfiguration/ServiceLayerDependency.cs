using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Repository;
using VehicleApp.Repository.Common;

namespace VehicleApp.Services.DIConfiguration
{
    public class ServiceLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();
            builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>();
        }
    }
}
