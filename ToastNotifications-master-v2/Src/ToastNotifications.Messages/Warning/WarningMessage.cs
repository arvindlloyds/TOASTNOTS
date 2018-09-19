using System.Windows;
using ToastNotifications.Messages.Core;

namespace ToastNotifications.Messages.Warning
{
    public class WarningMessage : MessageBase<WarningDisplayPart>
    {
        public WarningMessage(string messageText) : this(messageText, new MessageConfiguration())
        {
        }

        public WarningMessage(string messageText, MessageConfiguration configuration) : base(messageText, configuration)
        {
        }

        protected override WarningDisplayPart CreateDisplayPart()
        {
            return new WarningDisplayPart(this);
        }

        protected override void UpdateConfiguration(WarningDisplayPart displayPart, MessageConfiguration configuration)
        {
            if (configuration.FontSize != null)
                displayPart.Text.FontSize = configuration.FontSize.Value;

            displayPart.CloseButton.Visibility = configuration.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}