using System.Windows;
using ToastNotifications.Core;

namespace ToastNotifications.Messages.Core
{
    public abstract class MessageBase<TDisplayPart> : NotificationBase where TDisplayPart : NotificationDisplayPart
    {
        public string MessageText { get; }

        private NotificationDisplayPart _displayPart;
        public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = Configure());

        protected MessageBase(string messageText, MessageConfiguration configuration)
        {
            MessageText = messageText;
            Configuration = configuration;
        }

        private TDisplayPart Configure()
        {
            TDisplayPart displayPart = CreateDisplayPart();

            displayPart.Unloaded += OnUnloaded;

            UpdateConfiguration(displayPart, Configuration as MessageConfiguration);

            return displayPart;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _displayPart.Unloaded -= OnUnloaded;
        }

        protected abstract void UpdateConfiguration(TDisplayPart displayPart, MessageConfiguration configuration);

        protected abstract TDisplayPart CreateDisplayPart();
    }
}