using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using Xunit;

namespace VehicleApp.Services.Tests
{
    public class VehicleModelServiceTests
    {
        Mock<IVehicleModelRepository> RepositoryMock = new Mock<IVehicleModelRepository>();
        VehicleModelService VehicleModelService;
        Mock<IModelFilter> FilterMock = new Mock<IModelFilter>();
        Mock<ISorter> SorterMock = new Mock<ISorter>();
        Mock<IPagination> PaginationMock = new Mock<IPagination>();

        public VehicleModelServiceTests()
        {
            VehicleModelService = new VehicleModelService(RepositoryMock.Object);
        }

        [Fact]
        public async Task Add_ShouldAddModel()
        {
            //Arrange

            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.Id = Guid.NewGuid();
            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<IVehicleModel>())).ReturnsAsync(1);
            //Act

            var result = await VehicleModelService.Add(vehicleModel.Object);

            //Assert
            result.Should().Be(1);
            RepositoryMock.Verify(x => x.AddAsync(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddModeBecauseReturnsZeroFromRepository()
        {
            //Arrange

            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.Id = Guid.NewGuid();
            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<IVehicleModel>())).ReturnsAsync(0);
            //Act

            var result = await VehicleModelService.Add(vehicleModel.Object);

            //Assert
            result.Should().Be(0);
            RepositoryMock.Verify(x => x.AddAsync(It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldDeleteModel()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();

            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(1);

            //Act

            var result = await VehicleModelService.Delete(generatedGuid);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteModelBecauseRepoDeleteMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();

            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act

            var result = await VehicleModelService.Delete(generatedGuid);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdateModel()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.Id = Guid.NewGuid();
            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(1);

            //Act

            var result = await VehicleModelService.Update(generatedGuid, vehicleModel.Object);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateModelBecauseRepoUpdateMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.Id = Guid.NewGuid();
            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(0);

            //Act

            var result = await VehicleModelService.Update(generatedGuid, vehicleModel.Object);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
        }

        [Fact]
        public  async Task FindAsync_ShouldReturnFilteredSortedPaginatedResponseCollection()
        {
            //Arrange

            var makeId = Guid.NewGuid();

            List<VehicleModel> vehicleModels = new List<VehicleModel>() 
            { 
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "X5", VehicleMakeId = makeId},
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "M3 GTR", VehicleMakeId = makeId},
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "E220", VehicleMakeId = makeId}
            }; 
            
            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>(vehicleModels, 1, 10);

            RepositoryMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync(responseCollection);

            //Act

            var result = await VehicleModelService.FindAsync(FilterMock.Object, SorterMock.Object, PaginationMock.Object);

            //Assert
            result.Should().NotBeNull();
            result.Data.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnReturnNullBecauseNothingReturned()
        {
            //Arrange

            var makeId = Guid.NewGuid();

            List<VehicleModel> vehicleModels = new List<VehicleModel>()
            {
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "X5", VehicleMakeId = makeId},
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "M3 GTR", VehicleMakeId = makeId},
                new VehicleModel(){ Id = Guid.NewGuid(), Name = "E220", VehicleMakeId = makeId}
            };

            ResponseCollection<IVehicleModel> responseCollection = new ResponseCollection<IVehicleModel>(vehicleModels, 1, 10);

            RepositoryMock.Setup(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>())).ReturnsAsync((ResponseCollection<IVehicleModel>)null);

            //Act

            var result = await VehicleModelService.FindAsync(FilterMock.Object, SorterMock.Object, PaginationMock.Object);

            //Assert
            result.Should().BeNull();
            RepositoryMock.Verify(x => x.FindAsync(It.IsAny<IModelFilter>(), It.IsAny<ISorter>(), It.IsAny<IPagination>()), Times.Once);
        }

    }
}
