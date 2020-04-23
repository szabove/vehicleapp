using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.WebApi.RestModels;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    public class RequestToDomainModelMapping : Profile
    {
        public RequestToDomainModelMapping()
        {
            //from request to 
            CreateMap<PaginationQuery, IPagination>().ReverseMap();
            CreateMap<IPagination, Pagination>().ReverseMap();
        }
    }
}