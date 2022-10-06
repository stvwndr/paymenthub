namespace PaymentHub.Core.Notifications.Interfaces;

public interface INotificationHandler
{
    bool HasNotifications { get; }
    NotificationErrorMessage NotificationResponse { get; }
    void AddNotification(string notificationErrorMessage);
}
