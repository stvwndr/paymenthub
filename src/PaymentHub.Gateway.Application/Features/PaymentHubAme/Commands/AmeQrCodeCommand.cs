using MediatR;
using Newtonsoft.Json.Linq;

namespace PaymentHub.Gateway.Application.Features.PaymentHubAme.Commands;

public class AmeQrCodeCommand : IRequest<JToken>
{
    public Guid TransactionId => Guid.NewGuid();
    public Guid CustomerId { get; set; }
}
