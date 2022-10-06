using MediatR;
using PaymentHub.Core.Dtos.PagSeguro;

namespace PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Commands;

public class PagSeguroCreatePaymentCommand : IRequest<PagSeguroCreatePaymentResponseDto>
{
    public Guid TransactionId => Guid.NewGuid();
    public string GivenName { get; set; }
    public string CardNumber { get; set; }
    public string ValidThru { get; set; }
    public int Code { get; set; }
    public decimal Amount { get; set; }
}
