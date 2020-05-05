using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.RestModels;
using VehicleApp.WebApi.ViewModels;

namespace VehicleApp.WebApi.Controllers
{
    public class VehicleModelController : ApiController
    {
        private IVehicleModelService _service;
        private IMapper _mapper;
        private IModelFilter _filter;
        private ISorter<IVehicleModel> _sorter;

        public VehicleModelController(IVehicleModelService service, 
                                        IMapper mapper,
                                        IModelFilter filter,
                                        ISorter<IVehicleModel> sorter
                                        )
        {
            _service = service;
            _mapper = mapper;
            _filter = filter;
            _sorter = sorter;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddVehicleModel(ModelRest vehicleModel)
        {
            try
            {
                var response = await _service.Add(_mapper.Map<IVehicleModel>(vehicleModel));
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
        public async Task<HttpResponseMessage> GetVehicleModel(Guid id)
        {

            if (id == Guid.Empty)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = _mapper.Map<ModelRest>(await _service.Get(id));

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
        public async Task<HttpResponseMessage> FindAsync([FromUri] PaginationQuery paginationQuery,
                                                            string search,
                                                            string makeID = "",
                                                            string sortBy = "name",
                                                            string sortDirection = "asc"
                                                        )
        {

            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                _filter.Search = search;

                if (Guid.TryParse(makeID, out var _makeID))
                {
                    _filter.VehicleMakeID = _makeID;
                }
                
                _sorter.sortBy = sortBy;
                _sorter.sortDirection = sortDirection;

                var paginationParameters = _mapper.Map<IPagination>(paginationQuery);

                var response = await _service.FindAsync(_filter, paginationParameters, _sorter);

                var responseCollection = _mapper.Map<ResponseCollection<ModelRest>>(response);

                if (response.Data.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, responseCollection);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateVehicleModel(Guid ID, ModelRest vehicleModel)
        {
            try
            {
                var response = await _service.Update(ID, _mapper.Map<VehicleModel>(vehicleModel));

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
        public async Task<HttpResponseMessage> DeleteVehicleModel(Guid id)
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
