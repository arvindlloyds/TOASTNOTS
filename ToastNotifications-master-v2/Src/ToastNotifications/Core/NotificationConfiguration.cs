using System;

namespace ToastNotifications.Core
{
    public class BaseNotificationConfiguration: INotificationConfiguration
    {
        public bool ShowCloseButton { get; set; }
        public bool FreezeOnMouseEnter { get; set; }
        public bool UnfreezeOnMouseLeave { get; set; }

        public Action<INotification> NotificationClickAction { get; set; }
        public Action<INotification> CloseClickAction { get; set; }

        public BaseNotificationConfiguration()
        {
            ShowCloseButton = true;
            FreezeOnMouseEnter = true;
            UnfreezeOnMouseLeave = false;
        }
    }
}
