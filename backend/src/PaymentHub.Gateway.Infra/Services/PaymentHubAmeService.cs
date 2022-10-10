using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.Ame;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubAmeService : BaseHttpService, IPaymentHubAmeService
{
    private readonly IHttpContextAccessor _httpContext;

    public PaymentHubAmeService(HttpClient httpClient,
        IHttpContextAccessor httpContext,
        ILogger<PaymentHubAmeService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
        _httpContext = httpContext;
    }

    private string _getQrCodeUrl(Guid transactionId, Guid customerId)
        => $"qrcode/{transactionId}/{customerId}";
    private string _sendGiftCardPayment => $"payment/giftcard";

    public async Task<JToken> GetQrCode(Guid transactionId, Guid customerId)
    {
        return await SendAsync<JToken>(
            _getQrCodeUrl(transactionId, customerId),
            HttpMethod.Get, 
            headers: GetHeaders);
    }

    public async Task<AmePayWithGiftCardResponseDto> SendGiftCardPayment(JObject data)
    {
        return await SendAsync<AmePayWithGiftCardResponseDto>(_sendGiftCardPayment,
           HttpMethod.Post,
           data,
           headers: GetHeaders);
    }

    private IList<KeyValuePair<string, string>> GetHeaders =>
        new List<KeyValuePair<string, string>> {
            new (
                "Authorization",
                _httpContext.HttpContext.Request.Headers["Authorization"].ToString()
            )
        };
}
