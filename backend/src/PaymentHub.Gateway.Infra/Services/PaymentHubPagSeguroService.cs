using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.PagSeguro;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubPagSeguroService : BaseHttpService, IPaymentHubPagSeguroService
{
    public PaymentHubPagSeguroService(HttpClient httpClient,
        ILogger<PaymentHubPagSeguroService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    private string _sendPaymentUri => "payment";

    public async Task<PagSeguroCreatePaymentResponseDto> SendPayment(JObject data)
    {
        return await SendAsync<PagSeguroCreatePaymentResponseDto>(_sendPaymentUri, 
            HttpMethod.Post, 
            data);
    }
}
