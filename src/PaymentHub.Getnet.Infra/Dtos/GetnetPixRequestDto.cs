namespace PaymentHub.Getnet.Infra.Dtos;

public class GetnetPixRequestDto
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }
}
