using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.Ame;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubAme.Commands;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Application.Features.PaymentHubAme.Handlers;

public class AmeCreatePaymentHandler :
    IRequestHandler<AmeQrCodeCommand, JToken>,
    IRequestHandler<AmeGiftCardPaymentCommand, AmePayWithGiftCardResponseDto>
{
    private readonly IPaymentHubAmeService _ameService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public AmeCreatePaymentHandler(IPaymentHubAmeService ameService,
        ILogger<AmeCreatePaymentHandler> logger,
        INotificationHandler notificationHandler)
    {
        _ameService = ameService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<JToken> Handle(AmeQrCodeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Solicitando QRCode -> Ame | TransactionId: {request.TransactionId}");

        var response = await _ameService.GetQrCode(request.TransactionId,
            request.CustomerId);

        _logger.LogWarning($"Resposta geração QRCode -> Ame | {(string?)response}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }

    public async Task<AmePayWithGiftCardResponseDto> Handle(AmeGiftCardPaymentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Solicitando criação de novo pagamento com GiftCard -> Ame | TransactionId: {request.TransactionId}");

        var response = await _ameService.SendGiftCardPayment(JObject.FromObject(request));

        _logger.LogWarning($"Resposta criação novo pagamento com GiftCard -> Ame | {JsonConvert.SerializeObject(response)}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }
}
