using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.ActionFilters;
using VehicleApp.WebApi.RestModels;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.Controllers
{
    public class VehicleMakeController : ApiController
    {
        private IVehicleMakeService MakeService;
        private IMapper Mapper;
        private IFilterFacade FilterFacade;

        public VehicleMakeController(IVehicleMakeService makeService,
                                    IMapper mapper,
                                    IFilterFacade filterFacade
                                    )
        {
            MakeService = makeService;
            Mapper = mapper;
            FilterFacade = filterFacade;
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<HttpResponseMessage> AddVehicleMake(MakeRest vehicleMake)
        {
            if (vehicleMake == null)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = await MakeService.Add(Mapper.Map<IVehicleMake>(vehicleMake));
                if (response == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpGet]
        public async Task<HttpResponseMessage> GetVehicleMake(Guid id)
        {

            if (id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = Mapper.Map<MakeRest>( await MakeService.Get(id));

                if (response == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.Found, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
                
        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync([FromUri]MakeFilterParams filterParams)
        {

            try
            {

                IMakeFilter filter = FilterFacade.CreateMakeFilter();
                filter.Search = filterParams.Search;

                ISorter sorter = FilterFacade.CreateSorter();
                sorter.SortBy = filterParams.SortBy;
                sorter.SortDirection= filterParams.SortDirection;

                IPagination pagination = FilterFacade.CreatePagination();
                pagination.PageNumber = filterParams.PageNumber;
                pagination.RecordsPerPage = filterParams.RecordsPerPage;

                var response = await MakeService.FindAsync(filter, sorter, pagination);

                if (response == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var responseCollection = Mapper.Map<ResponseCollection<MakeRest>>(response);

                return Request.CreateResponse(HttpStatusCode.OK, responseCollection);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ValidateModelState]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateVehicleMake(Guid id, MakeRest vehicleMake)
        {

            if (vehicleMake == null || id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            try
            {
                var response = await MakeService.Update(id, Mapper.Map<VehicleMake>(vehicleMake));
                if (response == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteVehicleMake(Guid id)
        {

            if (id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = await MakeService.Delete(id);
                if (response == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
        }

    }
}
