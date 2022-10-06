using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubGetnet.Commands;
using PaymentHub.Gateway.Infra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentHub.Gateway.Application.Features.PaymentHubGetnet.Handlers;

public class GetnetPixQrCodeHandler : IRequestHandler<GetnetPixQrCodeCommand, JToken>
{
    private readonly IPaymentHubGetnetService _getnetService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public GetnetPixQrCodeHandler(IPaymentHubGetnetService getnetService,
        ILogger<GetnetPixQrCodeHandler> logger,
        INotificationHandler notificationHandler)
    {
        _getnetService = getnetService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<JToken> Handle(GetnetPixQrCodeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Solicitando QRCode para PIX -> Getnet | TransactionId: {request.TransactionId}");

        var response = await _getnetService.GetQrCodePix(request.TransactionId, 
            request.CustomerId);

        _logger.LogWarning($"Resposta geração QRCode -> Getnet | {(string?)response}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }
}
