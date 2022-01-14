using Xunit;
using Moq;
using Phoneshop.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PhoneShop.TestEF.PhoneTests
{
    public class Create : BaseTest
    {
        public Create() : base() { }

        [Fact]
        public void Should_ThrowException_When_PhoneExists()
        {
            //Arrange
            int id = 1;
            var phone = new Phone() { Id = id, Brand = new Brand() { Id = 1, Name = "test" }, Type = "test", BrandId = 1, Description = "test", Price = 1, Stock = 1 };
            phoneRepository.Setup(x => x.Get(id)).Returns(phone);

            //Act
            Action act = () => phoneService.Create(phone);

            //Assert
            Exception ex = Assert.Throws<Exception>(act);
            Assert.Equal("Phone test - test already exists", ex.Message);

        }

        [Fact]
        public void Should_CreatPhone()
        {
            //Arrange
            int id = 2;
            var brand = new Brand() { Id = id, Name = "test" };
            var phone = new Phone() { Id = id, Brand = brand };
            phoneRepository.Setup(x => x.Get(id)).Returns((Phone)null);
            brandRepository.Setup(x => x.Get()).Returns(new List<Brand>() { brand });

            //Act
            phoneService.Create(phone);

            //Assert
            phoneRepository.Verify(x => x.Create(phone), Times.Once);
            phoneRepository.Verify(x => x.Get(id), Times.Once);
            brandRepository.Verify(x => x.Get(), Times.Once);
        }

        [Fact]
        public void Should_CreateListOfPhones()
        {
            //Arrange
            int id = 1;            
            var brand = new Brand() { Id = id, Name = "test" };
            var list = new List<Phone>() { new Phone() { Brand = brand}, new Phone() { Brand = brand }, new Phone() { Brand = brand } };
            phoneRepository.Setup(x => x.Get(id)).Returns((Phone)null);
            brandRepository.Setup(x => x.Get()).Returns(new List<Brand>() { brand });

            //Act
            phoneService.Create(list);

            //Assert
            phoneRepository.Verify(x => x.Create(It.IsAny<Phone>()), Times.Exactly(3));
            phoneRepository.Verify(x => x.Get(It.IsAny<int>()), Times.Exactly(3));
            brandRepository.Verify(x => x.Get(), Times.Exactly(3));
        }
    }
}
