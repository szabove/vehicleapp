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
using VehicleApp.Common.Filters;

namespace VehicleApp.WebApi.Tests
{
    public class VehicleMakeControllerTests
    {
        VehicleMakeController VehicleMakeController;
        Mock<IVehicleMakeService> ServiceMock = new Mock<IVehicleMakeService>();
        Mock<IFilterFacade> FilterFacadeMock = new Mock<IFilterFacade>();
        Mock<IMakeFilter> filterMock = new Mock<IMakeFilter>();
        Mock<ISorter> sorterMock = new Mock<ISorter>();
        Mock<IPagination> paginationMock = new Mock<IPagination>();
        IMapper Mapper;

        public VehicleMakeControllerTests()
        {
            Mapper = SetupAutomapper();
            VehicleMakeController = new VehicleMakeController(ServiceMock.Object, Mapper, FilterFacadeMock.Object);
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
                Id = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(1);
            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleMakeController.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleMake_ShouldNotAddMakeStatusCodeNotAcceptable()
        {
            //Arrange

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(0);
            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleMakeController.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task AddVehicleMake_ShouldNotAddMakeStatusCodeBadReqeust()
        {
            //Arrange

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.NewGuid(),
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(null);
            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();
            //Act

            var response = await VehicleMakeController.AddVehicleMake(makeRest);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            ServiceMock.Verify(x => x.Add(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleMake_ReturnVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(vehicleMake);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.GetVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Found);
            ServiceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetVehicleMake_ShouldNotReturnVehicleMakeBecauseNotFound()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync((IVehicleMake)null);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.GetVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ServiceMock.Verify(x => x.Get(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleMake_ShouldUpdateVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(1);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.UpdateVehicleMake(generatedGuid, makeRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleMake_ShouldNotUpdateVehicleMakeBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(0);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.UpdateVehicleMake(generatedGuid, makeRest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            ServiceMock.Verify(x => x.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleMake_ShouldDeleteVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(1);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.DeleteVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleMake_ShouldNotDeleteVehicleMakeBecauseForbidden()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            MakeRest makeRest = new MakeRest()
            {
                Id = Guid.Empty,
                Name = "Opel"
            };

            IVehicleMake vehicleMake = Mapper.Map<IVehicleMake>(makeRest);

            ServiceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var response = await VehicleMakeController.DeleteVehicleMake(generatedGuid);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            ServiceMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnResponseCollection()
        {
            //Arrange

            MakeFilterParams filterParams = new MakeFilterParams()
            {
                Search = "t"
            };

            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<MakeRest> makeRests = new List<MakeRest>()
            {
                new MakeRest{ Id = Guid.NewGuid(), Name = "Opel"},
                new MakeRest{ Id = Guid.NewGuid(), Name = "Toyota"},
                new MakeRest{ Id = Guid.NewGuid(), Name = "BMW"},
            };

            var vehilceMakes = Mapper.Map<IEnumerable<IVehicleMake>>(makeRests);

            ResponseCollection<IVehicleMake> responseCollection = new ResponseCollection<IVehicleMake>(vehilceMakes, pageNumber, pageSize)
            {
            };

            filterMock.SetupAllProperties();
            sorterMock.SetupAllProperties();
            paginationMock.SetupAllProperties();

            FilterFacadeMock.Setup(x => x.CreateMakeFilter()).Returns(filterMock.Object);
            FilterFacadeMock.Setup(x => x.CreatePagination()).Returns(paginationMock.Object);
            FilterFacadeMock.Setup(x => x.CreateSorter()).Returns(sorterMock.Object);
            ServiceMock.Setup(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync(responseCollection);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var result = await VehicleMakeController.FindAsync(filterParams);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            ServiceMock.Verify(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldNotReturnResponseCollectionBeacuseNotFound()
        {
            //Arrange

            MakeFilterParams filterParams = new MakeFilterParams()
            {
                Search = "t"
            };

            int pageNumber = 1;
            int pageSize = 10;
            string search = "a";
            PaginationQuery _paginationQuery = new PaginationQuery(pageNumber, pageSize);

            List<MakeRest> makeRests = new List<MakeRest>()
            {
                new MakeRest{ Id = Guid.NewGuid(), Name = "Opel"},
                new MakeRest{ Id = Guid.NewGuid(), Name = "Toyota"},
                new MakeRest{ Id = Guid.NewGuid(), Name = "BMW"},
            };

            var vehilceMakes = Mapper.Map<IEnumerable<IVehicleMake>>(makeRests);

            ResponseCollection<IVehicleMake> responseCollection = new ResponseCollection<IVehicleMake>(vehilceMakes, pageNumber, pageSize)
            {
            };

            filterMock.SetupAllProperties();
            sorterMock.SetupAllProperties();
            paginationMock.SetupAllProperties();

            FilterFacadeMock.Setup(x => x.CreateMakeFilter()).Returns(filterMock.Object);
            FilterFacadeMock.Setup(x => x.CreatePagination()).Returns(paginationMock.Object);
            FilterFacadeMock.Setup(x => x.CreateSorter()).Returns(sorterMock.Object);
            ServiceMock.Setup(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync((ResponseCollection<IVehicleMake>)null);

            VehicleMakeController.Request = new HttpRequestMessage();
            VehicleMakeController.Configuration = new HttpConfiguration();

            //Act

            var result = await VehicleMakeController.FindAsync(filterParams);

            //Assert

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            ServiceMock.Verify(x => x.FindAsync(It.IsAny<IMakeFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

    }
}
