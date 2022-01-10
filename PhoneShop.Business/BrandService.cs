using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Linq;

namespace Phoneshop.Business
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public Brand GetOrCreate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var result = brandRepository.Get().FirstOrDefault(x => x.Name == name);

            if (result == null)
            {
                Create(new Brand { Name = name });
                return GetOrCreate(name);
            }

            return result;
        }

        public void Create(Brand brand)
        {
            brandRepository.Create(brand);
            brandRepository.Save();
        }

        //[ExcludeFromCodeCoverage]
        //public static Brand PhoneMapper(SqlDataReader reader)
        //{
        //    return new()
        //    {
        //        Id = reader.GetInt("Id"),
        //        Name = reader.GetString("Name"),
        //    };
        //}
    }
}
