using PaymentHub.Core.Enums;

namespace PaymentHub.Core.Dtos.PagSeguro;

public class PagSeguroCreatePaymentResponseDto
{
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public PaymentHubEnum.PaymentStatus Status { get; set; }
}
