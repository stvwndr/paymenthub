using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.PagSeguro;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Commands;
using PaymentHub.Gateway.Infra.Services.Interfaces;

namespace PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Handlers;

public class PagSeguroCreatePaymentHandler : IRequestHandler<PagSeguroCreatePaymentCommand, PagSeguroCreatePaymentResponseDto>
{
    private readonly IPaymentHubPagSeguroService _pagSeguroService;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger _logger;

    public PagSeguroCreatePaymentHandler(IPaymentHubPagSeguroService pagSeguroService,
        ILogger<PagSeguroCreatePaymentHandler> logger,
        INotificationHandler notificationHandler)
    {
        _pagSeguroService = pagSeguroService;
        _logger = logger;
        _notificationHandler = notificationHandler;
    }

    public async Task<PagSeguroCreatePaymentResponseDto> Handle(PagSeguroCreatePaymentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Solicitando criação de novo pagamento -> PagSeguro | TransactionId: {request.TransactionId}");

        var response = await _pagSeguroService.SendPayment(JObject.FromObject(request));

        _logger.LogWarning($"Resposta criação novo pagamento -> PagSeguro | {JsonConvert.SerializeObject(response)}" +
            $" | Houve notificações? {_notificationHandler.HasNotifications}" +
            $" | TransactionId: {request.TransactionId}");

        return _notificationHandler.HasNotifications
            ? default!
            : response;
    }
}
