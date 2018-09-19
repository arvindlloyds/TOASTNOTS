using System.Windows;

namespace ConfigurationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainViewModel();
        }

        private int _count;
        private readonly MainViewModel _vm;

        private string CreateMessage()
        {
            return $"{_count++} {SampleTextInput.Text}";
        }

        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowInformation(CreateMessage());
        }

        private void Button_ShowSuccessClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowSuccess(CreateMessage());
        }

        private void Button_ShowWarningClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowWarning(CreateMessage());
        }

        private void Button_ShowErrorClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowError(CreateMessage());
        }

        private void Button_ShowCustomizedMessageClick(object sender, RoutedEventArgs e)
        {
            _vm.ShowCustomizedMessage(CreateMessage());
        }
    }
}
