namespace PaymentHub.Getnet.Application.Dtos;

public class GetPixQrCodeRequestDto
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }
}
