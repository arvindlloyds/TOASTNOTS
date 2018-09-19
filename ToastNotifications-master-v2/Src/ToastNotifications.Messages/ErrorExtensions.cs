using ToastNotifications.Messages.Core;
using ToastNotifications.Messages.Error;

namespace ToastNotifications.Messages
{
    public static class ErrorExtensions
    {
        public static void ShowError(this Notifier notifier, string message)
        {
            notifier.Notify(() => new ErrorMessage(message));
        }

        public static void ShowError(this Notifier notifier, string message, MessageConfiguration configuration)
        {
            notifier.Notify(() => new ErrorMessage(message, configuration));
        }
    }
}
