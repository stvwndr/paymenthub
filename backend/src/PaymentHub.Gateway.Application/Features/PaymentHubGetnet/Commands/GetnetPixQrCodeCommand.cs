using MediatR;
using Newtonsoft.Json.Linq;

namespace PaymentHub.Gateway.Application.Features.PaymentHubGetnet.Commands;

public class GetnetPixQrCodeCommand : IRequest<JToken>
{
    public Guid TransactionId => Guid.NewGuid();
    public Guid CustomerId { get; set; }
}
