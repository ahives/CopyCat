using Microsoft.Extensions.Configuration;

namespace CopyCat.Data;

using Model;
using Microsoft.EntityFrameworkCore;

public class ImpersonationDbContext :
    DbContext
{
    public DbSet<AccountEntity> Accounts { get; set; }
    
    public DbSet<ImpersonatedAccountEntity> ImpersonatedAccounts { get; set; }

    public ImpersonationDbContext(DbContextOptions<ImpersonationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // IConfiguration configuration = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .Build();
        
            optionsBuilder.UseNpgsql("Host=localhost;Database=Impersonation;Username=admin;Password=");
        }
        // base.OnConfiguring(optionsBuilder);
    }
}