namespace DutchTreat.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Microsoft.Extensions.Configuration;

    public class DutchContext : IdentityDbContext<StoreUser>
    {
        private readonly IConfiguration _config;

        public DutchContext(DbContextOptions<DutchContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DutchConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
