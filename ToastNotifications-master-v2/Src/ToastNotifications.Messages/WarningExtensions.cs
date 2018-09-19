using ToastNotifications.Messages.Core;
using ToastNotifications.Messages.Warning;

namespace ToastNotifications.Messages
{
    public static class WarningExtensions
    {
        public static void ShowWarning(this Notifier notifier, string message)
        {
            notifier.Notify(() => new WarningMessage(message));
        }

        public static void ShowWarning(this Notifier notifier, string message, MessageConfiguration configuration)
        {
            notifier.Notify(() => new WarningMessage(message, configuration));
        }
    }
}
