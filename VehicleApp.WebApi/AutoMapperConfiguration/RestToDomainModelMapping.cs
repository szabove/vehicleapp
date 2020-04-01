﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleApp.Model;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    public class RestToDomainModelMapping : Profile
    {
        public RestToDomainModelMapping()
        {
            CreateMap<VehicleMakeDomainModel, VehicleMakeViewModel>();
        }
    }
}