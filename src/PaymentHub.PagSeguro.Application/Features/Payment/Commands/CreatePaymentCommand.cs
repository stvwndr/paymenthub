using MediatR;
using PaymentHub.Core.Dtos.PagSeguro;
using PaymentHub.PagSeguro.Infra.Dtos;

namespace PaymentHub.PagSeguro.Application.Features.Payment.Commands;

public class CreatePaymentCommand : IRequest<PagSeguroCreatePaymentResponseDto>
{
    public Guid TransactionId { get; set; }
    public string GivenName { get; set; }
    public string CardNumber { get; set; }
    public string ValidThru { get; set; }
    public int Code { get; set; }
    public decimal Amount { get; set; }


    public static explicit operator SendPaymentRequestDto(CreatePaymentCommand request)
    {
        return new SendPaymentRequestDto
        {
            TransactionId = request.TransactionId,
            GivenName = request.GivenName,
            CardNumber = request.CardNumber,
            Amount = request.Amount
        };
    }
}