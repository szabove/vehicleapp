using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.Controllers;
using VehicleApp.WebApi.RestModels;
using VehicleApp.WebApi.ViewModels;
using Xunit;

namespace VehicleApp.WebApi.Tests
{
    public class VehicleModelControllerTests
    {
        VehicleModelController VehicleModelController;
        Mock<IVehicleModelService> ServiceMock = new Mock<IVehicleModelService>();
        Mock<IFilterFacade> FilterFacadeMock = new Mock<IFilterFacade>();
        Mock<IModelFilter> filterMock = new Mock<IModelFilter>();
        Mock<ISorter> sorterMock = new Mock<ISorter>();
        Mock<IPagination> paginationMock = new Mock<IPagination>();
        IMapper Mapper;

        public VehicleModelControllerTests()
        {
            Mapper = SetupAutomapper();
            VehicleModelController = new VehicleModelController(ServiceMock.Object, Mapper, FilterFacadeMock.Object);
        }

        public IMapper SetupAutomapper()
        {
            var config = new MapperConfiguration(
                cfg => cfg.AddMaps(new[] {
                    typeof(VehicleApp.WebApi.AutoMapperConfiguration.RestToDomainModelMapping)
                }));

            var mapper = config.CreateMapper();
            return mapper;
        }

        [Fact]
        public async Task AddVehicleModel_ShouldAddModel()
        {
            //Arrange

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleModel vehicleModel = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(1);
            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleModelController.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleModel_ShouldNotAddModelStatusCodeNotAcceptable()
        {
            //Arrange

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(0);
            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleModelController.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleModel_ShouldNotAddModelStatusCodeBadReqeust()
        {
            //Arrange

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(null);
            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleModelController.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleModel_ReturnVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(vehicleModel);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.GetVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Found);
            ServiceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleModel_ShouldNotReturnVehicleModelBecauseNotFound()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((IVehicleModel)null);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.GetVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ServiceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleModel_ShouldUpdateVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(1);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.UpdateVehicleModel(generatedGuid, modelRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleModel_ShouldNotUpdateVehicleModelBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(0);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.UpdateVehicleModel(generatedGuid, modelRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            ServiceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleModel_ShouldDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(1);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.DeleteVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleModel_ShouldNotDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = Mapper.Map<IVehicleModel>(modelRest);

            ServiceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleModelController.DeleteVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            ServiceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnResponseCollection()
        {
            //Arrange

            ModelFilterParams filterParams = new ModelFilterParams()
            {
                Search = "t",
                VehicleMakeId = Guid.NewGuid()
            };

            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<ModelRest> modelRests = new List<ModelRest>()
            {
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Corsa"},
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Astra"},
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Zafira"},
            };

            var vehilceModels = Mapper.Map<ICollection<IVehicleModel>>(modelRests);

            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>(vehilceModels, pageNumber, pageSize);


            filterMock.SetupAllProperties();
            sorterMock.SetupAllProperties();
            paginationMock.SetupAllProperties();

            FilterFacadeMock.Setup(x => x.CreateModelFilter()).Returns(filterMock.Object);
            FilterFacadeMock.Setup(x => x.CreatePagination()).Returns(paginationMock.Object);
            FilterFacadeMock.Setup(x => x.CreateSorter()).Returns(sorterMock.Object);

            ServiceMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync(responseCollection);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var result = await VehicleModelController.FindAsync(filterParams);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldNotReturnResponseCollectionBeacuseNotFound()
        {
            //Arrange

            ModelFilterParams filterParams = new ModelFilterParams()
            {
                Search = "t",
                VehicleMakeId = Guid.NewGuid()
            };

            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<ModelRest> modelRests = new List<ModelRest>()
            {
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Corsa"},
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Astra"},
                new ModelRest{ Id = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Zafira"},
            };

            var vehilceModels = Mapper.Map<ICollection<IVehicleModel>>(modelRests);

            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>(vehilceModels, pageNumber, pageSize);


            filterMock.SetupAllProperties();
            sorterMock.SetupAllProperties();
            paginationMock.SetupAllProperties();

            FilterFacadeMock.Setup(x => x.CreateModelFilter()).Returns(filterMock.Object);
            FilterFacadeMock.Setup(x => x.CreatePagination()).Returns(paginationMock.Object);
            FilterFacadeMock.Setup(x => x.CreateSorter()).Returns(sorterMock.Object);

            ServiceMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync((ResponseCollection<IVehicleModel>)null);

            VehicleModelController.Request = new HttpRequestMessage();
            VehicleModelController.Configuration = new HttpConfiguration();

            //Act

            var result = await VehicleModelController.FindAsync(filterParams);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ServiceMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

    }
}
