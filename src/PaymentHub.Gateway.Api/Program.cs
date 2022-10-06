using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentHub.Core;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubGetnet.Commands;
using PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Commands;
using PaymentHub.Gateway.Infra;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPaymentHubCore();
builder.Services.AddGatewayInfra(builder.Configuration);
builder.Services.AddMediatR(typeof(PagSeguroCreatePaymentCommand).Assembly);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json",
            "PaymentHub Gateway Api v1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.MapPost("/payment/pagseguro", async (
    [FromBody] PagSeguroCreatePaymentCommand command,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(command);

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Created("payment", response);
});
app.MapGet("/payment/pix-qrcode/customerId", async (
    Guid customerId,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(new GetnetPixQrCodeCommand
    {
        CustomerId = customerId
    });

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Ok((string?)response);
});

app.Run();
