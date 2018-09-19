using ToastNotifications.Core;

namespace ToastNotifications.Messages.Core
{
    public class MessageConfiguration : BaseNotificationConfiguration
    {
        public double? FontSize { get; set; }
        public object Tag { get; set; }
    }
}