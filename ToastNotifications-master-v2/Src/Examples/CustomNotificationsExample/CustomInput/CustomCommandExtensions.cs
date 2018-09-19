using ToastNotifications;

namespace CustomNotificationsExample.CustomInput
{
    public static class CustomInputExtensions
    {
        public static void ShowCustomInput(this Notifier notifier, string message)
        {
            notifier.Notify(() => new CustomInputNotification(message, message));
        }
    }
}
