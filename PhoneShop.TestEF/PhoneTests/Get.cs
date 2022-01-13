using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Phoneshop.Domain.Entities;
using Xunit;

namespace PhoneShop.TestEF.PhoneTests
{
    public class Get : BaseTest
    {
        public Get() : base()
        {

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ReturnNull_When_IdIsZeroOrLower(int id)
        {
            //Arrange
            

            //Act
            var phone = phoneService.Get(id);
            
            //Assert
            Assert.Null(phone);
        }

        [Fact]
        public void Should_GetPhone()
        {
            //Arrange
            int id = 1;
            List<Phone> list = new List<Phone>() { new Phone() { Id = id, Brand = new Brand() { Id = 1 } } };
            phoneRepository.Setup(x => x.GetWithRelatedData(id, d => d.Brand)).Returns(list.AsQueryable());

            //Act
            var phone = phoneService.Get(id);

            //Assert
            Assert.Equal(id, phone.Id);
            phoneRepository.Verify(x => x.GetWithRelatedData(id, d => d.Brand), Times.Once);
        }

        [Fact]
        public void Should_GetListOfPhones()
        {
            //Arrange
            List<Phone> list = new() { new Phone() { Id = 1, Brand = new Brand() { } }, new Phone() };
            phoneRepository.Setup(x => x.GetWithRelatedData(d => d.Brand)).Returns(list);

            //Act
            var resultList = phoneService.Get();

            //Assert
            phoneRepository.Verify(x => x.GetWithRelatedData(d => d.Brand), Times.Once);
            Assert.Equal(list.Count(), resultList.Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowArgumentNullException_When_QueryIsNullOrEmpty(string query)
        {
            //Arrange


            //Act
            Action act = () => phoneService.Search(query);

            //Assert
            Exception ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'query')", ex.Message);
        }

        //[Fact]
        //public void Should_GetListOfPhones_WhenQueryIsNotEmptyOrNull()
        //{
        //    //Arrange
        //    var query = "test";
        //    List<Phone> list = new() { new Phone() { Id = 1, Brand = new Brand() { Name = "test" } }, new Phone() };
        //    phoneRepository.Setup(x => x.Get()).Returns(list);

        //    //Act
        //    var resultList = phoneService.Search(query);

        //    //Assert
        //    phoneRepository.Verify(x => x.Get(), Times.Once);
        //    Assert.Equal(1, resultList.Count());
        //}
    }
}
