using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using System.Linq.Expressions;

namespace VehicleApp.Common
{
    public class ModelSorter : ISorter<IVehicleModel>
    {
        public string sortBy { get; set; }
        public string sortDirection { get; set; }

        public Expression<Func<IVehicleModel, dynamic>> GetSortQuery()
        {
            sortBy.ToLower();
            switch (sortBy)
            {
                case "name":
                    Expression<Func<IVehicleModel, dynamic>> temp = x => x.Name;
                    return temp;

                default:
                    return null;
            }
        }

        public ICollection<IVehicleModel> SortData(ICollection<IVehicleModel> dataToSort, System.Linq.Expressions.Expression<Func<IVehicleModel, dynamic>> sortQuery)
        {
            sortDirection.ToLower();

            switch (sortDirection)
            {
                case "asc":
                    return dataToSort.AsQueryable().OrderBy(sortQuery).ToList();
                case "desc":
                    return dataToSort.AsQueryable().OrderByDescending(sortQuery).ToList();
                default:
                    return dataToSort.AsQueryable().OrderBy(sortQuery).ToList();
            }
        }
    }
}
