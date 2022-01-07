using Xunit;
using Moq;
using Phoneshop.Business;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System.Data.SqlClient;
using System;

namespace PhoneShop.Xunit.BrandTests
{
    public class BrandCreateTests
    {
        private readonly Mock<IRepository<Brand>> repository;
        private readonly IBrandService brandService;

        public BrandCreateTests()
        {
            repository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(repository.Object);
        }

        [Fact]
        public void Should_CreateBrand()
        {
            //Arrange
            var brand = new Brand() { Id = 1, Name = "Test" };

            //Act
            brandService.Create(brand);

            //Assert
            repository.Verify(r => r.ExecuteNonQuery(It.IsAny<SqlCommand>()), Times.Once);
        }

        [Fact]
        public void Should_GetBrand_When_BrandWithNameExists()
        {
            //Arrange
            var brandName = "test";

            repository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns(new Brand() { Id = 1, Name = "test" });

            //Act
            Brand brand = brandService.GetOrCreate(brandName);

            //Assert
            Assert.Equal(brandName, brand.Name);
            repository.Verify(r => r.GetRecord(It.IsAny<SqlCommand>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowArgumentNullException_When_NameIsEmptyOrNull(string name)
        {
            //Arrange

            //Act
            Action act = () => brandService.GetOrCreate(name);

            //Assert
            Exception exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Should_CreateNewBrand_When_BrandNameNotExists()
        {
            //Arrange
            var brandName = "test";

            Brand testBrand = null;

            repository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>()))
                .Returns(() => testBrand)
                .Callback(() => testBrand = new Brand() { Id = 1, Name = "test"});

            //Act
            Brand brand = brandService.GetOrCreate(brandName);

            //Assert
            repository.Verify(r => r.GetRecord(It.IsAny<SqlCommand>()), Times.Exactly(2));
            repository.Verify(r => r.ExecuteNonQuery(It.IsAny<SqlCommand>()), Times.Once);
            Assert.Equal(brandName, brand.Name);
        }

    }
}
