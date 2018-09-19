using System.Windows;
using ToastNotifications.Messages.Core;

namespace ToastNotifications.Messages.Information
{
    public class InformationMessage : MessageBase<InformationDisplayPart>
    {
        public InformationMessage(string messageText) : this(messageText, new MessageConfiguration())
        {
        }

        public InformationMessage(string messageText, MessageConfiguration configuration) : base(messageText, configuration)
        {
        }

        protected override InformationDisplayPart CreateDisplayPart()
        {
            return new InformationDisplayPart(this);
        }

        protected override void UpdateConfiguration(InformationDisplayPart displayPart, MessageConfiguration configuration)
        {
            if (configuration.FontSize != null)
                displayPart.Text.FontSize = configuration.FontSize.Value;

            displayPart.CloseButton.Visibility = configuration.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}