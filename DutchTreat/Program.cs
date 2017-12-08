using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DutchTreat
{
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration(SetupConfiguration)
                   .UseStartup<Startup>()
                   .Build();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Remove default configurations options
            builder.Sources.Clear();

            builder.AddJsonFile("config.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();
        }
    }
}
