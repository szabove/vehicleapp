using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;

namespace VehicleApp.Common
{
    public interface ISorter<T>
    {
        string sortBy { get; set; }
        string sortDirection { get; set; }
        Expression<Func<T, dynamic>> GetSortQuery();
        ICollection<T> SortData(ICollection<T> dataToSort, Expression<Func<T, dynamic>> sortQuery);
    }
}
