using System.Windows;
using ToastNotifications.Messages.Core;

namespace ToastNotifications.Messages.Success
{
    public class SuccessMessage : MessageBase<SuccessDisplayPart>
    {
        public SuccessMessage(string messageText) : this(messageText, new MessageConfiguration())
        {
        }

        public SuccessMessage(string messageText, MessageConfiguration configuration) : base(messageText, configuration)
        {
        }

        protected override SuccessDisplayPart CreateDisplayPart()
        {
            return new SuccessDisplayPart(this);
        }

        protected override void UpdateConfiguration(SuccessDisplayPart displayPart, MessageConfiguration configuration)
        {
            if (configuration.FontSize != null)
                displayPart.Text.FontSize = configuration.FontSize.Value;

            displayPart.CloseButton.Visibility = configuration.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}