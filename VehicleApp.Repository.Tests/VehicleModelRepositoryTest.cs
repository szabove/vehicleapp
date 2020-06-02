using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleApp.Common;
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using Xunit;

namespace VehicleApp.Repository.Tests
{
    public class VehicleModelRepositoryTest
    {
        private readonly VehicleModelRepository _sut;
        private readonly Mock<IRepository<VehicleModelEntity>> _repositoryMock = new Mock<IRepository<VehicleModelEntity>>();
        private readonly IMapper _mapper;

        private readonly Mock<IModelFilter> _filterMock = new Mock<IModelFilter>();
        private readonly Mock<IPagination<IVehicleModel>> _paginationMock = new Mock<IPagination<IVehicleModel>>();
        private readonly Mock<ISorter<IVehicleModel>> _sorterMock = new Mock<ISorter<IVehicleModel>>();

        public VehicleModelRepositoryTest()
        {
            _mapper = SetupAutomapper();
            _sut = new VehicleModelRepository(_repositoryMock.Object, _mapper);
        }

        public IMapper SetupAutomapper()
        {
            var config = new MapperConfiguration(
                cfg => cfg.AddMaps(new[] {
                    typeof(VehicleApp.Repository.AutoMapperConfiguration.DomainToEntityModelMapping)
                }));

            var mapper = config.CreateMapper();
            return mapper;
        }

        [Fact]
        public async Task Add_ShouldAddVehicleModel()
        {
            //Arrange

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa "
            };

            var vehicleModel = _mapper.Map<IVehicleModel>(vehicleModelEntity);

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<VehicleModelEntity>())).ReturnsAsync(1);

            //Act

            var result = await _sut.Add(vehicleModel);

            //Assert

            result.Should().Be(1);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<VehicleModelEntity>()), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddVehicleModel()
        {
            //Arrange

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity()
            {
                VehicleModelId = Guid.NewGuid(),
                VehicleMakeId = Guid.Empty,
                Name = "Corsa "
            };

            var vehicleModel = _mapper.Map<IVehicleModel>(vehicleModelEntity);

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<VehicleModelEntity>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Add(vehicleModel);

            //Assert

            result.Should().Be(0);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<VehicleModelEntity>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                VehicleModelId = generatedGuid,
                Name = "Corsa"
            };

            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleModelEntity);
            _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(1);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteVehicleModelBecauseNotFoundInDatabase()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                VehicleModelId = generatedGuid,
                Name = "Corsa"
            };

            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((VehicleModelEntity)null);
            _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Delete(generatedGuid);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdateVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity()
            {
                VehicleModelId = generatedGuid,
                VehicleMakeId  = Guid.NewGuid(),
                Name = "Corsa edited"
            };

            var vehicleModel = _mapper.Map<IVehicleModel>(vehicleModelEntity);

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleModelEntity>())).ReturnsAsync(1);

            //Act

            var result = await _sut.Update(generatedGuid, vehicleModel);

            //Assert

            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleModelEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateVehicleModelBecauseNotFoundInDatabase()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity()
            {
                VehicleModelId = generatedGuid,
                VehicleMakeId = Guid.NewGuid(),
                Name = "Corsa edited"
            };

            var vehicleModel = _mapper.Map<IVehicleModel>(vehicleModelEntity);

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleModelEntity>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Update(generatedGuid, vehicleModel);

            //Assert

            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleModelEntity>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ReturnFilteredByMakePaginatedSortedByNameOrderedByAscending()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            List<VehicleModelEntity> vehicleModelFromDB = new List<VehicleModelEntity>()
            {
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = generatedGuid, Name = "Corsa"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "X3"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = generatedGuid, Name = "Astra"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Auris"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Corvette"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = generatedGuid, Name = "Zafira"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = generatedGuid, Name = "Insignia"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "C3"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = Guid.NewGuid(), Name = "Golf VI"},
                new VehicleModelEntity() { VehicleModelId = Guid.NewGuid(), VehicleMakeId = generatedGuid, Name = "Meriva"}
            };

            IEnumerable<IVehicleModel> vehicleModels = _mapper.Map<IEnumerable<IVehicleModel>>(vehicleModelFromDB);

            _filterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();

            _filterMock.Object.VehicleMakeID = generatedGuid;
            _paginationMock.Object.PageNumber = 1;
            _paginationMock.Object.PageSize = 10;
            _sorterMock.Object.sortBy = "name";
            _sorterMock.Object.sortDirection = "asc";

            //Filter query
            Expression<Func<IVehicleModel, bool>> filterQuery = x => x.VehicleMakeId == _filterMock.Object.VehicleMakeID;

            //Sort query by name
            Expression<Func<IVehicleModel, dynamic>> sortQuery = x => x.Name;

            //Applying filter query to data from DB
            vehicleModels = vehicleModels.AsQueryable().Where(filterQuery).ToList();

            //Applying sorting query to data from DB
            switch (_sorterMock.Object.sortDirection)
            {
                case "asc":
                    vehicleModels = vehicleModels.AsQueryable().OrderBy(sortQuery).ToList();
                    break;
                case "desc":
                    vehicleModels = vehicleModels.AsQueryable().OrderByDescending(sortQuery).ToList();
                    break;
                default:
                    break;
            }

            _filterMock.Setup(X => X.GetFilterQuery()).Returns(filterQuery);

            _repositoryMock.Setup(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleModelEntity, bool>>>())).ReturnsAsync(_mapper.Map<IEnumerable<VehicleModelEntity>>(vehicleModels));

            _sorterMock.Setup(x => x.GetSortQuery()).Returns(sortQuery);
            _sorterMock.Setup(x => x.SortData(It.IsAny<ICollection<IVehicleModel>>(), It.IsAny<Expression<Func<IVehicleModel, dynamic>>>())).Returns(vehicleModels.ToList());

            _paginationMock.Setup(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleModel>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(vehicleModels.ToList());

            //Act

            var response = await _sut.FindAsync(_filterMock.Object, _paginationMock.Object, _sorterMock.Object);

            //Assert

            response.Should().NotBeNull();
            response.Data.Should().BeInAscendingOrder(sortQuery);
            response.Data.Should().HaveCount(5);
            response.PageNumber.Should().Be(1);
            response.PageSize.Should().Be(10);
            _filterMock.Verify(X => X.GetFilterQuery(), Times.Once);
            _repositoryMock.Verify(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleModelEntity, bool>>>()), Times.Once);
            _sorterMock.Verify(X => X.GetSortQuery(), Times.Once);
            _sorterMock.Verify(x => x.SortData(It.IsAny<ICollection<IVehicleModel>>(), It.IsAny<Expression<Func<IVehicleModel, dynamic>>>()), Times.Once);
            _paginationMock.Verify(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleModel>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}