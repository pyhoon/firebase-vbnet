# firebase-vbnet
Firebase push notification send to topic (VB.NET)

This code is modified from the code taken from https://stackoverflow.com/questions/55589936/using-vb-net-to-sent-notification-to-android-emulator-get-error-401 instead of sending push notification to device ids, it sends message to subscribed topic to Android and iOS as silent push notification.

# Usage:
```vb.net
    Private Sub BtnNotify_Click(sender As Object, e As EventArgs) Handles BtnNotify.Click
        Dim n As New Firebase.PushNotification
        n.SendNotification("/topics/and_topic-foobar", "Foo", "Bar")
        n.SendNotification("/topics/ios_topic-foobar", "Foo", "Bar")
    End Sub
```

Note: Remember to edit the serverKey and senderID inside SendNotification sub.
