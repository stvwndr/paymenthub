using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PaymentHub.Core;
using PaymentHub.Core.Auth;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Gateway.Application.Features.PaymentHubAme.Commands;
using PaymentHub.Gateway.Application.Features.PaymentHubGetnet.Commands;
using PaymentHub.Gateway.Application.Features.PaymentHubPagSeguro.Commands;
using PaymentHub.Gateway.Infra;
using Serilog;
using System.Text.Json.Serialization;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Basic"
    })
);

builder.Services.AddPaymentHubCore();
builder.Services.AddGatewayInfra(builder.Configuration);
builder.Services.AddMediatR(typeof(PagSeguroCreatePaymentCommand).Assembly);

builder.Services
    .AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddAuthorization();


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Logging.ClearProviders();
var loggerBuilder = new LoggerConfiguration()
    .Enrich.WithProperty("Creation", DateTime.UtcNow)
    .Enrich.FromLogContext()
    .WriteTo.Console();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production)
{
    loggerBuilder
        .MinimumLevel.Warning()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(
        new Uri(Environment.GetEnvironmentVariable("PAYMENT_HUB_ELASTICSEARCH_URL")
            ?? builder.Configuration["Services:ElasticConfiguration"])
        )
        {
            AutoRegisterTemplate = true,
            ConnectionTimeout = new TimeSpan(0, 0, 10),
            IndexFormat = $"paymenthub-gateway-prod-{DateTime.UtcNow:yyyy-MM}"
        });
}
builder.Logging.AddSerilog(loggerBuilder.CreateLogger());


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json",
        "PaymentHub Gateway Api v1");
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapPost("/payment/pagseguro", [Authorize] async (
    [FromBody] PagSeguroCreatePaymentCommand command,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(command);

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Created("payment", response);
});
app.MapGet("/payment/pix/qrcode/{customerId}", [Authorize] async (
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
app.MapGet("/payment/ame/qrcode/{customerId}", [Authorize] async (
    Guid customerId,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(new AmeQrCodeCommand
    {
        CustomerId = customerId
    });

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Ok((string?)response);
});
app.MapPost("/payment/ame/giftcard", [Authorize] async (
    [FromBody] AmeGiftCardPaymentCommand command,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(command);

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Created("payment", response);
});

app.Run();
