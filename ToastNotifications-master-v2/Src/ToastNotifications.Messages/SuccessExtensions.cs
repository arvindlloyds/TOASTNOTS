using ToastNotifications.Messages.Core;
using ToastNotifications.Messages.Success;

namespace ToastNotifications.Messages
{
    public static class SuccessExtensions
    {
        public static void ShowSuccess(this Notifier notifier, string message)
        {
            notifier.Notify(() => new SuccessMessage(message));
        }

        public static void ShowSuccess(this Notifier notifier, string message, MessageConfiguration configuration)
        {
            notifier.Notify(() => new SuccessMessage(message, configuration));
        }
    }
}
