using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PaymentHub.Core;
using PaymentHub.Core.Auth;
using PaymentHub.Core.Notifications.Interfaces;
using PaymentHub.PagSeguro.Application.Features.Payment.Commands;
using PaymentHub.PagSeguro.Infra;
using Serilog;
using Serilog.Sinks.Elasticsearch;
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
builder.Services.AddPagSeguroInfra(builder.Configuration);
builder.Services.AddMediatR(typeof(CreatePaymentCommand).Assembly);

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
            IndexFormat = $"paymenthub-pagseguro-prod-{DateTime.UtcNow:yyyy-MM}"
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
        "PaymentHub PagSeguro Api v1");
});


if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapPost("/payment", [Authorize] async (
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
