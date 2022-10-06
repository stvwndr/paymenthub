using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentHub.Gateway.Infra.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;
using System.Net.Http.Headers;

namespace PaymentHub.Gateway.Infra;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayInfra(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddHttpClient<IPaymentHubPagSeguroService, PaymentHubPagSeguroService>(c =>
        {
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(configuration["Services:PaymentHubPagSeguroUri"]);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddHttpClient<IPaymentHubGetnetService, PaymentHubGetnetService>(c =>
        {
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(configuration["Services:PaymentHubGetnetUri"]);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}