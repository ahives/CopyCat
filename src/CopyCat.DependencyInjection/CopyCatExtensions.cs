using CopyCat.Data;
using CopyCat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopyCat.DependencyInjection;

public static class CopyCatExtensions
{
    public static IServiceCollection AddCopyCat(this IServiceCollection services)
    {
        services.AddDbContext<ImpersonationDbContext>();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<ImpersonationDbContext>(x =>
            x.UseNpgsql(configuration.GetConnectionString("ImpersonationDbConnection")));

        services.AddScoped<IImpersonationAdminDataProvider, ImpersonationAdminDataProvider>();
        services.AddScoped<IAccountAdminDataProvider, AccountAdminDataProvider>();
        services.AddScoped<IAccountImpersonationDataProvider, AccountImpersonationDataProvider>();
        services.AddScoped<IAccountAdminService, AccountAdminService>();
        services.AddScoped<IImpersonatedAccountAdminService, ImpersonatedAccountAdminService>();
        services.AddScoped<IAccountImpersonationService, AccountImpersonationService>();

        return services;
    }
}