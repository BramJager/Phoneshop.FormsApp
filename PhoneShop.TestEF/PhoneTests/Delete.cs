using System;
using Moq;
using Phoneshop.Domain.Entities;
using Xunit;

namespace PhoneShop.TestEF.PhoneTests
{
    public class Delete : BaseTest
    {
        public Delete() : base() { }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ThrowArgumentNullException_When_IdIsZeroOrLower(int id)
        {
            //Arrange


            //Act
            Action act = () => phoneService.Delete(id);

            //Assert
            Exception ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'id')", ex.Message);
        }

        [Fact]
        public void Should_DeletePhone()
        {
            //Arrange
            int id = 1;
            var phone = new Phone() { Id = id };
            phoneRepository.Setup(x => x.Get(id)).Returns(phone);

            //Act
            phoneService.Delete(id);

            //Assert
            phoneRepository.Verify(x => x.Get(id), Times.Once);
            phoneRepository.Verify(x => x.Delete(phone), Times.Once);
        }

    }
}
