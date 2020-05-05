using System;
using System.Linq.Expressions;
using VehicleApp.Model.Common;

namespace VehicleApp.Common.Filters
{
    public interface IModelFilter
    {
        string Search { get; set; }
        Guid VehicleMakeID { get; set; }
        Expression<Func<IVehicleModel, bool>> GetFilterQuery();
    }
}