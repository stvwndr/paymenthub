namespace PaymentHub.Ame.Infra.Dtos;

public class PayWithGiftCardRequestDto
{
    public Guid TransactionId { get; set; }
    public string GiftCardNumber { get; set; }
    public decimal Amount { get; set; }
}
