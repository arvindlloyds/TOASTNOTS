using System;
using System.Windows;
using ToastNotifications.Core;
using ToastNotifications.Messages.Core;

namespace BasicUsageExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _vm = new ToastViewModel();

            Unloaded += OnUnload;
        }

        private int _count;
        private readonly ToastViewModel _vm;

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _vm.OnUnloaded();
        }

        private void Button_ShowInformationClick(object sender, RoutedEventArgs e)
        {
            ShowMessage(_vm.ShowInformation, "Information");
        }

        private void Button_ShowSuccessClick(object sender, RoutedEventArgs e)
        {
            ShowMessage(_vm.ShowSuccess, "Success");
        }

        private void Button_ShowWarningClick(object sender, RoutedEventArgs e)
        {
            ShowMessage(_vm.ShowWarning, "Warning");
        }

        private void Button_ShowErrorClick(object sender, RoutedEventArgs e)
        {
            ShowMessage(_vm.ShowError, "Error");
        }

        string _lastMessage;
        void ShowMessage(Action<string, MessageConfiguration> action, string name)
        {
            MessageConfiguration opts = new MessageConfiguration
            {
                CloseClickAction = CloseAction,
                Tag = $"[This is Tag Value ({++_count})]",
                FreezeOnMouseEnter = CbFreezeOnMouseEnter.IsChecked.GetValueOrDefault(),
                UnfreezeOnMouseLeave = CbUnfreezeOnMouseLeave.IsChecked.GetValueOrDefault(),
                ShowCloseButton = CbShowCloseButton.IsChecked.GetValueOrDefault()
            };
            _lastMessage = $"{_count} {name}";
            action(_lastMessage, opts);
            BClearLast.IsEnabled = true;
        }

        private void CloseAction(INotification obj)
        {
            var configuration = obj.Configuration as MessageConfiguration;
            _vm.ShowInformation($"Notification close clicked, Tag: {configuration?.Tag}");
        }


        private void Button_ClearClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearMessages("");
        }

        private void Button_ClearLastClick(object sender, RoutedEventArgs e)
        {
            _vm.ClearMessages(_lastMessage);
            BClearLast.IsEnabled = false;
        }

        private void Button_SameContentClick(object sender, RoutedEventArgs e)
        {
            const string sameContent = "Same Content - not duplicated";
            _vm.ClearMessages(sameContent);
            MessageConfiguration opts = new MessageConfiguration
            {
                CloseClickAction = CloseAction,
                Tag = "[This is Tag Value]",
                FreezeOnMouseEnter = CbFreezeOnMouseEnter.IsChecked.GetValueOrDefault(),
                ShowCloseButton = CbShowCloseButton.IsChecked.GetValueOrDefault()
            };
            _vm.ShowSuccess(sameContent, opts);
            _lastMessage = sameContent;
            BClearLast.IsEnabled = true;
        }
    }
}
