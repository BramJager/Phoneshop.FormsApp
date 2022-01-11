using Phoneshop.Domain.Entities;
using Xunit;

namespace PhoneShop.Xunit.PhoneTests
{
    public class PhoneEntityTest
    {
        [Fact]
        public void Should_SetFullName()
        {
            //Arrange
            var brand = new Brand() { Id = 1, Name = "test" };
            var phone = new Phone() { Id = 1, Brand = brand, Type = "test" };

            //Act
            string fullName = brand.Name + " " + phone.Type;

            //Assert
            Assert.Equal(fullName, phone.FullName);
        }

        [Fact]
        public void Should_SetBrandId()
        {
            //Arrange
            var phone = new Phone() { Id = 1, BrandId = 2, Type = "test" };
            int id = 2;

            //Act

            //Assert
            Assert.Equal(id, phone.BrandId);
        }
    }
}
