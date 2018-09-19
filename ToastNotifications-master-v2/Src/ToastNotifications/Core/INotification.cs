using System;

namespace ToastNotifications.Core
{
    public interface INotification
    {
        int Id { get; set; }

        NotificationDisplayPart DisplayPart { get; }

        INotificationConfiguration Configuration { get; set; }

        void Bind(Action<INotification> closeAction);

        void Close();

        string Message { get; }

        bool CanClose { get; set; }
    }
}