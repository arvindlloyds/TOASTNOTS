using System;
using System.Windows;
using ToastNotifications.Core;
using ToastNotifications.Display;
using ToastNotifications.Events;
using ToastNotifications.Lifetime;
// ReSharper disable LocalizableElement

namespace ToastNotifications
{
    public class Notifier : IDisposable
    {
        private readonly object _syncRoot;

        private readonly Action<NotifierConfiguration> _configureAction;
        private NotifierConfiguration _configuration;
        private INotificationsLifetimeSupervisor _lifetimeSupervisor;
        private NotificationsDisplaySupervisor _displaySupervisor;

        public Notifier(Action<NotifierConfiguration> configureAction)
        {
            _configureAction = configureAction;
            _syncRoot = new object();
        }

        public void Notify(Func<INotification> createNotificationFunc)
        {
            Configure();
            _lifetimeSupervisor.PushNotification(createNotificationFunc());
        }

        private void Configure()
        {
            lock (_syncRoot)
            {
                if (_configuration != null)
                    return;

                var cfg = CreateConfiguration();

                var keyboardEventHandler = cfg.KeyboardEventHandler ?? new BlockAllKeyInputEventHandler();

                _configuration = cfg;
                _lifetimeSupervisor = cfg.LifetimeSupervisor;
                _lifetimeSupervisor.UseDispatcher(cfg.Dispatcher);

                _displaySupervisor = new NotificationsDisplaySupervisor(
                    cfg.Dispatcher,
                    cfg.PositionProvider,
                    cfg.LifetimeSupervisor,
                    cfg.DisplayOptions,
                    keyboardEventHandler);
            }
        }

        private NotifierConfiguration CreateConfiguration()
        {
            var cfg = new NotifierConfiguration
            {
                Dispatcher = Application.Current.Dispatcher
            };
            _configureAction(cfg);

            if (cfg.LifetimeSupervisor == null)
                throw new ArgumentNullException(nameof(cfg.LifetimeSupervisor), "Missing configuration argument");

            if (cfg.PositionProvider == null)
                throw new ArgumentNullException(nameof(cfg.PositionProvider), "Missing configuration argument");
            return cfg;
        }

        public void ClearMessages()
        {
            ClearMessages("");
        }

        public void ClearMessages(string msg)
        {
            _lifetimeSupervisor?.ClearMessages(msg);
        }


        public object SyncRoot => _syncRoot;

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _configuration?.PositionProvider?.Dispose();
                _displaySupervisor?.Dispose();
                _lifetimeSupervisor?.Dispose();
            }

            _disposed = true;
        }

    }
}
