using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PaymentHub.Core;
using PaymentHub.Core.Auth;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.Getnet.Application.Features.Payment.Commands;
using PaymentHub.Getnet.Infra;
using Serilog;
using System.Text.Json.Serialization;

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
builder.Services.AddGetnetInfra(builder.Configuration);
builder.Services.AddMediatR(typeof(GetPixQrCodeCommand).Assembly);

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

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json",
        "PaymentHub Getnet Api v1");
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapGet("/pix-qrcode/{customerId}/{transactionId}", [Authorize] async (
    Guid customerId, 
    Guid transactionId,
    [FromServices] IMediator mediator,
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(new GetPixQrCodeCommand
    {
        TransactionId = transactionId,
        CustomerId = customerId
    });

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Ok(Convert.ToBase64String(response));
});


app.Run();
