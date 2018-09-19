using ConfigurationExample.Utilities;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Messages.Core;
using ToastNotifications.Position;

namespace ConfigurationExample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region notifier configuration
        private Notifier _notifier;

        public MainViewModel()
        {
            _notifier = CreateNotifier(Corner.TopRight, PositionProviderType.Window, NotificationLifetimeType.Basic);
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Closing += MainWindowOnClosing;

            FreezeOnMouseEnter = true;
            UnFreezeOnMouseEnter = true;
            ShowCloseButton = false;
        }

        public Notifier CreateNotifier(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier?.Dispose();
            _notifier = null;

            return new Notifier(cfg =>
            {
                cfg.PositionProvider = CreatePositionProvider(corner, relation);
                cfg.LifetimeSupervisor = CreateLifetimeSupervisor(lifetime);
                cfg.Dispatcher = Dispatcher.CurrentDispatcher;
            });
        }

        public void ChangePosition(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier = CreateNotifier(corner, relation, lifetime);
        }

        private void MainWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _notifier.Dispose();
        }

        private static INotificationsLifetimeSupervisor CreateLifetimeSupervisor(NotificationLifetimeType lifetime)
        {
            if (lifetime == NotificationLifetimeType.Basic)
                return new CountBasedLifetimeSupervisor(MaximumNotificationCount.FromCount(5));

            return new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(3), MaximumNotificationCount.UnlimitedNotifications());
        }

        private static IPositionProvider CreatePositionProvider(Corner corner, PositionProviderType relation)
        {
            switch (relation)
            {
                case PositionProviderType.Window:
                    {
                        return new WindowPositionProvider(Application.Current.MainWindow, corner, 5, 5);
                    }
                case PositionProviderType.Screen:
                    {
                        return new PrimaryScreenPositionProvider(corner, 5, 5);
                    }
                case PositionProviderType.Control:
                    {
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        var trackingElement = mainWindow?.TrackingElement;
                        return new ControlPositionProvider(mainWindow, trackingElement, corner, 5, 5);
                    }
            }

            throw new InvalidEnumArgumentException();
        }
        #endregion

        #region notifier messages
        internal void ShowWarning(string message)
        {
            _notifier.ShowWarning(message, CreateOptions());
        }

        private MessageConfiguration CreateOptions()
        {
            return new MessageConfiguration()
            {
                FreezeOnMouseEnter = FreezeOnMouseEnter,
                UnfreezeOnMouseLeave = UnFreezeOnMouseEnter,
                ShowCloseButton = ShowCloseButton
            };
        }

        internal void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message, CreateOptions());
        }

        public void ShowInformation(string message)
        {
            _notifier.ShowInformation(message, CreateOptions());
        }

        public void ShowError(string message)
        {
            _notifier.ShowError(message, CreateOptions());
        }

        public void ShowCustomizedMessage(string message)
        {
            var options = new MessageConfiguration
            {
                FontSize = 25,
                ShowCloseButton = false,
                FreezeOnMouseEnter = false,
                NotificationClickAction = n =>
                {
                    n.Close();
                    _notifier.ShowSuccess("clicked!");
                }
            };

            
            _notifier.ShowError(message, options);
        }
        #endregion

        #region example settings
        private Corner _corner;
        public Corner Corner
        {
            get => _corner;
            set
            {
                _corner = value;
                OnPropertyChanged(nameof(Corner));
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private PositionProviderType _positionProviderType;
        public PositionProviderType PositionProviderType
        {
            get => _positionProviderType;
            set
            {
                _positionProviderType = value;
                OnPropertyChanged(nameof(PositionProviderType));
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private NotificationLifetimeType _lifetime;

        public NotificationLifetimeType Lifetime
        {
            get => _lifetime;
            set
            {
                _lifetime = value;
                OnPropertyChanged("Lifetime");
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private bool _freezeOnMouseEnter;
        public bool FreezeOnMouseEnter
        {
            get => _freezeOnMouseEnter;
            set
            {
                _freezeOnMouseEnter = value;
                OnPropertyChanged("FreezeOnMouseEnter");
            }
        }

        private bool _unFreezeOnMouseEnter;
        public bool UnFreezeOnMouseEnter
        {
            get => _unFreezeOnMouseEnter;
            set
            {
                _unFreezeOnMouseEnter = value;
                OnPropertyChanged("UnFreezeOnMouseEnter");
            }
        }

        private bool _showCloseButton;
        public bool ShowCloseButton
        {
            get => _showCloseButton;
            set
            {
                _showCloseButton = value;
                OnPropertyChanged("ShowCloseButton");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}