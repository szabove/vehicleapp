using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace VehicleApp.Common.Filters
{
    public class Sorter : ISorter
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }

        public IEnumerable<T> SortDataAscending<T>(IEnumerable<T> data, Expression<Func<T, object>> expression) where T : class
        {
            return data.AsQueryable().OrderBy(expression).AsEnumerable();
        }

        public IEnumerable<T> SortDataDescending<T>(IEnumerable<T> data, Expression<Func<T, object>> expression) where T : class
        {
            return data.AsQueryable().OrderByDescending(expression).AsEnumerable();
        }

        public Expression<Func<T, object>> GetExpressionToSortBy<T>(string sortByProperty) where T : class
        {
            PropertyInfo prop = typeof(T).GetProperty(ConvertParameterToProperty<T>(sortByProperty));

            return x => prop.GetValue(x, null);
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

        public IEnumerable<T> GetSortedData<T>(IEnumerable<T> data, string SortByParameter, string sortByDirection) where T : class
        {
            switch (sortByDirection)
            {
                case "asc":
                    return SortDataAscending<T>(data, GetExpressionToSortBy<T>(ConvertParameterToProperty<T>(SortByParameter)));
                case "desc":
                    return SortDataDescending<T>(data, GetExpressionToSortBy<T>(SortByParameter));
                default:
                    return data;
            }
        }

    }
}
