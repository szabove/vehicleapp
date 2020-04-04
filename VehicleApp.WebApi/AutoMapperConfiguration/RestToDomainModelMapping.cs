using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    public class RestToDomainModelMapping : Profile
    {
        public RestToDomainModelMapping()
        {
            CreateMap<VehicleMakeDomainModel, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleMakeDomainModel, IVehicleMakeDomainModel>().ReverseMap();
            CreateMap<IVehicleMakeDomainModel, VehicleMakeViewModel>().ReverseMap();

            CreateMap<VehicleModelDomainModel, VehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleModelDomainModel, IVehicleModelDomainModel>().ReverseMap();
            CreateMap<IVehicleModelDomainModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}