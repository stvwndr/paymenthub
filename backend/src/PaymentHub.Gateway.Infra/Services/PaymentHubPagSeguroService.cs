using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.PagSeguro;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Infra.Services;

public class PaymentHubPagSeguroService : BaseHttpService, IPaymentHubPagSeguroService
{
    private readonly IHttpContextAccessor _httpContext;

    public PaymentHubPagSeguroService(HttpClient httpClient,
        IHttpContextAccessor httpContext,
        ILogger<PaymentHubPagSeguroService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
        _httpContext = httpContext;
    }

    private string _sendPaymentUrl => "payment";

    public async Task<PagSeguroCreatePaymentResponseDto> SendPayment(JObject data)
    {
        return await SendAsync<PagSeguroCreatePaymentResponseDto>(_sendPaymentUrl, 
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
