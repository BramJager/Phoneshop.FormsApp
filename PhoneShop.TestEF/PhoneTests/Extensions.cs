using Phoneshop.Business.Extensions;
using Phoneshop.Domain.Entities;
using Xunit;

namespace PhoneShop.TestEF.PhoneTests
{
    public class Extensions
    {
        [Fact]
        public void Should_GetPriceWithoutVat()
        {
            //Arrange
            var phone = new Phone() { Price = 12.00M };

            //Act
            var price = phone.PriceWithoutVat();

            //Assert
            Assert.Equal(phone.Price - (phone.Price * 0.21M), price);
        }
    }
}
