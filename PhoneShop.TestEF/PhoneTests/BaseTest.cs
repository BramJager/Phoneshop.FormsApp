using Moq;
using Phoneshop.Business;
using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;

namespace PhoneShop.TestEF.PhoneTests
{
    public class BaseTest
    {
        public readonly PhoneService phoneService;
        public readonly Mock<IRepository<Phone>> phoneRepository;
        public readonly Mock<IRepository<Brand>> brandRepository;
        public readonly BrandService brandService;

        public BaseTest()
        {
            phoneRepository = new Mock<IRepository<Phone>>();
            brandRepository = new Mock<IRepository<Brand>>();
            brandService = new BrandService(brandRepository.Object);
            phoneService = new PhoneService(phoneRepository.Object, brandService);
        }
    }
}
