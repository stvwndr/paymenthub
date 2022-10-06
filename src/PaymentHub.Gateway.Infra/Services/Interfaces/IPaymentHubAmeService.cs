using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.Ame;

namespace PaymentHub.Gateway.Infra.Services.Interfaces;

public interface IPaymentHubAmeService
{
    Task<JToken> GetQrCode(Guid transactionId, Guid customerId);
    Task<AmePayWithGiftCardResponseDto> SendGiftCardPayment(JObject data);
}
