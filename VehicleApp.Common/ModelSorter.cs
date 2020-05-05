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
        public Expression<Func<IVehicleModel, dynamic>> GetSortQuery()
        {
            sortBy.ToLower();
            switch (sortBy)
            {
                case "name":
                    Expression<Func<IVehicleModel, dynamic>> _sortByName = x => x.Name;
                    return _sortByName;
                case "abrv":
                    Expression<Func<IVehicleModel, dynamic>> _sortByAbrv = x => x.Abrv;
                    return _sortByAbrv;
                default:
                    return null;
            }
        }

        
    }
}
