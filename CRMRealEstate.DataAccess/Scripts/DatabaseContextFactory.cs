using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRMRealEstate.DataAccess.Scripts
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../CRMRealEstate.API");


            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var connectionString = configuration.GetConnectionString("ConnectionString");

            optionsBuilder.UseNpgsql(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}