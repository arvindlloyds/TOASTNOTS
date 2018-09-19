using ToastNotifications;

namespace CustomNotificationsExample.CustomMessage
{
    public static class CustomMessageExtensions
    {
        public static void ShowCustomMessage(this Notifier notifier, string title, string message)
        {
            notifier.Notify(() => new CustomNotification(title, message));
        }
    }
}
