using Moq;
using Phoneshop.Business;
using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;

namespace PhoneShop.TestEF.BrandTests
{
    public class BaseTest
    {
        public readonly Mock<IRepository<Brand>> brandRepository;
        public readonly BrandService brandService;

        public BaseTest()
        {
            brandRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(brandRepository.Object);
        }
    }
}
