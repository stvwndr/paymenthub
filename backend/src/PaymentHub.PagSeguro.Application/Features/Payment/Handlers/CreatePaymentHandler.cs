using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentHub.Core.Dtos.PagSeguro;
using PaymentHub.Core.Enums;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.PagSeguro.Application.Features.Payment.Commands;
using PaymentHub.PagSeguro.Infra.Dtos;
using PaymentHub.PagSeguro.Infra.Services.Interfaces;

namespace PaymentHub.PagSeguro.Application.Features.Payment.Handlers;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, PagSeguroCreatePaymentResponseDto>
{
    private readonly IPagSeguroService _pagSeguroService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public CreatePaymentHandler(IPagSeguroService pagSeguroService,
        ILogger<CreatePaymentHandler> logger,
        INotificationHandler notificationHandler)
    {
        _pagSeguroService = pagSeguroService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<PagSeguroCreatePaymentResponseDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Criando novo pagamento -> PagSeguro | TransactionId: {request.TransactionId}");

        //TODO: Grava o request na base

        var response = await _pagSeguroService.SendPayment((SendPaymentRequestDto)request);

        _logger.LogWarning($"Resposta criação novo pagamento -> PagSeguro | {JsonConvert.SerializeObject(response)}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : new PagSeguroCreatePaymentResponseDto
            {
                PaymentId = request.TransactionId,
                Amount = response.Amount,
                Status = response.Status == PagSeguroEnum.PaymentStatus.Authorized
                    ? PaymentHubEnum.PaymentStatus.Authorized
                    : PaymentHubEnum.PaymentStatus.Denied
            };
    }
}