namespace EduHub.Ch02;

public enum NotificationChannel { Email, Sms, Slack }

public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}