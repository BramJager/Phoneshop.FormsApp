using Xunit;
using Moq;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;

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
            brandRepository.Setup(x => x.Get()).Returns(list).Callback(list = new List<Brand>() { new Brand() { Name = "test" } });

            //Act
            var brand = brandService.GetOrCreate(name);

            //Assert
            brandRepository.Verify(x => x.Get(), Times.Once);
            brandRepository.Verify(x => x.Create(It.IsAny<Brand>()), Times.Once);
            Assert.Equal(name, brand.Name);
        }

        [Fact]
        public void Should_GetBrand_When_BrandDoesExist()
        {


        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowArgumentNullException_When_NameIsNullOrEmpty(string name)
        {

        }
    }
}
