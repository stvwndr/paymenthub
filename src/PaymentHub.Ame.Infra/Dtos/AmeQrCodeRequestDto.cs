namespace PaymentHub.Ame.Infra.Dtos;

public class AmeQrCodeRequestDto
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }
}

