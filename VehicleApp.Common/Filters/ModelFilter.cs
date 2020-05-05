using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Common.Filters
{
    public class ModelFilter : IModelFilter
    {
        public string Search { get; set; }
        public Guid VehicleMakeID { get; set; }

        public Expression<Func<IVehicleModel, bool>> GetFilterQuery()
        {
            var predicate = PredicateBuilder.New<IVehicleModel>(true);

            if (!string.IsNullOrEmpty(Search))
            {
                predicate = FilterByName(predicate);
            }

            if (VehicleMakeID != Guid.Empty)
            {
                predicate = FilterByMake(predicate); 
            }

            return predicate;
        }

        ExpressionStarter<IVehicleModel> FilterByName(ExpressionStarter<IVehicleModel> predicate)
        {
            Search.ToLower();
            return predicate = predicate.Start(x => x.Name.Contains(Search));
        }

        ExpressionStarter<IVehicleModel> FilterByMake(ExpressionStarter<IVehicleModel> predicate)
        {
            return predicate = predicate.And(x => x.VehicleMakeId == VehicleMakeID);
        }

    }
}
