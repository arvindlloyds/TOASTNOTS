using System.Windows;

namespace ToastNotifications.Messages.Success
{
    /// <summary>
    /// Interaction logic for SuccessDisplayPart.xaml
    /// </summary>
    public partial class SuccessDisplayPart
    {
        private readonly SuccessMessage _viewModel;

        public SuccessDisplayPart(SuccessMessage success)
        {
            InitializeComponent();

            _viewModel = success;
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
