using Newtonsoft.Json.Linq;

namespace PaymentHub.Gateway.Infra.Services.Interfaces;

public interface IPaymentHubGetnetService
{
    Task<JToken> GetQrCodePix(Guid transactionId, Guid customerId);
}
