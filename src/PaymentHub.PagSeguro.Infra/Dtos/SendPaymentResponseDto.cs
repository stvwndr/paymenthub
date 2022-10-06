using static PaymentHub.Core.Enums.PagSeguroEnum;

namespace PaymentHub.PagSeguro.Infra.Dtos
{
    public class SendPaymentResponseDto
    {
        public Guid TransactionId { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
