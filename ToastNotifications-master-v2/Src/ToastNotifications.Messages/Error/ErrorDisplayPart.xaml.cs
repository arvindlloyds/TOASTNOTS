using System.Windows;

namespace ToastNotifications.Messages.Error
{
    /// <summary>
    /// Interaction logic for ErrorDisplayPart.xaml
    /// </summary>
    public partial class ErrorDisplayPart
    {
        private readonly ErrorMessage _viewModel;

        public ErrorDisplayPart(ErrorMessage error)
        {
            InitializeComponent();

            _viewModel = error;
            Bind(_viewModel);
        }

        public override string GetMessage()
        {
            return _viewModel.MessageText;
        }

        protected override void SetCloseButtonVisibility(Visibility visibility)
        {
            CloseButton.Visibility = visibility;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();
        }
    }
}
