using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Common.Filters.Contracts;
using VehicleApp.Common.Filters.Parameters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.ActionFilters;

namespace VehicleApp.WebApi.Controllers
{
    public class VehicleModelController : ApiController
    {
        private IVehicleModelService ModelService;
        private IMapper Mapper;
        private IFilterFacade FilterFacade;

        public VehicleModelController(IVehicleModelService modelService, 
                                        IMapper mapper,
                                        IFilterFacade filterFacade
                                        )
        {
            ModelService = modelService;
            Mapper = mapper;
            FilterFacade = filterFacade;
        }

        [ValidateModelState]
        [HttpPost]
        public async Task<HttpResponseMessage> AddVehicleModel(ModelRest vehicleModel)
        {
            if (vehicleModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = await ModelService.Add(Mapper.Map<IVehicleModel>(vehicleModel));
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
                var response = Mapper.Map<ModelRest>(await ModelService.Get(id));

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
        public async Task<HttpResponseMessage> FindAsync([FromUri] ModelFilterParams filterParams)
        {

            try
            {
                IModelFilter filter = FilterFacade.CreateModelFilter();
                filter.Search = filterParams.Search;
                filter.VehicleMakeId = filterParams.VehicleMakeId;

                ISorter sorter = FilterFacade.CreateSorter();
                sorter.SortBy = filterParams.SortBy;
                sorter.SortDirection = filterParams.SortDirection;

                IPagination pagination = FilterFacade.CreatePagination();
                pagination.PageNumber = filterParams.PageNumber;
                pagination.RecordsPerPage = filterParams.RecordsPerPage;

                var response = await ModelService.FindAsync(filter, sorter, pagination);


                if (response == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                var responseCollection = Mapper.Map<ResponseCollection<ModelRest>>(response);

                return Request.CreateResponse(HttpStatusCode.OK, responseCollection);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ValidateModelState]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateVehicleModel(Guid ID, ModelRest vehicleModel)
        {
            if (ID == Guid.Empty || vehicleModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = await ModelService.Update(ID, Mapper.Map<VehicleModel>(vehicleModel));

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
            if (id == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var response = await ModelService.Delete(id);
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
    public class ModelRest
    {
        [Required]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        //public VehicleMakeViewModel VehicleMake { get; set; }
    }
}
