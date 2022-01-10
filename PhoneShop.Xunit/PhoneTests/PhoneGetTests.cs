using Moq;
using Phoneshop.Business;
using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace PhoneShop.Xunit.PhoneTests
{
    public class PhoneGetTests
    {
        private readonly Mock<IRepository<Brand>> brandRepository;
        private readonly Mock<IRepository<Phone>> phoneRepository;
        private readonly IBrandService brandService;
        private readonly IPhoneService phoneService;

        public PhoneGetTests()
        {
            brandRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(brandRepository.Object);
            phoneRepository = new Mock<IRepository<Phone>>();
            phoneService = new PhoneService(phoneRepository.Object, brandService);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ReturnNull_WhenIdIsZeroOrUnder(int id)
        {
            //Arrange


            //Act
            var phone = phoneService.Get(id);

            //Assert
            Assert.Null(phone);
        }


        [Fact]
        public void Should_GetPhoneById()
        {
            //Arrange
            var testPhone = new Phone() { Id = 1};
            var id = 1;
            phoneRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns(testPhone);

            //Act
            var phone = phoneService.Get(id);

            //Assert
            phoneRepository.Verify(r => r.GetRecord(It.IsAny<SqlCommand>()), Times.Once);
            Assert.Equal(id, phone.Id);
        }

        [Fact]
        public void Should_GetListOfPhones()
        {
            //Arrange
            var testList = new List<Phone>() { new Phone() { Id = 1 }, new Phone() { Id = 2 } };
            phoneRepository.Setup(r => r.GetRecords(It.IsAny<SqlCommand>()))
                                        .Returns(testList);

            //Act
            var list = phoneService.Get();

            //Assert
            phoneRepository.Verify(r => r.GetRecords(It.IsAny<SqlCommand>()), Times.Once);
            Assert.Equal(testList, list);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_ThrowArgumentNullException_When_QueryIsNullOrEmpty(string query)
        {
            //Arrange


            //Act
            Action action = () => phoneService.Search(query);

            //Assert
            Exception exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("Value cannot be null. (Parameter 'query')", exception.Message);
        }

        [Fact]
        public void Should_GetListOfPhones_When_QueryIsNotNull()
        {
            //Arrange
            var testList = new List<Phone>() { new Phone() { Id = 1 }, new Phone() { Id = 2 } };
            phoneRepository.Setup(r => r.GetRecords(It.IsAny<SqlCommand>()))
                                        .Returns(testList);

            string query = "test";

            //Act
            var list = phoneService.Search(query);

            //Assert
            phoneRepository.Verify(r => r.GetRecords(It.IsAny<SqlCommand>()), Times.Once);
            Assert.Equal(testList, list);
        }

        [Fact]
        public void Should_ThrowException_WhenPhoneAlreadyExists()
        {
            //Arrange
            Phone phone = new Phone() { Id = 1, Brand = new Brand() { Name = "test"}, Type = "test" };
            phoneRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns(phone);

            //Act
            Action act = () => phoneService.Create(phone);

            //Assert
            phoneRepository.Verify(r => r.ExecuteNonQuery(It.IsAny<SqlCommand>()), Times.Never);
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Phone test - test already exists", exception.Message);
        }

        [Fact]
        public void Should_CreatePhone()
        {
            //Arrange
            Phone phone = new Phone() { Id = 1, Brand = new Brand() { Name = "test" }, Type = "test" };
            phoneRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns((Phone)null);
            brandRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns(new Brand() { Id = 1, Name = "test" });

            //Act
            phoneService.Create(phone);

            //Assert
            phoneRepository.Verify(r => r.ExecuteNonQuery(It.IsAny<SqlCommand>()), Times.Once);
            brandRepository.Verify(r => r.GetRecord(It.IsAny<SqlCommand>()), Times.Once);
        }

        [Fact]
        public void Should_CreateListOfPhones()
        {
            //Arrange
            var brand = new Brand() { Name = "test" };
            var phoneList = new List<Phone>()
                {
                    new Phone() { Id = 1, Brand = brand, Type = "test" },
                    new Phone() { Id = 2, Brand = brand, Type = "test" },
                    new Phone() { Id = 3, Brand = brand, Type = "test" }
                };

            phoneRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns((Phone)null);
            brandRepository.Setup(r => r.GetRecord(It.IsAny<SqlCommand>())).Returns(new Brand() { Id = 1, Name = "test" });

            //Act
            phoneService.Create(phoneList);

            //Assert
            phoneRepository.Verify(r => r.ExecuteNonQuery(It.IsAny<SqlCommand>()), Times.Exactly(3));
            brandRepository.Verify(r => r.GetRecord(It.IsAny<SqlCommand>()), Times.Exactly(3));
        }
    }
}
