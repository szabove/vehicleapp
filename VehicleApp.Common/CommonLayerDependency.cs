using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public class CommonLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MakeFilter>().As<IMakeFilter>().InstancePerRequest();
            builder.RegisterType<ModelFilter>().As<IModelFilter>().InstancePerRequest();
            builder.RegisterType<MakePagination>().As<IPagination<IVehicleMake>>().InstancePerRequest();
            builder.RegisterType<ModelPagination>().As<IPagination<IVehicleModel>>().InstancePerRequest();
            builder.RegisterType<MakeSorter>().As<ISorter<IVehicleMake>>().InstancePerRequest();
            builder.RegisterType<ModelSorter>().As<ISorter<IVehicleModel>>().InstancePerRequest();
        }
    }
}
