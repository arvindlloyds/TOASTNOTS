using System;

namespace ToastNotifications.Core
{
    public abstract class NotificationBase : INotification
    {
        public virtual int Id { get; set; }

        private Action<INotification> _closeAction;

        public virtual bool CanClose { get; set; } = true;

        public abstract NotificationDisplayPart DisplayPart { get; }

        public INotificationConfiguration Configuration { get; set; }

        public virtual void Bind(Action<INotification> closeAction)
        {
            _closeAction = closeAction;
        }

        public virtual void Close()
        {
            Configuration.CloseClickAction?.Invoke(this);
            _closeAction?.Invoke(this);
        }

        public virtual string Message => DisplayPart.GetMessage();
    }
}