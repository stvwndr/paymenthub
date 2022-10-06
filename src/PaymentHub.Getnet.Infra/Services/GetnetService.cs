using Microsoft.Extensions.Logging;
using PaymentHub.Core.Factories;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Getnet.Infra.Dtos;
using PaymentHub.Getnet.Infra.Services.Interfaces;

namespace PaymentHub.Getnet.Infra.Services;

public class GetnetService : BaseHttpService, IGetnetService
{
    public GetnetService(HttpClient httpClient,
        ILogger<GetnetService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
    }

    private const string _getnetPixQrCodeUri = "https://site.getnet.com.br/pix/";
    
    public Task<byte[]> GetPixQrCode(PixRequestDto requestDto)
    {
        return Task.FromResult(QrCodeFactory.GenerateQrCode(
            $"{_getnetPixQrCodeUri}?customerid={requestDto.CustomerId}"
            ));
    }
}
