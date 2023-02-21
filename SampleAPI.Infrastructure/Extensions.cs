using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleAPI.Core.Constants;
using SampleAPI.Infrastructure.DAO;
using SampleAPI.Infrastructure.Services;

namespace SampleAPI.Infrastructure;

public static class Extensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(LibreTranslateEndpoints.TRANSLATE_API_CLIENT,
            client => client.BaseAddress = new Uri(configuration.GetSection(SectionNameConstants.APP_SETTINGS_SECTION)[LibreTranslateEndpoints.TRANSLATE_API_URL_KEY])
        );

        services.AddScoped<ITranslatorService, TranslatorService>();

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SampleAPIConnection")));

        return services;
    }
}