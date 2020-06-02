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
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Repository.AutoMapperConfiguration;
using VehicleApp.Repository.Common;
using Xunit;

namespace VehicleApp.Repository.Tests
{
    public class VehicleMakeRepositoryTests
    {
        private readonly VehicleMakeRepository _sut;
        private readonly Mock<IRepository<VehicleMakeEntity>> _repositoryMock = new Mock<IRepository<VehicleMakeEntity>>();
        private readonly IMapper _mapper;

        private readonly Mock<IMakeFilter> _filterMock = new Mock<IMakeFilter>();
        private readonly Mock<IPagination<IVehicleMake>> _paginationMock = new Mock<IPagination<IVehicleMake>>();
        private readonly Mock<ISorter<IVehicleMake>> _sorterMock = new Mock<ISorter<IVehicleMake>>();

        public VehicleMakeRepositoryTests()
        {
            _mapper = SetupAutomapper();
            _sut = new VehicleMakeRepository(_repositoryMock.Object, _mapper);
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
                VehicleMakeId = Guid.NewGuid(),
                Name = "Ford"
            };

            var vehicleMake = _mapper.Map<IVehicleMake>(vehicleMakeEntity);

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<VehicleMakeEntity>())).ReturnsAsync(1);

            //Act

            var result = await _sut.Add(vehicleMake);

            //Assert
            result.Should().Be(1);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<VehicleMakeEntity>()), Times.Once);
        }

        [Fact]
        public async Task Add_ShouldNotAddVehicleMakeBecauseEmptyGuid()
        {
            //Arrange

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = Guid.Empty,
                Name = "Ford"
            };

            var vehicleMake = _mapper.Map<IVehicleMake>(vehicleMakeEntity);

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<VehicleMakeEntity>())).ReturnsAsync(0);

            //Act

            var result = await _sut.Add(vehicleMake);

            //Assert
            result.Should().Be(0);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<VehicleMakeEntity>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldDeleteVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = generatedGuid,
                Name = "Ford"
            };

            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
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
        public async Task Delete_ShouldNotDeleteVehicleMakeBecauseEmptyGuid()
        {
            //Arrange

            var generatedGuid = Guid.Empty;

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = generatedGuid,
                Name = "Ford"
            };

            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(vehicleMakeEntity);
            _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(0);

            //Act
            var result = await _sut.Delete(generatedGuid);

            //Assert
            result.Should().Be(0);
            generatedGuid.Should().Be(Guid.Empty);
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldUpdateVehicleMake()
        {
            //Arrange

            var generatedGuid = Guid.NewGuid();

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = _mapper.Map<IVehicleMake>(vehicleMakeEntity);

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleMakeEntity>())).ReturnsAsync(1);
            
            //Act
            var result = await _sut.Update(generatedGuid, vehicleMake);

            //Assert
            result.Should().Be(1);
            generatedGuid.Should().NotBeEmpty();
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleMakeEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateVehicleMakeBecauseEmptyGuid()
        {
            //Arrange

            var generatedGuid = Guid.Empty;

            VehicleMakeEntity vehicleMakeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = generatedGuid,
                Name = "Ford"
            };

            var vehicleMake = _mapper.Map<IVehicleMake>(vehicleMakeEntity);

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleMakeEntity>())).ReturnsAsync(0);

            //Act
            var result = await _sut.Update(generatedGuid, vehicleMake);

            //Assert
            result.Should().Be(0);
            generatedGuid.Should().Be(Guid.Empty);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<VehicleMakeEntity>()), Times.Never);
        }

        [Fact]
        public async Task FindAsync_ReturnFilteredByNamePaginatedSortedByNameOrderedByAscending()
        {
            //Arrange

            List<VehicleMakeEntity> vehicleMakesFromDB = new List<VehicleMakeEntity>()
            {
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Opel"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Audi"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "BMW"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Toyota"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Ford"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Suzuki"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Nissan"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Hyundai"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "Mazda"},
                new VehicleMakeEntity() { VehicleMakeId = Guid.NewGuid(), Name = "VolksWagen"}
            };

            IEnumerable<IVehicleMake> vehicleMakes = _mapper.Map<IEnumerable<IVehicleMake>>(vehicleMakesFromDB);

            _filterMock.SetupAllProperties();
            _paginationMock.SetupAllProperties();
            _sorterMock.SetupAllProperties();

            _filterMock.Object.Search = "A";
            _paginationMock.Object.PageNumber = 1;
            _paginationMock.Object.PageSize = 10;
            _sorterMock.Object.sortBy = "name";
            _sorterMock.Object.sortDirection = "asc";
            
            //Filter query
            Expression<Func<IVehicleMake, bool>> filterQuery = x => x.Name.Contains(_filterMock.Object.Search.ToLower());

            //Sort query by name
            Expression<Func<IVehicleMake, dynamic>> sortQuery = x => x.Name;

            //Applying filter query to data from DB
            vehicleMakes = vehicleMakes.AsQueryable().Where(filterQuery).ToList();

            //Applying sorting query to data from DB
            switch (_sorterMock.Object.sortDirection)
            {
                case "asc":
                    vehicleMakes = vehicleMakes.AsQueryable().OrderBy(sortQuery).ToList();
                    break;
                case "desc":
                    vehicleMakes = vehicleMakes.AsQueryable().OrderByDescending(sortQuery).ToList();
                    break;
                default:
                    break;
            }
            
            _filterMock.Setup(X => X.GetFilterQuery()).Returns(filterQuery);
            
            _repositoryMock.Setup(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleMakeEntity, bool>>>())).ReturnsAsync(_mapper.Map<IEnumerable<VehicleMakeEntity>>(vehicleMakes));

            _sorterMock.Setup(x => x.GetSortQuery()).Returns(sortQuery);
            _sorterMock.Setup(x => x.SortData(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<Expression<Func<IVehicleMake, dynamic>>>())).Returns(vehicleMakes.ToList());

            _paginationMock.Setup(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(vehicleMakes.ToList());
            
            //Act
            var response = await _sut.FindAsync(_filterMock.Object, _paginationMock.Object, _sorterMock.Object);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeInAscendingOrder(sortQuery);
            response.Data.Should().HaveCount(5);
            response.PageNumber.Should().Be(1);
            response.PageSize.Should().Be(10);
            _filterMock.Verify(X => X.GetFilterQuery(), Times.Once);
            _repositoryMock.Verify(x => x.WhereQueryAsync(It.IsAny<Expression<Func<VehicleMakeEntity, bool>>>()), Times.Once);
            _sorterMock.Verify(X => X.GetSortQuery(), Times.Once);
            _sorterMock.Verify(x => x.SortData(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<Expression<Func<IVehicleMake, dynamic>>>()), Times.Once);
            _paginationMock.Verify(x => x.PaginatedResult(It.IsAny<ICollection<IVehicleMake>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
