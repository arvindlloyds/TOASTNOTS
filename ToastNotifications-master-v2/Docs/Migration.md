```
 _______              _   _   _       _   _  __ _           _   _                         ___  
|__   __|            | | | \ | |     | | (_)/ _(_)         | | (_)                       |__ \
   | | ___   __ _ ___| |_|  \| | ___ | |_ _| |_ _  ___ __ _| |_ _  ___  _ __  ___   __   __ ) |
   | |/ _ \ / _` / __| __| . ` |/ _ \| __| |  _| |/ __/ _` | __| |/ _ \| '_ \/ __|  \ \ / // /
   | | (_) | (_| \__ \ |_| |\  | (_) | |_| | | | | (_| (_| | |_| | (_) | | | \__ \   \ V // /_
   |_|\___/ \__,_|___/\__|_| \_|\___/ \__|_|_| |_|\___\__,_|\__|_|\___/|_| |_|___/    \_/|____|

```

# ToastNotifications v2

#### Migrating from v1

ToastNotifications v2 is completely new implementation and it's not compatibile with version 1.
Instead of creating NotificationsSource and NotificationTray now you have single service called Notifier.
Notifier is core mechanism used to configure and display different type of messages.
These messages are now plugins and they are sitting now in separate nugget "ToastNotification.Messages"
You have to install it, if you want to use predefined messages like Error, Warning, Success, Information.
You can also create now your own messages, or create different theme for built-in messages.

##### Migration instructions
1. Remove  old version references and usings
2. Remove NotificationTray from xaml
3. Remove NotificationSource from viewModel
4. Now follow [install instructions for v2](https://github.com/raflop/ToastNotifications)
