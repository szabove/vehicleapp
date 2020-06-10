using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common.Filters;
using VehicleApp.Model.Common;
using Autofac.Extras.AggregateService;

namespace VehicleApp.Common
{
    public class CommonLayerDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAggregateService<IFilterAggregateService>();
            builder.RegisterType<FilterFacade>().As<IFilterFacade>().InstancePerRequest();
            builder.RegisterType<MakeFilter>().As<IMakeFilter>().InstancePerRequest();
            builder.RegisterType<ModelFilter>().As<IModelFilter>().InstancePerRequest();
            builder.RegisterType<Sorter>().As<ISorter>().InstancePerRequest();
            builder.RegisterType<Pagination>().As<IPagination>().InstancePerRequest();
        }
    }
}
