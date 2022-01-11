using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phoneshop.Domain.Entities;
using System.Configuration;

namespace PhoneShop.Business
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Brand> Brands { get; set; }
    }
}
