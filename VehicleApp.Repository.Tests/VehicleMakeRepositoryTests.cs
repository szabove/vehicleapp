using Autofac.Extras.Moq;
using AutoMapper;
using AutoMapper.Configuration;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Mock<IVehicleMakeRepository> _sut;
        private readonly Mock<IRepository<VehicleMakeEntity>> _repositoryMock = new Mock<IRepository<VehicleMakeEntity>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public VehicleMakeRepositoryTests()
        {
            _sut = new Mock<IVehicleMakeRepository>();
        }

        [Fact]
        public async Task Add_ShouldAddUser()
        {
            //Arrange

            var mapper = SetupAutomapper();

            VehicleMakeEntity makeEntity = new VehicleMakeEntity
            {
                VehicleMakeId = Guid.NewGuid(),
                Name = "Ford"
            };

            var testEntity = mapper.Map<IVehicleMake>(makeEntity);

            _repositoryMock.Setup(x => x.AddAsync(makeEntity)).ReturnsAsync(1);
            _sut.Setup(x => x.Add(testEntity)).ReturnsAsync(1);

            //Act

            var result = await _sut.Object.Add(testEntity);
            //Assert
            

            result.Should().Be(1);
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
    }
}
