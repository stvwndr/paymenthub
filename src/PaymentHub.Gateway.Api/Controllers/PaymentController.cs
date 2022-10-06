using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Commands;

namespace PaymentHub.Gateway.Api.Controllers
{
    [ApiController]
    [Route("payment")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("pagseguro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePaymentPagSeguro([FromBody] PagSeguroCreatePaymentCommand command,
            [FromServices] IMediator mediator,
            [FromServices] INotificationHandler notificationHandler)
        {
            var response = await mediator.Send(command);

            return notificationHandler.HasNotifications
                ? BadRequest(notificationHandler.NotificationResponse)
                : Created("payment", response);
        }
    }
}
