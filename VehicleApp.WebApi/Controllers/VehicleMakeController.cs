using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [RoutePrefix("api/make")]
    public class VehicleMakeController : ApiController
    {
        private IVehicleMakeService _service;
        private IMapper _mapper;

        public VehicleMakeController(IVehicleMakeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> AddVehicleMake([FromBody]VehicleMakeViewModel vehicleMake)
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
        [Route("get/{id}")]
        public async Task<HttpResponseMessage> GetVehicleMake(Guid id)
        {

            if (id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = _mapper.Map<VehicleMake>( await _service.Get(id));

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
        public async Task<HttpResponseMessage> GetAllVehicleMakes()
        {
            try
            {
                var response = _mapper.Map<ICollection<VehicleMake>>(await _service.GetAll());
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<HttpResponseMessage> UpdateVehicleMake([FromBody]VehicleMakeViewModel vehicleMake)
        {
            try
            {
                var response = await _service.Update(_mapper.Map<VehicleMake>(vehicleMake));

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
