using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/model")]
    public class VehicleModelController : ApiController
    {
        private IVehicleModelService _vehicleModelService;
        private IMapper _mapper;
        private IPaginationService<IVehicleModel> _paginationService;

        public VehicleModelController(IVehicleModelService vehicleMakeService, IMapper mapper, IPaginationService<IVehicleModel> paginationService)
        {
            _vehicleModelService = vehicleMakeService;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> AddVehicleModel(ModelRest vehicleModel)
        {
            try
            {
                var response = await _vehicleModelService.Add(_mapper.Map<IVehicleModel>(vehicleModel));
                if (response == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<HttpResponseMessage> GetVehicleModel(Guid id)
        {

            if (id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = _mapper.Map<ModelRest>(await _vehicleModelService.Get(id));

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
        [Route("getall/{id}")]
        public async Task<HttpResponseMessage> GetAllVehicleModels(Guid id, [FromUri] PaginationQuery paginationQuery, string abc = "")
        {

            if (id == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (paginationQuery == null)
                {
                    var defaultPaginatedResponse = _mapper.Map<ICollection<ModelRest>>(await _vehicleModelService.GetAllModelsFromMake(id, abc));
                    return Request.CreateResponse(HttpStatusCode.OK, defaultPaginatedResponse);
                }

                var fetchedData = await _vehicleModelService.GetAllModelsFromMake(id, abc);

                //var response = _mapper.Map<ICollection<VehicleMake>>();

                var paginationParams = _mapper.Map<IPagination>(paginationQuery);

                var pagedResponse = _paginationService.PaginatedResult(fetchedData, paginationParams);

                var response = new PagedResponse<ModelRest>(_mapper.Map<ICollection<ModelRest>>(pagedResponse));

                response.SetPagingParams(paginationParams.PageNumber, paginationParams.PageSize);

                return Request.CreateResponse(HttpStatusCode.OK, response);


                //var response = _mapper.Map<ICollection<VehicleModel>>(await _vehicleModelService.GetAllModelsFromMake(id, abc));
                //return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<HttpResponseMessage> UpdateVehicleModel(ModelRest vehicleModel)
        {
            try
            {
                var response = await _vehicleModelService.Update(_mapper.Map<VehicleModel>(vehicleModel));

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

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<HttpResponseMessage> DeleteVehicleModel(Guid id)
        {
            try
            {
                var response = await _vehicleModelService.Delete(id);
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
