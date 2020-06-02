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
        VehicleModelController _sut;
        Mock<IVehicleModelService> _serviceMock = new Mock<IVehicleModelService>();
        Mock<IModelFilter> _filterMock = new Mock<IModelFilter>();
        Mock<ISorter<IVehicleModel>> _sorterMock = new Mock<ISorter<IVehicleModel>>();
        Mock<IPagination<IVehicleModel>> _paginationMock = new Mock<IPagination<IVehicleModel>>();
        IMapper _mapper;

        public VehicleModelControllerTests()
        {
            _mapper = SetupAutomapper();
            _sut = new VehicleModelController(_serviceMock.Object, _mapper, _filterMock.Object, _sorterMock.Object, _paginationMock.Object);
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
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(1);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleModel_ShouldNotAddModelStatusCodeNotAcceptable()
        {
            //Arrange

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(0);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleModel_ShouldNotAddModelStatusCodeBadReqeust()
        {
            //Arrange

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(null);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleModel(modelRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleModel_ReturnVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(vehicleModel);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.GetVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Found);
            _serviceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleModel_ShouldNotReturnVehicleModelBecauseNotFound()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleModel = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((IVehicleModel)null);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.GetVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            _serviceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleModel_ShouldUpdateVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(1);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.UpdateVehicleModel(generatedGuid, modelRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleModel_ShouldNotUpdateVehicleModelBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(0);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.UpdateVehicleModel(generatedGuid, modelRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            _serviceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleModel_ShouldDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(1);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.DeleteVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleModel_ShouldNotDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            ModelRest modelRest = new ModelRest()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa"
            };

            IVehicleModel vehicleMake = _mapper.Map<IVehicleModel>(modelRest);

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.DeleteVehicleModel(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            _serviceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnResponseCollection()
        {
            //Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<ModelRest> modelRests = new List<ModelRest>()
            {
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Corsa"},
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Astra"},
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Zafira"},
            };

            var vehilceModels = _mapper.Map<ICollection<IVehicleModel>>(modelRests);

            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>()
            {
                Data = vehilceModels,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _filterMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();

            _serviceMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<IPagination<IVehicleModel>>(), It.IsAny<ISorter<IVehicleModel>>())).ReturnsAsync(responseCollection);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var result = await _sut.FindAsync(_paginationQuery, search);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<IPagination<IVehicleModel>>(), It.IsAny<ISorter<IVehicleModel>>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldNotReturnResponseCollectionBeacuseNotFound()
        {
            //Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<ModelRest> modelRests = new List<ModelRest>()
            {
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Corsa"},
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Astra"},
                new ModelRest{ VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Zafira"},
            };

            var vehilceModels = _mapper.Map<ICollection<IVehicleModel>>(modelRests);

            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>()
            {
                Data = vehilceModels,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _filterMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();

            _serviceMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<IPagination<IVehicleModel>>(), It.IsAny<ISorter<IVehicleModel>>())).ReturnsAsync((ResponseCollection<IVehicleModel>)null);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var result = await _sut.FindAsync(_paginationQuery, search);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            _serviceMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<IPagination<IVehicleModel>>(), It.IsAny<ISorter<IVehicleModel>>()), Times.Once);
        }

    }
}
