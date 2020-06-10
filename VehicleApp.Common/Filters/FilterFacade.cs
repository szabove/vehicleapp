using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Common.Filters
{
    public class FilterFacade : IFilterFacade
    {
        IMakeFilter MakeFilter;
        IModelFilter ModelFilter;
        IPagination Pagination;
        ISorter Sorter;
        //public FilterFacade(IMakeFilter makeFilter,
        //                    IModelFilter modelFilter,
        //                    IPagination pagination,
        //                    ISorter sorter
        //                    )
        //{
        //    MakeFilter = makeFilter;
        //    ModelFilter = modelFilter;
        //    Pagination = pagination;
        //    Sorter = sorter;
        //}

        public FilterFacade(IFilterAggregateService filterService)
        {
            MakeFilter = filterService.MakeFilter;
            ModelFilter = filterService.ModelFilter;
            Pagination = filterService.Pagination;
            Sorter = filterService.Sorter;
        }

        public IMakeFilter CreateMakeFilter()
        {
            return this.MakeFilter;
        }

        public IModelFilter CreateModelFilter()
        {
            return ModelFilter;
        }

        public ISorter CreateSorter()
        {
            return Sorter;
        }

        public IPagination CreatePagination()
        {
            return Pagination;
        }
    }
}
