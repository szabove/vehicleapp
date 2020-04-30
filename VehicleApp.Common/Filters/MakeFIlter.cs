using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public class MakeFilter : IMakeFilter
    {
        public MakeFilter()
        {
        }

        public string Search { get; set; }

        public Expression<Func<IVehicleMake, bool>> GetFilterQuery()
        {
            var predicate = PredicateBuilder.New<IVehicleMake>(true);

            if (!string.IsNullOrEmpty(Search))
            {
                predicate = FilterByName(predicate);
            }

            return predicate;
        }
        
        //Filtering Functions
        ExpressionStarter<IVehicleMake> FilterByName(ExpressionStarter<IVehicleMake> predicate)
        {
            Search.ToLower();
            return predicate = predicate.Start(name => name.Name.ToLower().Contains(Search));
        }

    }
}
