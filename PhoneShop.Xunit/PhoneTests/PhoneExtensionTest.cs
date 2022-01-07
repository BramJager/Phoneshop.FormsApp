using Phoneshop.Domain.Entities;
using Phoneshop.Business.Extensions;
using Xunit;

namespace PhoneShop.Xunit.PhoneTests
{
    public class PhoneExtensionTest
    {
        [Fact]
        public void Should_GetPriceWithoutVat()
        {
            //Arrange
            var phone = new Phone() { Id = 1, Price = 12 };

            //Act
            var priceWithouVat = phone.PriceWithoutVat();
            var expected = phone.Price - phone.Price * 0.21;

            //Assert
            Assert.Equal(expected, priceWithouVat);
        }
    }
}
