using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using VehicleApp.Common.Filters.Contracts;

namespace VehicleApp.Common.Filters
{
    public class Sorter : ISorter
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }

        public IQueryable<T> SortDataAscending<T>(IQueryable<T> data, Expression<Func<T, object>> expression) where T : class
        {
            return data.OrderBy(expression); 
        }

        public IQueryable<T> SortDataDescending<T>(IQueryable<T> data, Expression<Func<T, object>> expression) where T : class
        {
            return data.OrderByDescending(expression);
        }

        public Expression<Func<T, object>> GetExpressionToSortBy<T>(string sortByProperty) where T : class
        {
            var arg = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(arg, sortByProperty);
            var expression = Expression.Lambda<Func<T, object>>(property, new ParameterExpression[] { arg });
            return expression;
        }


        public string ConvertParameterToProperty<T>(string parameter) where T : class
        {
            Type type = typeof(T);

            var allProperties = type.GetProperties();

            foreach (var item in allProperties)
            {
                var lowercaseProperty = item.Name.ToLower();

                if (lowercaseProperty == parameter.ToLower())
                {
                    return item.Name;
                }
            }
            return null;
        }

        public IQueryable<T> GetSortingQuery<T>(IQueryable<T> data, string SortByParameter, string sortByDirection) where T : class
        {
            switch (sortByDirection)
            {
                case "asc":
                    return SortDataAscending<T>(data, GetExpressionToSortBy<T>(ConvertParameterToProperty<T>(SortByParameter)));
                case "desc":
                    return SortDataDescending<T>(data, GetExpressionToSortBy<T>(ConvertParameterToProperty<T>(SortByParameter)));
                default:
                    return null;
            }
        }

    }
}
