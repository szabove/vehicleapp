using Autofac;
using VehicleApp.DAL;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository.DIConfiguration
{
    public class RepositoryLayerDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleContext>().As<IVehicleContext>().InstancePerRequest();
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>().InstancePerRequest();
            builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
