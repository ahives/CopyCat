using CopyCat.Data;
using CopyCat.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ImpersonationDbContext>();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<ImpersonationDbContext>(x =>
    x.UseNpgsql(configuration.GetConnectionString("ImpersonationDbConnection")));

builder.Services.AddScoped<IImpersonationAdminDataProvider, ImpersonationAdminDataProvider>();
builder.Services.AddScoped<IAccountAdminDataProvider, AccountAdminDataProvider>();
builder.Services.AddScoped<IAccountAdminService, AccountAdminService>();
builder.Services.AddScoped<IImpersonatedAccountAdminService, ImpersonatedAccountAdminService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();