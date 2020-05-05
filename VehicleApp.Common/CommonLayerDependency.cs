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
            builder.RegisterType<MakeFilter>().As<IMakeFilter>();
            builder.RegisterType<ModelFilter>().As<IModelFilter>();
            builder.RegisterType<MakePagination>().As<IPagination<IVehicleMake>>();
            builder.RegisterType<ModelPagination>().As<IPagination<IVehicleModel>>(); 
            builder.RegisterType<MakeSorter>().As<ISorter<IVehicleMake>>();
            builder.RegisterType<ModelSorter>().As<ISorter<IVehicleModel>>();
        }
    }
}
