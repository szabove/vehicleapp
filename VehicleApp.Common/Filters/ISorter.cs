using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public interface ISorter
    {
        string SortBy { get; set; }
        string SortDirection { get; set; }

        IEnumerable<T> SortDataAscending<T>(IEnumerable<T> data, Expression<Func<T, object>> expression) where T : class;
        IEnumerable<T> SortDataDescending<T>(IEnumerable<T> data, Expression<Func<T, object>> expression) where T : class;
        Expression<Func<T, object>> GetExpressionToSortBy<T>(string sortByProperty) where T : class;
        string ConvertParameterToProperty<T>(string parameter) where T : class;
        IEnumerable<T> GetSortedData<T>(IEnumerable<T> data, string SortByParameter, string sortByDirection) where T : class;
    }
}
