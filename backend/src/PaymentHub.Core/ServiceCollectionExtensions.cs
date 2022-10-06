using Microsoft.Extensions.DependencyInjection;
using PaymentHub.Core.Notifications;
using PaymentHub.Core.Notifications.Interfaces;

namespace PaymentHub.Core;

public static class ServiceCollectionExtensions
{
    public static void AddPaymentHubCore(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
