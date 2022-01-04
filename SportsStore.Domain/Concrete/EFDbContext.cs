using SportsStore.Domain.Entities;
using System.Data.Entity;
using Npgsql;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext():base(nameOrConnectionString: "EFDbContext")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
