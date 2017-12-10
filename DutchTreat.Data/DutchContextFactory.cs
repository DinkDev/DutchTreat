namespace DutchTreat.Data
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DutchContextFactory : IDesignTimeDbContextFactory<DutchContext>
    {
        public DutchContext CreateDbContext(string[] args)
        {
            // Create a configuration 
            var builder = new WebHostBuilder();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            return new DutchContext(new DbContextOptionsBuilder<DutchContext>().Options, config);
        }
    }
}
