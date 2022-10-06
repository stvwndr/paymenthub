using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubGetnetService : BaseHttpService, IPaymentHubGetnetService
{
    public PaymentHubGetnetService(HttpClient httpClient,
        ILogger<PaymentHubGetnetService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    private string _getQrCodeUri(Guid transactionId, Guid customerId) 
        => $"pix-qrcode/{transactionId}/{customerId}";

    public async Task<JToken> GetQrCodePix(Guid transactionId, Guid customerId)
    {
        return await SendAsync<JToken>(
            _getQrCodeUri(transactionId, customerId),
            HttpMethod.Get);
    }
}
