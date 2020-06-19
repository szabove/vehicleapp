using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters.Contracts
{
    public interface ISorter
    {
        string SortBy { get; set; }
        string SortDirection { get; set; }

        IQueryable<T> SortDataAscending<T>(IQueryable<T> data, Expression<Func<T, object>> expression) where T : class;
        IQueryable<T> SortDataDescending<T>(IQueryable<T> data, Expression<Func<T, object>> expression) where T : class;
        Expression<Func<T, object>> GetExpressionToSortBy<T>(string sortByProperty) where T : class;
        string ConvertParameterToProperty<T>(string parameter) where T : class;
        IQueryable<T> GetSortingQuery<T>(IQueryable<T> data, string SortByParameter, string sortByDirection) where T : class;
    }
}
