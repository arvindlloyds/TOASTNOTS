using CustomNotificationsExample.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToastNotifications.Core;

namespace CustomNotificationsExample.CustomCommand
{
    public class CustomCommandNotification : NotificationBase, INotifyPropertyChanged
    {
        private CustomCommandDisplayPart _displayPart;

        public ICommand ConfirmCommand { get; set; }
        public ICommand DeclineCommand { get; set; }

        public CustomCommandNotification(string message,
            Action<CustomCommandNotification> confirmAction,
            Action<CustomCommandNotification> declineAction)
        {
            MessageText = message;

            ConfirmCommand = new RelayCommand(x => confirmAction(this));
            DeclineCommand = new RelayCommand(x => declineAction(this));
        }

        public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = new CustomCommandDisplayPart(this));

        #region binding properties

        private string _message;

        public string MessageText
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
