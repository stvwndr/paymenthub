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
            var url = Environment.GetEnvironmentVariable("PAYMENT_HUB_PAGSEGURO_URL")
                ?? configuration["Services:PaymentHubPagSeguroUrl"];
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddHttpClient<IPaymentHubGetnetService, PaymentHubGetnetService>(c =>
        {
            var url = Environment.GetEnvironmentVariable("PAYMENT_HUB_GETNET_URL")
                ?? configuration["Services:PaymentHubGetnetUrl"];
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddHttpClient<IPaymentHubAmeService, PaymentHubAmeService>(c =>
        {
            var url = Environment.GetEnvironmentVariable("PAYMENT_HUB_AME_URL")
                ?? configuration["Services:PaymentHubAmeUrl"];
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}