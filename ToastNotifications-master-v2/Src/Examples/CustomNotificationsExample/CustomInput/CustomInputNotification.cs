using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToastNotifications.Core;

namespace CustomNotificationsExample.CustomInput
{
    public class CustomInputNotification : NotificationBase, INotifyPropertyChanged
    {
        private CustomInputDisplayPart _displayPart;

        public CustomInputNotification(string message, string initialText)
        {
            MessageText = message;
            InputText = initialText;
        }

        public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = new CustomInputDisplayPart(this));

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

        private string _inputText;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
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
