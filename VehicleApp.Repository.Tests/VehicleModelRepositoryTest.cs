using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.Common.Filters;
using VehicleApp.DAL;
using VehicleApp.Model.Common;
using VehicleApp.Repository.Common;
using Xunit;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VehicleApp.Repository.Tests.Utility;

namespace VehicleApp.Repository.Tests
{
    public class VehicleModelRepositoryTests
    {
        private readonly VehicleModelRepository VehicleModelRepository;
        private readonly Mock<IVehicleContext> DatabaseContextMock = new Mock<IVehicleContext>();
        private readonly Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper Mapper;
        private readonly Mock<IModelFilter> FilterMock = new Mock<IModelFilter>();
        private readonly Mock<IPagination> PaginationMock = new Mock<IPagination>();
        private readonly Mock<ISorter> SorterMock = new Mock<ISorter>();

        public VehicleModelRepositoryTests()
        {
            Mapper = SetupAutomapper();
            VehicleModelRepository = new VehicleModelRepository(Mapper, DatabaseContextMock.Object, UnitOfWorkMock.Object);
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
            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                Id = Guid.NewGuid(),
                VehicleMakeId = Guid.NewGuid(),
                Name = "Focus"
            };

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.AddUoWAsync(It.IsAny<VehicleModelEntity>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            var result = await VehicleModelRepository.AddAsync(vehicleModel);

            //Assert
            result.Should().Be(1);
            UnitOfWorkMock.Verify(x => x.AddUoWAsync(It.IsAny<VehicleModelEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);

        }

        [Fact]
        public async Task Add_ShouldNotAddBecauseVehicleModelIsNull()
        {
            //Arrange
            VehicleModelEntity vehicleModelEntity = null;

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.AddUoWAsync(It.IsAny<VehicleModelEntity>()));
            UnitOfWorkMock.Setup(x => x.CommitAsync());

            //Act

            int result = await VehicleModelRepository.AddAsync(vehicleModel);

            //Assert
            result.Should().Be(0);
            UnitOfWorkMock.Verify(x => x.AddUoWAsync(It.IsAny<VehicleModelEntity>()), Times.Never);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                Id = generatedGuid,
                VehicleMakeId = Guid.NewGuid(),
                Name = "Focus"
            };

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.DeleteUoWAsync<VehicleModelEntity>(It.IsAny<Guid>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            int result = await VehicleModelRepository.DeleteAsync(generatedGuid);

            //Assert
            result.Should().Be(1);
            UnitOfWorkMock.Verify(x => x.DeleteUoWAsync<VehicleModelEntity>(It.IsAny<Guid>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteVehicleModelBecauseEmptyGuid()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                Id = generatedGuid,
                VehicleMakeId = Guid.NewGuid(),
                Name = "Focus"
            };

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.DeleteUoWAsync<VehicleModelEntity>(It.IsAny<Guid>())).ReturnsAsync(0);
            UnitOfWorkMock.Setup(x => x.CommitAsync());

            //Act

            int result = await VehicleModelRepository.DeleteAsync(generatedGuid);

            //Assert
            result.Should().Be(0);
            UnitOfWorkMock.Verify(x => x.DeleteUoWAsync<VehicleModelEntity>(It.IsAny<Guid>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldUpdateVehicleModel()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                Id = generatedGuid,
                VehicleMakeId = Guid.NewGuid(),
                Name = "Focus"
            };

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);

            DatabaseContextMock.Setup(x => x.Set<VehicleModelEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.UpdateUoWAsync(It.IsAny<VehicleModelEntity>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await VehicleModelRepository.UpdateAsync(generatedGuid, vehicleModel);

            //Assert
            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            UnitOfWorkMock.Verify(x => x.UpdateUoWAsync(It.IsAny<VehicleModelEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateVehicleModelBecauseNotExistingInDbContext()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleModelEntity vehicleModelEntity = new VehicleModelEntity
            {
                Id = generatedGuid,
                VehicleMakeId = Guid.NewGuid(),
                Name = "Focus"
            };

            var vehicleModel = Mapper.Map<IVehicleModel>(vehicleModelEntity);

            DatabaseContextMock.Setup(x => x.Set<VehicleModelEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleModelEntity);
            UnitOfWorkMock.Setup(x => x.UpdateUoWAsync(It.IsAny<VehicleModelEntity>())).ReturnsAsync(0);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await VehicleModelRepository.UpdateAsync(generatedGuid, vehicleModel);

            //Assert
            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            UnitOfWorkMock.Verify(x => x.UpdateUoWAsync(It.IsAny<VehicleModelEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task FindAsync_ReturnFilteredByVehicleMakeIdPaginatedSortedByNameOrderedByAscending()
        {
            //Arrange

            var bmwGuid = Guid.NewGuid();

            List<VehicleModelEntity> vehicleModelsFromDB = new List<VehicleModelEntity>()
            {
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "E 520", VehicleMakeId = bmwGuid},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "M3 GTR", VehicleMakeId = bmwGuid},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "Corsa", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "Felicia", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "Celica", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "A4", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "E 220", VehicleMakeId = bmwGuid},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "i30", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "CX-5", VehicleMakeId = Guid.NewGuid()},
                        new VehicleModelEntity { Id = Guid.NewGuid(), Name = "159", VehicleMakeId = Guid.NewGuid()}
            };

            var queryList = vehicleModelsFromDB.AsQueryable();

            FilterMock.SetupAllProperties();
            PaginationMock.SetupAllProperties();
            SorterMock.SetupAllProperties();

            FilterMock.Object.Search = "E";
            FilterMock.Object.VehicleMakeId = bmwGuid;
            PaginationMock.Object.PageNumber = 1;
            PaginationMock.Object.RecordsPerPage = 10;
            SorterMock.Object.SortBy = "name";
            SorterMock.Object.SortDirection = "asc";

            queryList = queryList.Where(x => x.VehicleMakeId == FilterMock.Object.VehicleMakeId);
            queryList = queryList.OrderBy(x => x.Name);

            Mock<DbSet<VehicleModelEntity>> DbSetMock = new Mock<DbSet<VehicleModelEntity>>();
            DbSetMock.As<IDbAsyncEnumerable<VehicleModelEntity>>()
                .Setup(x => x.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<VehicleModelEntity>(queryList.GetEnumerator()));

            DbSetMock.As<IQueryable<VehicleModelEntity>>()
                .Setup(x => x.Provider)
                .Returns(new TestDbAsyncQueryProvider<VehicleModelEntity>(queryList.Provider));

            DbSetMock.As<IQueryable<VehicleModelEntity>>().Setup(x => x.Expression).Returns(queryList.Expression);
            DbSetMock.As<IQueryable<VehicleModelEntity>>().Setup(x => x.ElementType).Returns(queryList.ElementType);
            DbSetMock.As<IQueryable<VehicleModelEntity>>().Setup(x => x.GetEnumerator()).Returns(queryList.GetEnumerator());

            DatabaseContextMock.Setup(x => x.Set<VehicleModelEntity>()).Returns(DbSetMock.Object);

            SorterMock.Setup(x => x.GetSortedData(It.IsAny<IEnumerable<VehicleModelEntity>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(queryList);

            PaginationMock.Setup(x => x.GetPaginatedData(It.IsAny<IEnumerable<VehicleModelEntity>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(queryList);

            //Act
            var response = await VehicleModelRepository.FindAsync(FilterMock.Object, SorterMock.Object, PaginationMock.Object);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeInAscendingOrder(x => x.Name);
            response.Data.Should().HaveCount(3);
            response.PageNumber.Should().Be(1);
            response.PageSize.Should().Be(10);
            DatabaseContextMock.Verify(x => x.Set<VehicleModelEntity>(), Times.Once);
            SorterMock.Verify(x => x.GetSortedData(It.IsAny<IEnumerable<VehicleModelEntity>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            PaginationMock.Verify(x => x.GetPaginatedData(It.IsAny<IEnumerable<VehicleModelEntity>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}