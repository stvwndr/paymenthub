using Microsoft.Extensions.Logging;
using PaymentHub.Ame.Infra.Dtos;
using PaymentHub.Ame.Infra.Services.Interfaces;
using PaymentHub.Core.Factories;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using static PaymentHub.Core.Enums.AmeEnum;

namespace PaymentHub.Ame.Infra.Services;

public class AmeService : BaseHttpService, IAmeService
{
    public AmeService(HttpClient httpClient,
        ILogger<AmeService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    private const string _ameQrCodeUri = "https://www.amedigital.com/";

    public Task<byte[]> GetQrCode(AmeQrCodeRequestDto request)
    {
        return Task.FromResult(QrCodeFactory.GenerateQrCode(
            $"{_ameQrCodeUri}?customerid={request.CustomerId}"
            ));
    }

    public Task<PayWithGiftCardResponseDto> PayWithGiftCard(PayWithGiftCardRequestDto request)
    {
        return Task.FromResult(
            new PayWithGiftCardResponseDto
            {
                TransactionId = request.TransactionId,
                Amount = request.Amount,
                Status = PaymentStatus.Authorized
            });
    }
}

