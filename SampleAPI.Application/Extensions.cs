using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleAPI.Infrastructure;

namespace SampleAPI.Application;

public static class Extensions
{
    public static IServiceCollection RegisterApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructure(configuration);

        return services;
    }
}