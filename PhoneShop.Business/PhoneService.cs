using Phoneshop.Business.Extensions;
using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

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
            phoneRepository.Mapper = PhoneMapper;
        }

        public Phone Get(int id)
        {
            if (id <= 0) return null;

            using (var command = new SqlCommand("SELECT * FROM phones WHERE Id=" + id))
            {
                return phoneRepository.GetRecord(command);
            }
        }

        public IEnumerable<Phone> Get()
        {
            using (var command = new SqlCommand("SELECT phones.*, Brands.Id as BrandsId, Brands.Name as BrandName FROM phones INNER JOIN Brands ON phones.BrandId = brands.id Order By Brands.Name"))
            {
                return phoneRepository.GetRecords(command);
            }
        }

        public IEnumerable<Phone> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException(nameof(query));

            var moviesFromDb = phoneRepository.GetRecords(new SqlCommand($"SELECT phones.*, Brands.Id as BrandsId, Brands.Name as BrandName FROM phones " +
                $"INNER JOIN Brands ON phones.BrandId = brands.id " +
                $"WHERE Type like '%{query}%' OR description like '%{query}%' OR Brands.Name like '%{query}%' Order By Brands.Name"));

            return moviesFromDb;
        }

        public void Create(Phone phone)
        {
            var found = phoneRepository
                .GetRecord(new SqlCommand($@"select TOP 1 * FROM phones P
                                            INNER JOIN brands B on P.brandid = B.Id
                                            WHERE P.[Type] = '{phone.Type}' AND B.Name = '{phone.Brand.Name}'"));

            if (found != null)
                throw new Exception($"Phone {phone.Brand.Name} - {phone.Type} already exists");

            var brand = brandService.GetOrCreate(phone.Brand.Name);

            var command = new SqlCommand("INSERT INTO Phones (BrandId, Stock, Type, Description, Price) VALUES (@BrandId, @Stock, @Type, @Description, @Price)");
            command.Parameters.AddWithValue("@BrandId", brand.Id);
            command.Parameters.AddWithValue("@Description", phone.Description);
            command.Parameters.AddWithValue("@Type", phone.Type);
            command.Parameters.AddWithValue("@Price", phone.Price);
            command.Parameters.AddWithValue("@Stock", phone.Stock);

            phoneRepository.ExecuteNonQuery(command);
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

            var command = new SqlCommand("DELETE FROM Phones WHERE Id=@id");
            command.Parameters.AddWithValue("@id", id);

            phoneRepository.ExecuteNonQuery(command);
        }

        [ExcludeFromCodeCoverage]
        public Phone PhoneMapper(SqlDataReader reader)
        {
            return new Phone()
            {
                Id = reader.GetInt("Id"),
                BrandId = reader.GetInt("brandid"),
                Description = reader.GetString("Description"),
                Price = reader.GetDouble("price"),
                Stock = reader.GetInt("stock"),
                Type = reader.GetString("Type"),
                Brand = new Brand
                {
                    Id = reader.GetInt("BrandsId"),
                    Name = reader.GetString("BrandName")
                }
            };
        }
    }
}
