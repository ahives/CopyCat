using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CopyCat.Data;

public class ImpersonationDbFactory :
    IDesignTimeDbContextFactory<ImpersonationDbContext>
{
    public ImpersonationDbContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ImpersonationDbContext>()
            .UseNpgsql(configuration.GetConnectionString("ImpersonationDbConnection"), options => options.EnableRetryOnFailure());

        return new ImpersonationDbContext(optionsBuilder.Options);
    }
}