using Newtonsoft.Json;
using PaymentHub.Core.Notifications.Interfaces;

namespace PaymentHub.Core.Notifications;

public class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notifications;

    public NotificationHandler()
    {
        _notifications = new List<Notification>();
    }


    public NotificationErrorMessage NotificationResponse => new NotificationErrorMessage
    {
        Details = _notifications.Select(c => new MessageDetails
        {
            DetailedMessage = c.Key,
            Message = c.Message
        })
    };

    public bool HasNotifications => _notifications.Any();
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void AddNotification(string notificationErrorMessage)
    {
        if (!notificationErrorMessage.ToLower().Contains("detailedmessage"))
            AddNotification("Error", notificationErrorMessage);
        else
        {
            var notifications = JsonConvert.DeserializeObject<NotificationErrorMessage>(notificationErrorMessage);

            notifications?.Details?.ToList().ForEach(c => AddNotification(c.DetailedMessage, c.Message));
        }
    }

    private void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }   
}
