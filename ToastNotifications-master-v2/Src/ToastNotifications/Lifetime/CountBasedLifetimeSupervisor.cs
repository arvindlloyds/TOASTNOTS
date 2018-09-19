﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using ToastNotifications.Core;

namespace ToastNotifications.Lifetime
{
    public class CountBasedLifetimeSupervisor : INotificationsLifetimeSupervisor
    {
        private readonly int _maximumNotificationCount;
        private NotificationsList _notifications;

        public CountBasedLifetimeSupervisor(MaximumNotificationCount maximumNotificationCount)
        {
            _maximumNotificationCount = maximumNotificationCount.Count;

            _notifications = new NotificationsList();
        }

        public void PushNotification(INotification notification)
        {
            if (_disposed)
            {
                Debug.WriteLine($"Warn ToastNotifications {this}.{nameof(PushNotification)} is already disposed");
                return;
            }

            int numberOfNotificationsToClose = Math.Max(_notifications.Count - _maximumNotificationCount, 0);

            var notificationsToRemove = _notifications
                .OrderBy(x => x.Key)
                .Take(numberOfNotificationsToClose)
                .Select(x => x.Value)
                .ToList();

            foreach (var n in notificationsToRemove)
                CloseNotification(n.Notification);

            _notifications.Add(notification);
            RequestShowNotification(new ShowNotificationEventArgs(notification));
        }

        public void CloseNotification(INotification notification)
        {
            _notifications.TryRemove(notification.Id, out var removedNotification);
            RequestCloseNotification(new CloseNotificationEventArgs(removedNotification.Notification));
        }

        protected virtual void RequestShowNotification(ShowNotificationEventArgs e)
        {
            ShowNotificationRequested?.Invoke(this, e);
        }

        protected virtual void RequestCloseNotification(CloseNotificationEventArgs e)
        {
            CloseNotificationRequested?.Invoke(this, e);
        }


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
                _notifications?.Clear();
                _notifications = null;
            }

            _disposed = true;
        }

        public void UseDispatcher(Dispatcher dispatcher)
        {
        }


        public void ClearMessages(string msg)
        {
        }

        public event EventHandler<ShowNotificationEventArgs> ShowNotificationRequested;
        public event EventHandler<CloseNotificationEventArgs> CloseNotificationRequested;
    }
}