using System.Windows;

namespace CustomNotificationsExample.CustomCommand
{
    /// <summary>
    /// Interaction logic for CustomCommandDisplayPart.xaml
    /// </summary>
    public partial class CustomCommandDisplayPart
    {
        public CustomCommandDisplayPart(CustomCommandNotification notification)
        {
            InitializeComponent();
            Bind(notification);
        }

        protected override void SetCloseButtonVisibility(Visibility visibility)
        {
            
        }
    }
}
