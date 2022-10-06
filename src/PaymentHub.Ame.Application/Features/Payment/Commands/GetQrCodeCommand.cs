using MediatR;
using PaymentHub.Ame.Infra.Dtos;

namespace PaymentHub.Ame.Application.Features.Payment.Commands;

public class GetQrCodeCommand : IRequest<byte[]>
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }


    public static explicit operator AmeQrCodeRequestDto(GetQrCodeCommand request)
    {
        return new AmeQrCodeRequestDto
        {
            TransactionId = request.TransactionId,
            CustomerId = request.CustomerId
        };
    }
}
