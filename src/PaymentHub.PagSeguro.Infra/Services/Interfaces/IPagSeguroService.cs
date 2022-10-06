using PaymentHub.PagSeguro.Infra.Dtos;

namespace PaymentHub.PagSeguro.Infra.Services.Interfaces;

public interface IPagSeguroService
{
    Task<SendPaymentResponseDto> SendPayment(SendPaymentRequestDto request);
}
