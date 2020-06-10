using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    public class RestToDomainModelMapping : Profile
    {
        public RestToDomainModelMapping()
        {
            //EntityMappings
            CreateMap<VehicleMake, MakeRest>().ReverseMap();
            CreateMap<VehicleMake, IVehicleMake>().ReverseMap();
            CreateMap<IVehicleMake, MakeRest>().ReverseMap();
            CreateMap<ResponseCollection<IVehicleMake>, ResponseCollection<MakeRest>>().ReverseMap();

            CreateMap<VehicleModel, ModelRest>().ReverseMap();
            CreateMap<VehicleModel, IVehicleModel>().ReverseMap();
            CreateMap<IVehicleModel, ModelRest>().ReverseMap();
            CreateMap<ResponseCollection<IVehicleModel>, ResponseCollection<ModelRest>>().ReverseMap();

            ////FilterMappings
            //CreateMap<BaseFilterParams, IBaseFilterParams>().ReverseMap();
        }
    }
}