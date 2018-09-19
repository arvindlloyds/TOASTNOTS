```
 _______              _   _   _       _   _  __ _           _   _                         ___  
|__   __|            | | | \ | |     | | (_)/ _(_)         | | (_)                       |__ \
   | | ___   __ _ ___| |_|  \| | ___ | |_ _| |_ _  ___ __ _| |_ _  ___  _ __  ___   __   __ ) |
   | |/ _ \ / _` / __| __| . ` |/ _ \| __| |  _| |/ __/ _` | __| |/ _ \| '_ \/ __|  \ \ / // /
   | | (_) | (_| \__ \ |_| |\  | (_) | |_| | | | | (_| (_| | |_| | (_) | | | \__ \   \ V // /_
   |_|\___/ \__,_|___/\__|_| \_|\___/ \__|_|_| |_|\___\__,_|\__|_|\___/|_| |_|___/    \_/|____|

```

# ToastNotifications v2
#### Toast notifications for WPF

## Creating custom notifications
[Example code](https://github.com/raflop/ToastNotifications/tree/master-v2/Src/Examples/CustomNotificationsExample)

### 1 Install nugget:
[Install-Package ToastNotifications](https://www.nuget.org/packages/ToastNotifications/)

### 2 Create notifier instance
```csharp
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
/* * */
Notifier notifier = new Notifier(cfg =>
{
    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(5), MaximumNotificationCount.FromCount(15));
    cfg.PositionProvider = new PrimaryScreenPositionProvider(Corner.BottomRight, 10, 10);
});
```

### 3 Create CustomNotification class

This class will contains all data needed to display in notification in this example its just a "Title" property but it can be anything even commands for buttons.
CustomNotification derrive from "NotificationBase" which is base class for all notifications in ToastNotifications v2.
Every notification is composed of two things:
* Its data - properties used for data binding
* DisplayPart - which describes how notification looks and display its data

1. Create class called CustomNotification
2. Derrive it from NotificationBase and INotifyPropertyChanged
3. Add notification data using properties
4. Create DisplayPart property as shown below

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToastNotifications.Core;

public class CustomNotification : NotificationBase, INotifyPropertyChanged
{
    private CustomDisplayPart _displayPart;

    public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = new CustomDisplayPart(this));

    public CustomNotification(string message)
    {
        Message = message;
    }

    private string _message;
    public string Message
    {
        get
        {
            return _message;
        }
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
        if (handler != null)
            handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

### 4 Create DisplayPart for notification

DisplayPart describes notification appearance, its nothing more than just costom UI control derrivied from NotificationDisplayPart.
The simpliest way to create new DisplayPart is by creating WPF "UserControl" in Visual Studio.

1. Create UserControl called CustomDisplayPart
2. Change the base class from UserControl to NotificationDisplayPart in CustomDisplayPart.xaml and CustomDisplayPart.xaml.cs
3. Create constructor parameter of type CustomNotification to connect DisplayPart with notification data
4. Bind UI controls to notification data

CustomDisplayPart.xaml
```xml
<core:NotificationDisplayPart x:Class="CustomNotificationsExample.CustomMessage.CustomDisplayPart"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:local="clr-namespace:CustomNotificationsExample.CustomMessage"
             xmlns:core="clr-namespace:ToastNotifications.Core;assembly=ToastNotifications"
             mc:Ignorable="d"  Background="Gray"
             d:DesignHeight="60" d:DesignWidth="250"
             d:DataContext="{d:DesignInstance local:CustomNotification, IsDesignTimeCreatable=False}" >
    <Grid  Margin="5">
        <TextBlock Text="{Binding Message}" FontWeight="Light" Foreground="White" Grid.Row="1" TextWrapping="Wrap" />
    </Grid>
</core:NotificationDisplayPart>
```
CustomDisplayPart.xaml.cs
```csharp
    public partial class CustomDisplayPart
    {
        public CustomDisplayPart(CustomNotification customNotification)
        {
            InitializeComponent();
            Bind(customNotification);
        }
    }
```
### 5 Create extension method helper
Extension method will be used to simplify the usage of CustomNotification in applicaiton code

```csharp
    public static class CustomMessageExtensions
    {
        public static void ShowCustomMessage(this Notifier notifier, string message)
        {
            notifier.Notify<CustomNotification>(() => new CustomNotification(message));
        }
    }
```

### 6 Use notifier with CustomNotification
```csharp
    notifier.ShowCustomMessage("This is custom notification based on user control");
```
