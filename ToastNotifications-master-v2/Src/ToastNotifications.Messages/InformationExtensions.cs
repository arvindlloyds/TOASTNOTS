using ToastNotifications.Messages.Core;
using ToastNotifications.Messages.Information;

namespace ToastNotifications.Messages
{
    public static class InformationExtensions
    {
        public static void ShowInformation(this Notifier notifier, string message)
        {
            notifier.Notify(() => new InformationMessage(message));
        }

        public static void ShowInformation(this Notifier notifier, string message, MessageConfiguration configuration)
        {
            notifier.Notify(() => new InformationMessage(message, configuration));
        }
    }
}
