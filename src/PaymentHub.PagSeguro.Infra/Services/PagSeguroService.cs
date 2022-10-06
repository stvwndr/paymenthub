using Microsoft.Extensions.Logging;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.PagSeguro.Infra.Dtos;
using PaymentHub.PagSeguro.Infra.Services.Interfaces;
using static PaymentHub.Core.Enums.PagSeguroEnum;

namespace PaymentHub.PagSeguro.Infra.Services;

public class PagSeguroService : BaseHttpService, IPagSeguroService
{
    public PagSeguroService(HttpClient httpClient,
        ILogger<PagSeguroService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    public Task<SendPaymentResponseDto> SendPayment(SendPaymentRequestDto request)
    {
        return Task.FromResult(
            new SendPaymentResponseDto
            {
                TransactionId = request.TransactionId,
                Amount = request.Amount,
                Status = PaymentStatus.Authorized
            });
    }
}
