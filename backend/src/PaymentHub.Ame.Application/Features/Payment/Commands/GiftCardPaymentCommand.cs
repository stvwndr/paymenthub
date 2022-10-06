using MediatR;
using PaymentHub.Ame.Infra.Dtos;
using PaymentHub.Core.Dtos.Ame;

namespace PaymentHub.Ame.Application.Features.Payment.Commands;

public class GiftCardPaymentCommand : IRequest<AmePayWithGiftCardResponseDto>
{
    public Guid TransactionId { get; set; }
    public string GiftCardNumber { get; set; }
    public decimal Amount { get; set; }


    public static explicit operator PayWithGiftCardRequestDto(GiftCardPaymentCommand request)
    {
        return new PayWithGiftCardRequestDto
        {
            TransactionId = request.TransactionId,
            GiftCardNumber = request.GiftCardNumber,
            Amount = request.Amount
        };
    }
}
