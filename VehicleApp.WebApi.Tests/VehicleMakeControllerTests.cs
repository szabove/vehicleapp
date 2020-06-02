using AutoMapper;
using Moq;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Model.Common;
using VehicleApp.Services.Common;
using VehicleApp.WebApi.Controllers;
using VehicleApp.WebApi.ViewModels;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Web.Http.Results;
using VehicleApp.WebApi.RestModels;

namespace VehicleApp.WebApi.Tests
{
    public class VehicleMakeControllerTests
    {
        VehicleMakeController _sut;
        Mock<IVehicleMakeService> _serviceMock = new Mock<IVehicleMakeService>();
        Mock<IMakeFilter> _filterMock = new Mock<IMakeFilter>();
        Mock<ISorter<IVehicleMake>> _sorterMock = new Mock<ISorter<IVehicleMake>>();
        Mock<IPagination<IVehicleMake>> _paginationMock = new Mock<IPagination<IVehicleMake>>();
        IMapper _mapper;

        public VehicleMakeControllerTests()
        {
            _mapper = SetupAutomapper();
            _sut = new VehicleMakeController(_serviceMock.Object, _mapper, _filterMock.Object, _sorterMock.Object, _paginationMock.Object);
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
        public async Task AddVehicleMake_ShouldAddMake()
        {
            //Arrange

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(1);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleMake_ShouldNotAddMakeStatusCodeNotAcceptable()
        {
            //Arrange

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(0);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleMake_ShouldNotAddMakeStatusCodeBadReqeust()
        { 
            //Arrange

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(null);
            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();
            //Act

            var response = await _sut.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _serviceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleMake_ReturnVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(vehicleMake);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.GetVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Found);
            _serviceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleMake_ShouldNotReturnVehicleMakeBecauseNotFound()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((IVehicleMake)null);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.GetVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            _serviceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleMake_ShouldUpdateVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(1);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.UpdateVehicleMake(generatedGuid, makeRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleMake_ShouldNotUpdateVehicleMakeBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(0);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.UpdateVehicleMake(generatedGuid, makeRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            _serviceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleMake_ShouldDeleteVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(1);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.DeleteVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleMake_ShouldNotDeleteVehicleMakeBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                VehicleMakeId = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = _mapper.Map<IVehicleMake>(makeRest);

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var response = await _sut.DeleteVehicleMake(generatedGuid);

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

            List<MakeRest> makeRests = new List<MakeRest>()
            {
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "Opel"},
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "Toyota"},
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "BMW"},
            };

            var vehilceMakes = _mapper.Map<ICollection<IVehicleMake>>(makeRests);

            ResponseCollection<IVehicleMake> responseCollection = new ResponseCollection<IVehicleMake>()
            {
                Data = vehilceMakes,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _filterMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();

            _serviceMock.Setup(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<IPagination<IVehicleMake>>(), It.IsAny<ISorter<IVehicleMake>>())).ReturnsAsync(responseCollection);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var result = await _sut.FindAsync(_paginationQuery, search);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            _serviceMock.Verify(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<IPagination<IVehicleMake>>(), It.IsAny<ISorter<IVehicleMake>>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldNotReturnResponseCollectionBeacuseNotFound()
        {
            //Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<MakeRest> makeRests = new List<MakeRest>()
            {
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "Opel"},
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "Toyota"},
                new MakeRest{ VehicleMakeId = Guid.NewGuid(), Name = "BMW"},
            };

            var vehilceMakes = _mapper.Map<ICollection<IVehicleMake>>(makeRests);

            ResponseCollection<IVehicleMake> responseCollection = new ResponseCollection<IVehicleMake>()
            {
                Data = vehilceMakes,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            _filterMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();

            _serviceMock.Setup(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<IPagination<IVehicleMake>>(), It.IsAny<ISorter<IVehicleMake>>())).ReturnsAsync((ResponseCollection<IVehicleMake>)null);

            _sut.Request = new HttpRequestMessage();
            _sut.Configuration = new HttpConfiguration();

            //Act

            var result = await _sut.FindAsync(_paginationQuery, search);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            _serviceMock.Verify(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<IPagination<IVehicleMake>>(), It.IsAny<ISorter<IVehicleMake>>()), Times.Once);
        }

    }
}
