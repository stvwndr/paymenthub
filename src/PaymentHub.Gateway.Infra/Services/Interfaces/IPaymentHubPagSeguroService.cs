using Newtonsoft.Json.Linq;
using PaymentHub.Core.Dtos.PagSeguro;

namespace PaymentHub.Gateway.Infra.Services.Interfaces;

public interface IPaymentHubPagSeguroService
{
    Task<PagSeguroCreatePaymentResponseDto> SendPayment(JObject data);
}
