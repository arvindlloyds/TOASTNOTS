using System.Windows;

namespace ToastNotifications.Messages.Information
{
    /// <summary>
    /// Interaction logic for InformationDisplayPart.xaml
    /// </summary>
    public partial class InformationDisplayPart
    {
        private readonly InformationMessage _viewModel;

        public InformationDisplayPart(InformationMessage information)
        {
            InitializeComponent();

            _viewModel = information;
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
