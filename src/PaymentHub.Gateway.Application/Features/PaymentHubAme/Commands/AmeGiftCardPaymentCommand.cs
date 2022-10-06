using MediatR;
using PaymentHub.Core.Dtos.Ame;

namespace PaymentHub.Gateway.Application.Features.PaymentHubAme.Commands;

public class AmeGiftCardPaymentCommand : IRequest<AmePayWithGiftCardResponseDto>
{
    public Guid TransactionId => Guid.NewGuid();
    public string GiftCardNumber { get; set; }
    public decimal Amount { get; set; }
}

