using System;

namespace ToastNotifications.Core
{
    public interface INotificationConfiguration
    {
        bool ShowCloseButton { get; set; }
        bool FreezeOnMouseEnter { get; set; }
        bool UnfreezeOnMouseLeave { get; set; }

        Action<INotification> NotificationClickAction { get; set; }
        Action<INotification> CloseClickAction { get; set; }
    }
}
