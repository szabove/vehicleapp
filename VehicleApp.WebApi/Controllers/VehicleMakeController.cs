using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.RestModels;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.Controllers
{
    public class VehicleMakeController : ApiController
    {
        private IVehicleMakeService _service;
        private IMapper _mapper;
        private IMakeFilter _filter;

        public VehicleMakeController(IVehicleMakeService service,
                                    IMapper mapper,
                                    IMakeFilter filter
                                    )
        {
            _service = service;
            _mapper = mapper;
            _filter = filter;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddVehicleMake(MakeRest vehicleMake)
        {
            try
            {
                var response = await _service.Add(_mapper.Map<IVehicleMake>(vehicleMake));
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
                var response = _mapper.Map<MakeRest>( await _service.Get(id));

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
        public async Task<HttpResponseMessage> FindAsync([FromUri]PaginationQuery paginationQuery, string search)
        {

            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                _filter.Search = search;

                var paginationParameters = _mapper.Map<IPagination>(paginationQuery);

                var response = await _service.FindAsync(_filter, paginationParameters);

                var responseCollection = _mapper.Map<ResponseCollection<MakeRest>>(response);

                if (response.Data.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        //try
        //{

        //    if (paginationQuery == null)
        //    {
        //        var defaultPaginatedResponse = _mapper.Map<ICollection<MakeRest>>(await _service.GetAllSorted(abc));
        //        return Request.CreateResponse(HttpStatusCode.OK, new PagedResponse<MakeRest>(defaultPaginatedResponse));
        //    }

        //    //var fetchedData = await _service.GetAllSorted(abc);

        //    //var response = _mapper.Map<ICollection<VehicleMake>>();

        //    var paginationParams = _mapper.Map<IPagination>(paginationQuery);

        //    var pagedResponse = _pagination.PaginatedResult(fetchedData, paginationParams);

        //    var response = new PagedResponse<MakeRest>(_mapper.Map<ICollection<MakeRest>>(pagedResponse));

        //    response.SetPagingParams(paginationParams.PageNumber, paginationParams.PageSize);

        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}
        //catch (Exception ex)
        //{
        //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //}



        [HttpPut]
        public async Task<HttpResponseMessage> UpdateVehicleMake(Guid ID, MakeRest vehicleMake)
        {
            try
            {
                var response = await _service.Update(ID, _mapper.Map<VehicleMake>(vehicleMake));
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
            try
            {
                var response = await _service.Delete(id);
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
