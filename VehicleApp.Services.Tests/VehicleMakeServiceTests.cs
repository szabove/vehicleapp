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
        Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        VehicleMakeService _sut;

        public VehicleMakeServiceTests()
        {
            _sut = new VehicleMakeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Add_ShouldAddMake()
        {
            //Arrange

            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.VehicleMakeId = Guid.NewGuid();
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);
            //Act

            var result = await _sut.Add(vehicleMake.Object);

            //Assert
            result.Should().Be(1);
            _mockUnitOfWork.Verify(x => x.Makes.Add(It.IsAny<IVehicleMake>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddMakeBecauseEmptyGuid()
        {
            //Arrange

            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.VehicleMakeId = Guid.Empty;
            vehicleMake.Object.Name = "Ford";

            //Act

            var result = await _sut.Add(vehicleMake.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Makes.Add(It.IsAny<IVehicleMake>()), Times.Never);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Add_ShouldNotAddMakeBecauseAlreadyAdded()
        {
            //Arrange

            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();

            vehicleMake.Object.VehicleMakeId = Guid.NewGuid();
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Add(It.IsAny<IVehicleMake>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Add(vehicleMake.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Makes.Add(It.IsAny<IVehicleMake>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteMake()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();
            vehicleMake.Object.VehicleMakeId = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Delete(It.IsAny<Guid>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            _mockUnitOfWork.Verify(x => x.Makes.Delete(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteMakeBecauseRepoDeleteMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();
            vehicleMake.Object.VehicleMakeId = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            _mockUnitOfWork.Verify(x => x.Makes.Delete(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldUpdateMake()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();
            vehicleMake.Object.VehicleMakeId = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await _sut.Update(generatedGuid, vehicleMake.Object);

            //Assert
            result.Should().Be(1);
            _mockUnitOfWork.Verify(x => x.Makes.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateMakeBecauseRepoUpdateMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleMake> vehicleMake = new Mock<IVehicleMake>();

            vehicleMake.SetupAllProperties();
            vehicleMake.Object.VehicleMakeId = generatedGuid;
            vehicleMake.Object.Name = "Ford";

            _mockUnitOfWork.Setup(x => x.Makes.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>())).ReturnsAsync(0);

            //Act
            var result = await _sut.Update(generatedGuid, vehicleMake.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Makes.Update(It.IsAny<Guid>(), It.IsAny<IVehicleMake>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

    }
}
