using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PhoneShop.Business
{
    [ExcludeFromCodeCoverage]
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Brand> Brands { get; set; }
    }
}
