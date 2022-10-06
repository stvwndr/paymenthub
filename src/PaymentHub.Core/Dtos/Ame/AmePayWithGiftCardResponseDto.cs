using PaymentHub.Core.Enums;

namespace PaymentHub.Core.Dtos.Ame;

public class AmePayWithGiftCardResponseDto
{
    public Guid PaymentId { get; set; }
    public PaymentHubEnum.PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string ErrorMessage { get; set; }
}
