using static PaymentHub.Core.Enums.AmeEnum;

namespace PaymentHub.Ame.Infra.Dtos;

public class PayWithGiftCardResponseDto
{
    public Guid TransactionId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string ErrorMessage { get; set; }
}
