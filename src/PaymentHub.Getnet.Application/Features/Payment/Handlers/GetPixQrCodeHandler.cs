using MediatR;
using Microsoft.Extensions.Logging;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Getnet.Application.Features.Payment.Commands;
using PaymentHub.Getnet.Infra.Dtos;
using PaymentHub.Getnet.Infra.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentHub.Getnet.Application.Features.Payment.Handlers;

public class GetPixQrCodeHandler : IRequestHandler<GetPixQrCodeCommand, byte[]>
{
    private readonly IGetnetService _getnetService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public GetPixQrCodeHandler(IGetnetService getnetService,
        ILogger<GetPixQrCodeHandler> logger,
        INotificationHandler notificationHandler)
    {
        _getnetService = getnetService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<byte[]> Handle(GetPixQrCodeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Obtendo novo QRCode para PIX -> Getnet | TransactionId: {request.TransactionId}");

        //Grava o request na base

        var response = await _getnetService.GetPixQrCode((PixRequestDto)request);

        _logger.LogWarning($"Resposta criação do QRCode -> Getnet" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }
}
