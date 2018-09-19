using CustomNotificationsExample.CustomMessage;
using CustomNotificationsExample.MahAppsNotification;
using System;
using System.Windows;
using CustomNotificationsExample.CustomCommand;
using CustomNotificationsExample.CustomInput;
using ToastNotifications;
using ToastNotifications.Events;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace CustomNotificationsExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Notifier _notifier;

        public MainWindow()
        {
            InitializeComponent();

            Unloaded += OnUnload;

            _notifier = new Notifier(cfg =>
            {
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(15), MaximumNotificationCount.FromCount(15));
                cfg.PositionProvider = new PrimaryScreenPositionProvider(Corner.BottomRight, 10, 10);
                cfg.KeyboardEventHandler = new AllowedSourcesInputEventHandler(new []{ typeof(CustomInputDisplayPart) });
            });
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _notifier.Dispose();
        }
        
        private void CustomMessage_Click(object sender, RoutedEventArgs e)
        {
            _notifier.ShowCustomMessage("Custom notificaton", "This is custom notification based on user control");
        }

        private void CustomCommand_Click(object sender, RoutedEventArgs e)
        {
            _notifier.ShowCustomCommand("Custom command example",
                confirmAction: n => n.Close(), // do something usefull here
                declineAction: n => n.Close());
        }

        private void CustomInput_Click(object sender, RoutedEventArgs e)
        {
            _notifier.ShowCustomInput("Custom input example");
        }

        private void MahApps_Click(object sender, RoutedEventArgs e)
        {
            _notifier.ShowMahAppsNotification("MahApps notification", "This is custom notification with MahApps styles");
        }

    }
}
