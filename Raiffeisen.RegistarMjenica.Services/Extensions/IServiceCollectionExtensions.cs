using DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raiffeisen.RegistarMjenica.Services.Contexts;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;
using Raiffeisen.RegistarMjenica.Api.Services.Repositories;
using Raiffeisen.RegistarMjenica.Api.Services.Services;

namespace Raiffeisen.RegistarMjenica.Api.Services.Exceptions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRfLogger, RfLogger>();
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddCustomServices();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name })
                .EnableSensitiveDataLogging();
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>))
            .AddScoped<IMjenicaRepository, MjenicaRepository>()
            .AddScoped<IMjenicaExemptionHistoryRepository, MjenicaExemptionHistoryRepository>();
    }

    private static void AddCustomServices(this IServiceCollection services)
    {
        services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IRBBHApiService, RBBHApiService>()
            .AddSingleton<IExportService, ExportService>()
            .AddTransient<IMjenicaJobService, MjenicaJobService>()
            .AddScoped<IMjenicaService, MjenicaService>()
            // .AddScoped<IImportService, ImportService>()
            .AddScoped<IMjenicaExemptionHistoryService, MjenicaExemptionHistoryService>();
    }
}