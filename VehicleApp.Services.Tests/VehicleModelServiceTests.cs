using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using Xunit;

namespace VehicleApp.Services.Tests
{
    public class VehicleModelServiceTests
    {
        Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        VehicleModelService _sut;

        public VehicleModelServiceTests()
        {
            _sut = new VehicleModelService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Add_ShouldAddModel()
        {
            //Arrange

            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);
            //Act

            var result = await _sut.Add(vehicleModel.Object);

            //Assert
            result.Should().Be(1);
            _mockUnitOfWork.Verify(x => x.Models.Add(It.IsAny<IVehicleModel>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddModelBecauseEmptyGuid()
        {
            //Arrange

            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.VehicleMakeId = Guid.Empty;
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            //Act

            var result = await _sut.Add(vehicleModel.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Models.Add(It.IsAny<IVehicleModel>()), Times.Never);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Add_ShouldNotAddModelBecauseAlreadyAdded()
        {
            //Arrange

            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();

            vehicleModel.Object.VehicleMakeId = Guid.NewGuid();
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Add(It.IsAny<IVehicleModel>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Add(vehicleModel.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Models.Add(It.IsAny<IVehicleModel>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteModel()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();
            vehicleModel.Object.VehicleMakeId = generatedGuid;
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Delete(It.IsAny<Guid>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            _mockUnitOfWork.Verify(x => x.Models.Delete(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteModelBecauseRepoDeleteMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();
            vehicleModel.Object.VehicleMakeId = generatedGuid;
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Delete(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            _mockUnitOfWork.Verify(x => x.Models.Delete(It.IsAny<Guid>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldUpdateModel()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();
            vehicleModel.Object.VehicleMakeId = generatedGuid;
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(1);
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await _sut.Update(generatedGuid, vehicleModel.Object);

            //Assert
            result.Should().Be(1);
            _mockUnitOfWork.Verify(x => x.Models.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateModelBecauseRepoUpdateMethodCallReturnsZero()
        {
            //Arange

            var generatedGuid = Guid.NewGuid();
            Mock<IVehicleModel> vehicleModel = new Mock<IVehicleModel>();

            vehicleModel.SetupAllProperties();
            vehicleModel.Object.VehicleMakeId = generatedGuid;
            vehicleModel.Object.VehicleModelId = Guid.NewGuid();
            vehicleModel.Object.Name = "Focus";

            _mockUnitOfWork.Setup(x => x.Models.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>())).ReturnsAsync(0);

            //Act
            var result = await _sut.Update(generatedGuid, vehicleModel.Object);

            //Assert
            result.Should().Be(0);
            _mockUnitOfWork.Verify(x => x.Models.Update(It.IsAny<Guid>(), It.IsAny<IVehicleModel>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }

    }
}
