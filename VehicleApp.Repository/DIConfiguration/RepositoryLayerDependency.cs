using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;

namespace VehicleApp.Repository.DIConfiguration
{
    public class RepositoryLayerDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleContext>();
        }
    }
}
