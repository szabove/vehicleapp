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
    public class VehicleMakeRepositoryTests
    {
        private readonly VehicleMakeRepository VehicleMakeRepository;
        private readonly Mock<IVehicleContext> DatabaseContextMock = new Mock<IVehicleContext>();
        private readonly Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper Mapper;
        private readonly Mock<IMakeFilter> FilterMock = new Mock<IMakeFilter>();
        private readonly Mock<IPagination> PaginationMock = new Mock<IPagination>();
        private readonly Mock<ISorter> SorterMock = new Mock<ISorter>();

        public VehicleMakeRepositoryTests()
        {
            Mapper = SetupAutomapper();
            VehicleMakeRepository = new VehicleMakeRepository(Mapper, DatabaseContextMock.Object, UnitOfWorkMock.Object);
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
        public async Task Add_ShouldAddVehicleMake()
        {
            //Arrange
            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Ford"
            };

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x => x.AddUoWAsync(It.IsAny<VehicleMakeEntity>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            var result = await VehicleMakeRepository.AddAsync(vehicleMake);

            //Assert
            result.Should().Be(1);
            UnitOfWorkMock.Verify(x => x.AddUoWAsync(It.IsAny<VehicleMakeEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);

        }

        [Fact]
        public async Task Add_ShouldNotAddBecauseVehicleMakeIsNull()
        {
            //Arrange
            VehicleMakeEntity vehicleMakeEntity = null;

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x => x.AddUoWAsync(It.IsAny<VehicleMakeEntity>()));
            UnitOfWorkMock.Setup(x => x.CommitAsync());

            //Act

            int result = await VehicleMakeRepository.AddAsync(vehicleMake);

            //Assert
            result.Should().Be(0);
            UnitOfWorkMock.Verify(x => x.AddUoWAsync(It.IsAny<VehicleMakeEntity>()), Times.Never);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                Id = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x => x.DeleteUoWAsync<VehicleMakeEntity>(It.IsAny<Guid>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act

            int result = await VehicleMakeRepository.DeleteAsync(generatedGuid);

            //Assert
            result.Should().Be(1);
            UnitOfWorkMock.Verify(x => x.DeleteUoWAsync<VehicleMakeEntity>(It.IsAny<Guid>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteVehicleMakeBecauseEmptyGuid()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                Id = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x => x.DeleteUoWAsync<VehicleMakeEntity>(It.IsAny<Guid>())).ReturnsAsync(0);
            UnitOfWorkMock.Setup(x => x.CommitAsync());

            //Act

            int result = await VehicleMakeRepository.DeleteAsync(generatedGuid);

            //Assert
            result.Should().Be(0);
            UnitOfWorkMock.Verify(x => x.DeleteUoWAsync<VehicleMakeEntity>(It.IsAny<Guid>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldUpdateVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                Id = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);

            DatabaseContextMock.Setup(x => x.Set<VehicleMakeEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x=>x.UpdateUoWAsync(It.IsAny<VehicleMakeEntity>())).ReturnsAsync(1);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await VehicleMakeRepository.UpdateAsync(generatedGuid, vehicleMake);

            //Assert
            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            UnitOfWorkMock.Verify(x => x.UpdateUoWAsync(It.IsAny<VehicleMakeEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateVehicleMakeBecauseNotExistingInDbContext()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                Id = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = Mapper.Map<IVehicleMake>(vehicleMakeEntity);

            DatabaseContextMock.Setup(x => x.Set<VehicleMakeEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
            UnitOfWorkMock.Setup(x => x.UpdateUoWAsync(It.IsAny<VehicleMakeEntity>())).ReturnsAsync(0);
            UnitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await VehicleMakeRepository.UpdateAsync(generatedGuid, vehicleMake);

            //Assert
            result.Should().Be(0);
            generatedGuid.Should().NotBeEmpty();
            UnitOfWorkMock.Verify(x => x.UpdateUoWAsync(It.IsAny<VehicleMakeEntity>()), Times.Once);
            UnitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task FindAsync_ReturnFilteredByNamePaginatedSortedByNameOrderedByAscending()
        {
            //Arrange
            
            List<VehicleMakeEntity> vehicleMakesFromDB = new List<VehicleMakeEntity>()
            {
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Opel"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Audi"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "BMW"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Toyota"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Ford"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Suzuki"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Nissan"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Hyundai"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Mazda"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "VolksWagen"}
            };

            var queryList = vehicleMakesFromDB.AsQueryable();
            queryList = queryList.Where(x => x.Name.Contains(FilterMock.Object.Search.ToLower()));
            queryList = queryList.OrderBy(x => x.Name);

            Mock<DbSet<VehicleMakeEntity>> DbSetMock = new Mock<DbSet<VehicleMakeEntity>>();
            DbSetMock.As<IDbAsyncEnumerable<VehicleMakeEntity>>()
                .Setup(x => x.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<VehicleMakeEntity>(queryList.GetEnumerator()));

            DbSetMock.As<IQueryable<VehicleMakeEntity>>()
                .Setup(x => x.Provider)
                .Returns(new TestDbAsyncQueryProvider<VehicleMakeEntity>(queryList.Provider));

            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.Expression).Returns(queryList.Expression);
            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.ElementType).Returns(queryList.ElementType);
            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.GetEnumerator()).Returns(queryList.GetEnumerator());

            DatabaseContextMock.Setup(x => x.Set<VehicleMakeEntity>()).Returns(DbSetMock.Object);
                        
            SorterMock.Setup(x => x.GetSortedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(queryList);

            PaginationMock.Setup(x => x.GetPaginatedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(queryList);

            FilterMock.SetupAllProperties();
            PaginationMock.SetupAllProperties();
            SorterMock.SetupAllProperties();

            FilterMock.Object.Search = "A";
            PaginationMock.Object.PageNumber = 1;
            PaginationMock.Object.RecordsPerPage = 10;
            SorterMock.Object.SortBy= "name";
            SorterMock.Object.SortDirection = "asc";


            //Act
            var response = await VehicleMakeRepository.FindAsync(FilterMock.Object, SorterMock.Object, PaginationMock.Object);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeInAscendingOrder(x=>x.Name);
            response.Data.Should().HaveCount(5);
            response.PageNumber.Should().Be(1);
            response.PageSize.Should().Be(10);
            DatabaseContextMock.Verify(x => x.Set<VehicleMakeEntity>(), Times.Once);
            SorterMock.Verify(x=>x.GetSortedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            PaginationMock.Verify(x=>x.GetPaginatedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ReturnFilteredByNamePaginatedSortedByNameOrderedByDescending()
        {
            //Arrange

            List<VehicleMakeEntity> vehicleMakesFromDB = new List<VehicleMakeEntity>()
            {
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Opel"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Audi"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "BMW"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Toyota"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Ford"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Suzuki"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Nissan"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Hyundai"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "Mazda"},
                        new VehicleMakeEntity { Id = Guid.NewGuid(), Name = "VolksWagen"}
            };

            var queryList = vehicleMakesFromDB.AsQueryable();
            queryList = queryList.Where(x => x.Name.Contains(FilterMock.Object.Search.ToLower()));
            queryList = queryList.OrderByDescending(x => x.Name);

            Mock<DbSet<VehicleMakeEntity>> DbSetMock = new Mock<DbSet<VehicleMakeEntity>>();
            DbSetMock.As<IDbAsyncEnumerable<VehicleMakeEntity>>()
                .Setup(x => x.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<VehicleMakeEntity>(queryList.GetEnumerator()));

            DbSetMock.As<IQueryable<VehicleMakeEntity>>()
                .Setup(x => x.Provider)
                .Returns(new TestDbAsyncQueryProvider<VehicleMakeEntity>(queryList.Provider));

            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.Expression).Returns(queryList.Expression);
            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.ElementType).Returns(queryList.ElementType);
            DbSetMock.As<IQueryable<VehicleMakeEntity>>().Setup(x => x.GetEnumerator()).Returns(queryList.GetEnumerator());

            DatabaseContextMock.Setup(x => x.Set<VehicleMakeEntity>()).Returns(DbSetMock.Object);

            SorterMock.Setup(x => x.GetSortedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(queryList);

            PaginationMock.Setup(x => x.GetPaginatedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(queryList);

            FilterMock.SetupAllProperties();
            PaginationMock.SetupAllProperties();
            SorterMock.SetupAllProperties();

            FilterMock.Object.Search = "A";
            PaginationMock.Object.PageNumber = 1;
            PaginationMock.Object.RecordsPerPage = 10;
            SorterMock.Object.SortBy = "name";
            SorterMock.Object.SortDirection = "asc";


            //Act
            var response = await VehicleMakeRepository.FindAsync(FilterMock.Object, SorterMock.Object, PaginationMock.Object);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeInDescendingOrder(x => x.Name);
            response.Data.Should().HaveCount(5);
            response.PageNumber.Should().Be(1);
            response.PageSize.Should().Be(10);
            DatabaseContextMock.Verify(x => x.Set<VehicleMakeEntity>(), Times.Once);
            SorterMock.Verify(x => x.GetSortedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            PaginationMock.Verify(x => x.GetPaginatedData(It.IsAny<IEnumerable<VehicleMakeEntity>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
