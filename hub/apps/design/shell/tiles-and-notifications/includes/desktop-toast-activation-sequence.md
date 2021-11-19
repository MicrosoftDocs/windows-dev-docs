When the user clicks any of your notifications (or a button on the notification), the following will happen...

**If your app is currently running**...

1. The **ToastNotificationManagerCompat.OnActivated** event will be invoked on a background thread.

**If your app is currently closed**...

1. Your app's EXE will be launched and `ToastNotificationManagerCompat.WasCurrentProcessToastActivated()` will return true to indicate the process was started due to a modern activation and that the event handler will soon be invoked.
1. Then, the 
 **ToastNotificationManagerCompat.OnActivated** event will be invoked on a background thread.