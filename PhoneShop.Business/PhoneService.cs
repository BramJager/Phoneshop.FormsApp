using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Phoneshop.Business
{
    public class PhoneService : IPhoneService
    {

        private readonly IRepository<Phone> phoneRepository;
        private readonly IBrandService brandService;

        public PhoneService(IRepository<Phone> phoneRepository, IBrandService brandService)
        {
            this.phoneRepository = phoneRepository;
            this.brandService = brandService;
        }

        public Phone Get(int id)
        {
            if (id <= 0) return null;

            return phoneRepository.GetWithRelatedData(id, d => d.Brand);
        }

        public IEnumerable<Phone> Get()
        {
            return phoneRepository.GetWithRelatedData(d => d.Brand);
        }

        public IEnumerable<Phone> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException(nameof(query));

            var phonesFromDb = phoneRepository.Get().Where(x => x.FullName.Contains(query));

            return phonesFromDb;
        }

        public void Create(Phone phone)
        {
            var found = phoneRepository.Get(phone.Id);

            if (found != null)
                throw new Exception($"Phone {phone.Brand.Name} - {phone.Type} already exists");

            var brand = brandService.GetOrCreate(phone.Brand.Name);
            phone.Brand = brand;

            phoneRepository.Create(phone);
            phoneRepository.Save();
        }

        public void Create(List<Phone> phones)
        {
            foreach (var item in phones)
                Create(item);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id));

            var phone = phoneRepository.Get(id);
            phoneRepository.Delete(phone);
            phoneRepository.Save();
        }

        //[ExcludeFromCodeCoverage]
        //public Phone PhoneMapper(SqlDataReader reader)
        //{
        //    return new Phone()
        //    {
        //        Id = reader.GetInt("Id"),
        //        BrandId = reader.GetInt("brandid"),
        //        Description = reader.GetString("Description"),
        //        Price = reader.GetDouble("price"),
        //        Stock = reader.GetInt("stock"),
        //        Type = reader.GetString("Type"),
        //        Brand = new Brand
        //        {
        //            Id = reader.GetInt("BrandsId"),
        //            Name = reader.GetString("BrandName")
        //        }
        //    };
        //}
    }
}
