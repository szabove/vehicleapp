using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Repository.DIConfiguration;
using VehicleApp.Services;
using VehicleApp.Services.Common;
using VehicleApp.Services.DIConfiguration;
using VehicleApp.WebApi.Controllers;

namespace VehicleApp.WebApi.App_Start
{
    public static class DIContainerConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<VehicleMakeController>().InstancePerRequest();
            builder.RegisterType<VehicleModelController>().InstancePerRequest();

            builder.RegisterModule<ServiceLayerDependency>();
            builder.RegisterModule<RepositoryLayerDependency>();
            builder.RegisterModule<CommonLayerDependency>();

            //Automapper
            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(new[] {
                    typeof(VehicleApp.WebApi.AutoMapperConfiguration.RestToDomainModelMapping),
                    typeof(VehicleApp.Repository.AutoMapperConfiguration.DomainToEntityModelMapping)
                }))).SingleInstance();

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

            var container = builder.Build();

            //Global Http configuration
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}