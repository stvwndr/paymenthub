using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentHub.Core;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.PagSeguro.Application.Features.Payment.Commands;
using PaymentHub.PagSeguro.Infra;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPaymentHubCore();
builder.Services.AddPagSeguroInfra(builder.Configuration);
builder.Services.AddMediatR(typeof(CreatePaymentCommand).Assembly);

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
            "PaymentHub PagSeguro Api v1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.MapPost("/payment", async (
    [FromBody] CreatePaymentCommand command, 
    [FromServices] IMediator mediator, 
    [FromServices] INotificationHandler notificationHandler) =>
{
    var response = await mediator.Send(command);

    return notificationHandler.HasNotifications
        ? Results.BadRequest(notificationHandler.NotificationResponse)
        : Results.Created("payment", response);
});


app.Run();
