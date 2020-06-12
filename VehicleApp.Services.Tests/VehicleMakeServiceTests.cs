using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using Xunit;

namespace VehicleApp.Services.Tests
{
    public class VehicleMakeServiceTests
    {
        Mock<IVehicleMakeRepository> RepositoryMock = new Mock<IVehicleMakeRepository>();
        VehicleMakeService VehicleMakeService;

        public VehicleMakeServiceTests()
        {
            VehicleMakeService = new VehicleMakeService(RepositoryMock.Object);
        }

        [Fact]
        public async Task Add_ShouldAddMake()
        {
            //Arrange

            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.Id = Guid.NewGuid();
            vehicleMake.Object.Name = "Ford";

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<IVehicleMake>())).ReturnsAsync(1);
            //Act

            var result = await VehicleMakeService.Add(vehicleMake.Object);

            //Assert
            result.Should().Be(1);
            RepositoryMock.Verify(x => x.AddAsync(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddMakeBecauseReturnsZeroFromRepository()
        {
            //Arrange

            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.Id = Guid.NewGuid();
            vehicleMake.Object.Name = "Ford";

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<IVehicleMake>())).ReturnsAsync(0);
            //Act

            var result = await VehicleMakeService.Add(vehicleMake.Object);

            //Assert
            result.Should().Be(0);
            RepositoryMock.Verify(x => x.AddAsync(It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldDeleteMake()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            
            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(1);

            //Act

            var result = await VehicleMakeService.Delete(generatedGuid);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteMakeBecauseRepoDeleteMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();

            RepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act

            var result = await VehicleMakeService.Delete(generatedGuid);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdateMake()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.Id = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(1);

            //Act

            var result = await VehicleMakeService.Update(generatedGuid, vehicleMake.Object);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateMakeBecauseRepoUpdateMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.Id = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(0);

            //Act

            var result = await VehicleMakeService.Update(generatedGuid, vehicleMake.Object);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            RepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
        }

    }
}
