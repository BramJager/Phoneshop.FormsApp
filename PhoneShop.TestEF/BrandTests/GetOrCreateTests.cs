using Xunit;
using Moq;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System;

namespace PhoneShop.TestEF.BrandTests
{
    public class GetOrCreateTests : BaseTest
    {
        public GetOrCreateTests() : base() { }

        [Fact]
        public void Should_CreateBrand_When_BrandDoesNotExist()
        {
            //Arrange
            var name = "test";
            var list = new List<Brand>();
            brandRepository.Setup(x => x.Get()).Returns(() => list)
                                               .Callback(() => list = new List<Brand>() { new Brand() { Name = name } });

            //Act
            var brand = brandService.GetOrCreate(name);

            //Assert
            brandRepository.Verify(x => x.Get(), Times.Exactly(2));
            brandRepository.Verify(x => x.Create(It.IsAny<Brand>()), Times.Once);
            Assert.Equal(name, brand.Name);
        }

        [Fact]
        public void Should_GetBrand_When_BrandDoesExist()
        {
            //Arrange
            var name = "test";
            var list = new List<Brand>();
            brandRepository.Setup(x => x.Get()).Returns(() => list = new List<Brand>() { new Brand() { Name = name } });

            //Act
            var brand = brandService.GetOrCreate(name);

            //Assert
            brandRepository.Verify(x => x.Get(), Times.Once);
            brandRepository.Verify(x => x.Create(It.IsAny<Brand>()), Times.Never);
            Assert.Equal(name, brand.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowArgumentNullException_When_NameIsNullOrEmpty(string name)
        {
            //Arrange


            //Act
            Action act = () => brandService.GetOrCreate(name);

            //Assert
            Exception ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'name')", ex.Message);
        }
    }
}
