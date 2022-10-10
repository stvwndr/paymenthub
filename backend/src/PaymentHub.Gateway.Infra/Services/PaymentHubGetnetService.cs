using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubGetnetService : BaseHttpService, IPaymentHubGetnetService
{
    private readonly IHttpContextAccessor _httpContext;

    public PaymentHubGetnetService(HttpClient httpClient,
        IHttpContextAccessor httpContext,
        ILogger<PaymentHubGetnetService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
        _httpContext = httpContext;
    }

    private string _getQrCodeUrl(Guid transactionId, Guid customerId) 
        => $"pix-qrcode/{transactionId}/{customerId}";

    public async Task<JToken> GetQrCodePix(Guid transactionId, Guid customerId)
    {
        return await SendAsync<JToken>(
            _getQrCodeUrl(transactionId, customerId),
            HttpMethod.Get,
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
