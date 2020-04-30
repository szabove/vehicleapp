using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public class CommonLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MakeFilter>().As<IMakeFilter>();
            builder.RegisterType<VehicleMakePagination>().As<IPagination<IVehicleMake>>();
        }
    }
}
