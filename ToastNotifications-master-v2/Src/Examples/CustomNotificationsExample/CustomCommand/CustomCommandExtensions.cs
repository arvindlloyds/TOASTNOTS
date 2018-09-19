using System;
using ToastNotifications;

namespace CustomNotificationsExample.CustomCommand
{
    public static class CustomCommandExtensions
    {
        public static void ShowCustomCommand(this Notifier notifier, string message, Action<CustomCommandNotification> confirmAction, Action<CustomCommandNotification> declineAction)
        {
            notifier.Notify(() => new CustomCommandNotification(message, confirmAction, declineAction));
        }
    }
}
