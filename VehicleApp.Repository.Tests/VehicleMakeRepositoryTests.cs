using Autofac.Extras.Moq;
using AutoMapper;
using AutoMapper.Configuration;
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
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.AutoMapperConfiguration;
using VehicleApp.Repository.Common;
using Xunit;
using Moq.Protected;

namespace VehicleApp.Repository.Tests
{
    public class VehicleMakeRepositoryTests
    {
        private readonly VehicleMakeRepository VehicleMakeRepository;
        private readonly Mock<IVehicleContext> DatabaseContext = new Mock<IVehicleContext>();
        private readonly Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper Mapper;
        private readonly Mock<IMakeFilter> FilterMock = new Mock<IMakeFilter>();
        private readonly Mock<IPagination> PaginationMock = new Mock<IPagination>();
        private readonly Mock<ISorter> SorterMock = new Mock<ISorter>();

        public VehicleMakeRepositoryTests()
        {
            Mapper = SetupAutomapper();
            VehicleMakeRepository = new VehicleMakeRepository(Mapper, DatabaseContext.Object, UnitOfWorkMock.Object);
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

            DatabaseContext.Setup(x => x.Set<VehicleMakeEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
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

            DatabaseContext.Setup(x => x.Set<VehicleMakeEntity>().FindAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
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

        //        [Fact]
        //        public async Task FindAsync_ReturnFilteredByNamePaginatedSortedByNameOrderedByAscending()
        //        {
        //            //Arrange

        //            List<VehicleMakeEntity> vehicleMakesFromDB = new List<VehicleMakeEntity>()
        //            {
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Opel"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Audi"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "BMW"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Toyota"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Ford"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Suzuki"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Nissan"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Hyundai"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "Mazda"},
        //                new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "VolksWagen"}
        //            };

        //            IEnumerable<IVehicleMake> vehicleMakes = _mapper.Map<IEnumerable<IVehicleMake>>(vehicleMakesFromDB);

        //            _filterMock.SetupAllProperties();
        //            _paginationMock.SetupAllProperties();
        //            _sorterMock.SetupAllProperties();

        //            _filterMock.Object.Search = "A";
        //            _paginationMock.Object.PageNumber = 1;
        //            _paginationMock.Object.PageSize = 10;
        //            _sorterMock.Object.sortBy = "name";
        //            _sorterMock.Object.sortDirection = "asc";

        //            //Filter query
        //            Expression<Func<IVehicleMake, bool>> filterQuery = x => x.Name.Contains(_filterMock.Object.Search.ToLower());

        //            //Sort query by name
        //            Expression<Func<IVehicleMake, dynamic>> sortQuery = x => x.Name;

        //            //Applying filter query to data from DB
        //            vehicleMakes = vehicleMakes.AsQueryable().Where(filterQuery).ToList();

        //            //Applying sorting query to data from DB
        //            switch (_sorterMock.Object.sortDirection)
        //            {
        //                case "asc":
        //                    vehicleMakes = vehicleMakes.AsQueryable().OrderBy(sortQuery).ToList();
        //                    break;
        //                case "desc":
        //                    vehicleMakes = vehicleMakes.AsQueryable().OrderByDescending(sortQuery).ToList();
        //                    break;
        //                default:
        //                    break;
        //            }

        //            _filterMock.Setup(X => X.GetFilterQuery()).Returns(filterQuery);

        //            _repositoryMock.Setup(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleMakeEntity, bool>>>())).ReturnsAsync(_mapper.Map<IEnumerable<VehicleMakeEntity>>(vehicleMakes));

        //            _sorterMock.Setup(x => x.GetSortQuery()).Returns(sortQuery);
        //            _sorterMock.Setup(x => x.SortData(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<Expression<Func<IVehicleMake, dynamic>>>())).Returns(vehicleMakes.ToList());

        //            _paginationMock.Setup(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(vehicleMakes.ToList());

        //            //Act
        //            var response = await _sut.FindAsync(_filterMock.Object, _paginationMock.Object, _sorterMock.Object);

        //            //Assert
        //            response.Should().NotBeNull();
        //            response.Data.Should().BeInAscendingOrder(sortQuery);
        //            response.Data.Should().HaveCount(5);
        //            response.PageNumber.Should().Be(1);
        //            response.PageSize.Should().Be(10);
        //            _filterMock.Verify(X => X.GetFilterQuery(), Times.Once);
        //            _repositoryMock.Verify(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleMakeEntity, bool>>>()), Times.Once);
        //            _sorterMock.Verify(X => X.GetSortQuery(), Times.Once);
        //            _sorterMock.Verify(x => x.SortData(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<Expression<Func<IVehicleMake, dynamic>>>()), Times.Once);
        //            _paginationMock.Verify(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        //        }
    }
}
