using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Entities;
using System.Configuration;

namespace PhoneShop.Business
{
    public class DataContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }

        public DbSet<Brand> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PhoneshopDB"].ConnectionString);
        }
    }
}
