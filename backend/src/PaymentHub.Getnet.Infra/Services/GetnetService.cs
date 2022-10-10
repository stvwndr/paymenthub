using Microsoft.Extensions.Logging;
using PaymentHub.Core.Factories;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Core.Services;
using PaymentHub.Getnet.Infra.Dtos;
using PaymentHub.Getnet.Infra.Services.Interfaces;

namespace PaymentHub.Getnet.Infra.Services;

public class GetnetService : BaseHttpService, IGetnetService
{
    private readonly HttpClient _httpClient;

    public GetnetService(HttpClient httpClient,
        ILogger<GetnetService> logger,
        INotificationHandler notificationHandler)
        : base(httpClient, logger, notificationHandler)
    {
        _httpClient = httpClient;
    }
   
    public Task<byte[]> GetPixQrCode(GetnetPixRequestDto requestDto)
    {
        return Task.FromResult(QrCodeFactory.GenerateQrCode(
            $"{_httpClient.BaseAddress}?customerid={requestDto.CustomerId}"
            ));
    }
}
