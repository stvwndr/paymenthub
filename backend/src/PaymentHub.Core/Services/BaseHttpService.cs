using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentHub.Core.Notifications.Interfaces;

namespace PaymentHub.Core.Services;

public abstract class BaseHttpService
{
    private readonly HttpClient _httpClient;
    private readonly INotificationHandler _notificationHandler;
    private readonly ILogger<BaseHttpService> _logger;
    private const string GenericIntegrationErrorMessageKey = "HttpGenericSendAsyncError";
    private const string IntegrationErrorMessageKey = "HttpSendAsyncError";

    protected BaseHttpService(HttpClient httpClient, 
        ILogger<BaseHttpService> logger,
        INotificationHandler notificationHandler)
    {
        _httpClient = httpClient;
        _notificationHandler = notificationHandler;
        _logger = logger;
    }

    protected async Task<TResponse> SendAsync<TResponse>(string requestUri, HttpMethod httpMethod, object content = null,
        IList<KeyValuePair<string, object>> properties = null, IList<KeyValuePair<string, string>> headers = null,
        bool ignoreNullValues = false)
    {
        var requestMessage = HttpRequestMessageBuilder(requestUri, httpMethod, content, properties, headers, ignoreNullValues);

        var response = await _httpClient.SendAsync(requestMessage);

        var contentResult = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<TResponse>(contentResult);

        _notificationHandler.AddNotification(!string.IsNullOrWhiteSpace(contentResult)
            ? contentResult!
            : response.ReasonPhrase ?? string.Empty);

        return default!;
    }

    protected async Task<HttpResponseMessage> SendAsync(string requestUri, HttpMethod httpMethod,
        object content = null, IList<KeyValuePair<string, object>> properties = null,
        IList<KeyValuePair<string, string>> headers = null,
        bool ignoreNullValues = false)
    {
        var requestMessage = HttpRequestMessageBuilder(requestUri, httpMethod, content, properties, headers, ignoreNullValues);

        var response = await _httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode) return response;

        var contentResult = await response.Content.ReadAsStringAsync();

        _notificationHandler.AddNotification(!string.IsNullOrWhiteSpace(contentResult)
            ? contentResult!
            : response.ReasonPhrase ?? string.Empty);

        return response;
    }

    private HttpRequestMessage HttpRequestMessageBuilder(string requestUri, HttpMethod httpMethod, object content,
        IList<KeyValuePair<string, object>> properties = null, IList<KeyValuePair<string, string>> headers = null,
        bool ignoreNullValues = false)
    {
        var uri = new Uri(requestUri, UriKind.RelativeOrAbsolute);

        var options = new JsonSerializerSettings();
        if (ignoreNullValues)
        {
            options.NullValueHandling = NullValueHandling.Ignore;
        }

        var stringContent = content != null
            ? new StringContent(JsonConvert.SerializeObject(content, options), System.Text.Encoding.UTF8, "application/json")
            : null;

        var requestMessage = new HttpRequestMessage
        {
            RequestUri = uri,
            Method = httpMethod,
            Content = stringContent
        };

        properties?.ToList().ForEach(p =>
            requestMessage.Properties.Add(p));

        headers?.ToList().ForEach(p =>
            requestMessage.Headers.Add(p.Key, p.Value));

        return requestMessage;
    }
}
