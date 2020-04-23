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
    public class ServiceLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
            //
            //builder.RegisterType<Pagination>().As<IPagination>();
            builder.RegisterType<VehicleMakePaginationService>().As<IPaginationService<IVehicleMake>>();
            builder.RegisterType<VehicleModelPaginationService>().As<IPaginationService<IVehicleModel>>();
        }
    }
}
