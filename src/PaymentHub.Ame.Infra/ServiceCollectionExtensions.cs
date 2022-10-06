using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentHub.Ame.Infra.Services;
using PaymentHub.Ame.Infra.Services.Interfaces;
using System.Net.Http.Headers;

namespace PaymentHub.Ame.Infra;

public static class ServiceCollectionExtensions
{
    public static void AddAmeInfra(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddHttpClient<IAmeService, AmeService>(c =>
        {
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(configuration["Services:AmeUri"]);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}
