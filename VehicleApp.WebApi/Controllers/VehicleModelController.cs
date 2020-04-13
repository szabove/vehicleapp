using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.Controllers
{
    [RoutePrefix("api/model")]
    public class VehicleModelController : ApiController
    {
        private IVehicleModelService _vehicleModelService;
        private IMapper _mapper;

        public VehicleModelController(IVehicleModelService vehicleMakeService, IMapper mapper)
        {
            _vehicleModelService = vehicleMakeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("add")]
        public async Task<HttpResponseMessage> AddVehicleModel(VehicleModelViewModel vehicleModel)
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
                var response = _mapper.Map<VehicleModel>(await _vehicleModelService.Get(id));

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
        [Route("getall")]
        public async Task<HttpResponseMessage> GetAllVehicleModels()
        {
            try
            {
                var response = _mapper.Map<VehicleModel>(await _vehicleModelService.GetAll());
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<HttpResponseMessage> UpdateVehicleModel([FromBody]VehicleModelViewModel vehicleModel)
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
