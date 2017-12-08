namespace DutchTreat.Data
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

    public class DutchContext : DbContext
    {
        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
