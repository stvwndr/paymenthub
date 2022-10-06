namespace PaymentHub.PagSeguro.Infra.Dtos;

public class SendPaymentRequestDto
{
    public Guid TransactionId { get; set; }
    public string GivenName { get; set; }
    public string CardNumber { get; set; }
    public decimal Amount { get; set; }
}
