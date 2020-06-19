using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.WebApi.Controllers;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    public class RestToDomainModelMapping : Profile
    {
        public RestToDomainModelMapping()
        {
            CreateMap<VehicleMake, MakeRest>().ReverseMap();
            CreateMap<IVehicleMake, MakeRest>().ReverseMap();
            CreateMap<ResponseCollection<IVehicleMake>, ResponseCollection<MakeRest>>().ReverseMap();

            CreateMap<VehicleModel, ModelRest>().ReverseMap();
            CreateMap<IVehicleModel, ModelRest>().ReverseMap();
            CreateMap<ResponseCollection<IVehicleModel>, ResponseCollection<ModelRest>>().ReverseMap();
        }
    }
}