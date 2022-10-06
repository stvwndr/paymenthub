using MediatR;
using PaymentHub.Getnet.Infra.Dtos;

namespace PaymentHub.Getnet.Application.Features.Payment.Commands;

public class GetPixQrCodeCommand : IRequest<byte[]>
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }


    public static explicit operator GetnetPixRequestDto(GetPixQrCodeCommand request)
    {
        return new GetnetPixRequestDto
        {
            TransactionId = request.TransactionId,
            CustomerId = request.CustomerId
        };
    }
}
