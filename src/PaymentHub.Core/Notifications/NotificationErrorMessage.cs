namespace PaymentHub.Core.Notifications;

public class NotificationErrorMessage
{
    public string Message { get; set; } = "Error";
    public IEnumerable<MessageDetails> Details { get; set; }
}

public class MessageDetails
{
    public string DetailedMessage { get; set; }
    public string Message { get; set; }
}