using System.Windows;
using ToastNotifications.Messages.Core;

namespace ToastNotifications.Messages.Error
{
    public class ErrorMessage : MessageBase<ErrorDisplayPart>
    {
        public ErrorMessage(string messageText) : this(messageText, new MessageConfiguration())
        {
        }

        public ErrorMessage(string messageText, MessageConfiguration configuration) : base(messageText, configuration)
        {
        }

        protected override ErrorDisplayPart CreateDisplayPart()
        {
            return new ErrorDisplayPart(this);
        }

        protected override void UpdateConfiguration(ErrorDisplayPart displayPart, MessageConfiguration configuration)
        {
            if (configuration.FontSize != null)
                displayPart.Text.FontSize = configuration.FontSize.Value;

            displayPart.CloseButton.Visibility = configuration.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}