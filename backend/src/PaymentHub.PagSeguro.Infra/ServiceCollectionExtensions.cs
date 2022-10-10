using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentHub.PagSeguro.Infra.Services;
using PaymentHub.PagSeguro.Infra.Services.Interfaces;
using System.Net.Http.Headers;

namespace PaymentHub.PagSeguro.Infra;

public static class ServiceCollectionExtensions
{
    public static void AddPagSeguroInfra(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddHttpClient<IPagSeguroService, PagSeguroService>(c =>
        {
            c.DefaultRequestHeaders.Accept.Clear();
            c.BaseAddress = new Uri(configuration["Services:PagSeguroUrl"]);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}
