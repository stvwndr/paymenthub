using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.Ame;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubAmeService : BaseHttpService, IPaymentHubAmeService
{
    public PaymentHubAmeService(HttpClient httpClient,
        ILogger<PaymentHubAmeService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    private string _getQrCodeUrl(Guid transactionId, Guid customerId)
        => $"qrcode/{transactionId}/{customerId}";
    private string _sendGiftCardPayment => $"payment/giftcard";

    public async Task<JToken> GetQrCode(Guid transactionId, Guid customerId)
    {
        return await SendAsync<JToken>(
            _getQrCodeUrl(transactionId, customerId),
            HttpMethod.Get);
    }

    public async Task<AmePayWithGiftCardResponseDto> SendGiftCardPayment(JObject data)
    {
        return await SendAsync<AmePayWithGiftCardResponseDto>(_sendGiftCardPayment,
           HttpMethod.Post,
           data);
    }
}
