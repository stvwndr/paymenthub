using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentHub.Ame.Application.Features.Payment.Commands;
using PaymentHub.Ame.Infra.Dtos;
using PaymentHub.Ame.Infra.Services.Interfaces;
using PaymentHub.Core.Dtos.Ame;
using PaymentHub.Core.Enums;
using PaymentHub.Core.Notifications.Interfaces;

namespace PaymentHub.Ame.Application.Features.Payment.Handlers;

public class AmePaymentHandler : 
    IRequestHandler<GetQrCodeCommand, byte[]>,
    IRequestHandler<GiftCardPaymentCommand, AmePayWithGiftCardResponseDto>
{
    private readonly IAmeService _ameService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public AmePaymentHandler(IAmeService ameService,
        ILogger<AmePaymentHandler> logger,
        INotificationHandler notificationHandler)
    {
        _ameService = ameService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<byte[]> Handle(GetQrCodeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Obtendo novo QRCode -> Ame | TransactionId: {request.TransactionId}");

        //TODO: Grava o request na base

        var response = await _ameService.GetQrCode((AmeQrCodeRequestDto)request);

        _logger.LogWarning($"Resposta criação do QRCode -> Ame" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }

    public async Task<AmePayWithGiftCardResponseDto> Handle(GiftCardPaymentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Criando novo pagamento com GiftCard -> Ame | TransactionId: {request.TransactionId}");

        //TODO: Grava o request na base

        var response = await _ameService.PayWithGiftCard((PayWithGiftCardRequestDto)request);

        _logger.LogWarning($"Resposta criação novo pagamento com GiftCard -> Ame | {JsonConvert.SerializeObject(response)}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : new AmePayWithGiftCardResponseDto
            {
                PaymentId = request.TransactionId,
                Amount = response.Amount,
                Status = response.Status == AmeEnum.PaymentStatus.Authorized
                    ? PaymentHubEnum.PaymentStatus.Authorized
                    : PaymentHubEnum.PaymentStatus.Denied
            };
    }
}
