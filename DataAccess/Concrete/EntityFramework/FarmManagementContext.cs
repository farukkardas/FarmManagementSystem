using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Concrete.EntityFramework
{
    public class FarmManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-1TJQMOV;Database=Farm01;user id=sa;password=05366510050Ab*;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Cow> Cows { get; set; }
        public DbSet<Bull> Bulls { get; set; }
        public DbSet<Calves> Calves { get; set; }
        public DbSet<Sheep> Sheeps { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<FuelConsumption> FuelConsumptions { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }
        public DbSet<Provender> Provenders { get; set; }
        public DbSet<MilkSales> MilkSales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AnimalSales> AnimalSales { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsOnSale> ProductsOnSale { get; set; }
    }
}