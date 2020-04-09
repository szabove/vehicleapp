using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Repository.AutoMapperConfiguration
{
    public class DomainToEntityModelMapping: Profile
    {
        public DomainToEntityModelMapping()
        {
            CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
        }
    }
}
