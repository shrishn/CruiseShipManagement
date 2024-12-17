using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CruiseShipManagement.DataAccess
{
    public class CruiseShipContextFactory : IDesignTimeDbContextFactory<CruiseShipContext>
    {
        public CruiseShipContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CruiseShipContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new CruiseShipContext(optionsBuilder.Options);
        }
    }
}
