using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentHub.Getnet.Infra.Services;
using PaymentHub.Getnet.Infra.Services.Interfaces;
using System.Net.Http.Headers;

namespace PaymentHub.Getnet.Infra;

public static class ServiceCollectionExtensions
{
    public static void AddGetnetInfra(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddHttpClient<IGetnetService, GetnetService>(c =>
        {
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(configuration["Services:GetnetUri"]);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}
