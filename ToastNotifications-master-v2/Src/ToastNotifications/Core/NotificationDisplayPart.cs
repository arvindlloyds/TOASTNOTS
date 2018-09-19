using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications.Display;

namespace ToastNotifications.Core
{
    public abstract class NotificationDisplayPart : UserControl
    {
        protected INotificationAnimator Animator;

        protected INotification Notification { get; set; }

        protected INotificationConfiguration Configuration => Notification?.Configuration;

        protected NotificationDisplayPart()
        {
            Animator = new NotificationAnimator(this, TimeSpan.FromMilliseconds(300), TimeSpan.FromMilliseconds(300));

            Margin = new Thickness(1);
            Animator.Setup();
            MinHeight = 60;

            Loaded += OnLoaded;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Configuration?.NotificationClickAction?.Invoke(Notification);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (Configuration != null && Configuration.FreezeOnMouseEnter)
            {
                Notification.CanClose = false;
                SetCloseButtonVisibility(Visibility.Visible);
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (Configuration != null)
            {
                if (Configuration.FreezeOnMouseEnter && Configuration.UnfreezeOnMouseLeave)
                {
                    Notification.CanClose = true;
                }

                var closeButtonVisibility = Configuration.ShowCloseButton ? Visibility.Visible : Visibility.Hidden;
                SetCloseButtonVisibility(closeButtonVisibility);
            }

            base.OnMouseLeave(e);
        }

        public virtual string GetMessage()
        {
            return Notification.Message;
        }

        public void Bind<TNotification>(TNotification notification) where TNotification : INotification
        {
            Notification = notification;
            DataContext = Notification;
        }

        private  void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Notification == null)
            {
                throw new InvalidOperationException(
                    $"Unbound notification. {nameof(Bind)} method was not invoked after conrol initialization.");
            }

            Animator.PlayShowAnimation();
        }

        public virtual void OnClose()
        {
            Animator.PlayHideAnimation();
        }

        protected virtual void SetCloseButtonVisibility(Visibility visibility)
        {
        }
    }
}
