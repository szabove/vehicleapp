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
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleMake, IVehicleMake>().ReverseMap();
            CreateMap<IVehicleMake, VehicleMakeViewModel>().ReverseMap();

            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleModel, IVehicleModel>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}