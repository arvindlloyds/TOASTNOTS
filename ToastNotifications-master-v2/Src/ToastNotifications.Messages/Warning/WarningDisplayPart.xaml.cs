using System.Windows;

namespace ToastNotifications.Messages.Warning
{
    /// <summary>
    /// Interaction logic for WarningDisplayPart.xaml
    /// </summary>
    public partial class WarningDisplayPart
    {
        private readonly WarningMessage _viewModel;

        public WarningDisplayPart(WarningMessage warning)
        {
            InitializeComponent();

            _viewModel = warning;
            Bind(_viewModel);
        }

        public override string GetMessage()
        {
            return _viewModel.MessageText;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();
        }

        protected override void SetCloseButtonVisibility(Visibility visibility)
        {
            CloseButton.Visibility = visibility;
        }
    }
}
