using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleApp.WebApi.AutoMapperConfiguration
{
    static public class MappingConfiguration
    {
        public static void IncludeAllMappingProfiles()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.AddMaps(new[] {
                    typeof(VehicleApp.WebApi.AutoMapperConfiguration.RestToDomainModelMapping)
                })
            );
        }
    }
}