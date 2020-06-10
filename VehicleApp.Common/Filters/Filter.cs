using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public class Filter
    {
        //IPagination Pagination;
        //ISorter Sorter;
        //public Filter(IPagination pagination, ISorter sorter)
        //{
        //    Pagination = pagination;
        //    Sorter = sorter;
        //}

        //public IEnumerable<T> GetSortedData<T>(IEnumerable<T> data, string SortByParameter, string sortByDirection) where T : class
        //{
        //    switch (sortByDirection)
        //    {
        //        case "asc":
        //            return Sorter.SortDataAscending<T>(data, Sorter.GetExpressionToSortBy<T>(Sorter.ConvertParameterToProperty<T>(SortByParameter)));
        //        case "desc":
        //            return Sorter.SortDataDescending<T>(data, Sorter.GetExpressionToSortBy<T>(SortByParameter));
        //        default:
        //            return data;
        //    }
        //}

        //public IEnumerable<T> GetPaginatedData<T>(IEnumerable<T> data, int pageSize, int pageNumber) where T : class
        //{
        //    return Pagination.PaginatedResult(data, pageSize, pageNumber);
        //}

    }
}
