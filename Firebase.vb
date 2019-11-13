Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Namespace Firebase
    Public Class PushNotification
        Protected Class WebRequestFcmDataAndroid
            Public Property [to] As String
            Public Property [data] As New FcmData                   ' Note: data is sent with "normal" priority by default
            'Public Property [notification] As New FcmNotification  ' Note: notification is sent with "high" priority by default
            Public Property [priority] As String = "high"
        End Class

        Protected Class WebRequestFcmDataIOS
            'Public Property registration_ids As List(Of String)
            Public Property [to] As String
            Public Property [data] As New FcmData                   ' Note: data is sent with priority 5 by default
            'Public Property [notification] As New FcmNotification  ' Note: notification is sent with priority 10 by default
            Public Property [priority] As Integer = 10
            Public Property [content_available] As Boolean = True   ' Silent Push Notification    
        End Class

        Protected Class FcmNotification
            Public Property [title] As String
            Public Property [body] As String
            Public Property [sound] As String = "default"
        End Class

        Protected Class FcmData
            ' ===========================
            ' User defined custom Property
            ' ===========================
            Public Property [title] As String
            Public Property [body] As String
            Public Property [content] As String
        End Class

        Public Sub SendNotification(ByVal topic As String, ByVal title As String, ByVal body As String, ByVal received As Date)
            Try
                Dim fcmPath As String = "https://fcm.googleapis.com/fcm/send"
                Dim serverKey As String = "AAAAZ..."
                Dim senderID As String = "xxxxxxxxxxxx"

                Dim request As HttpWebRequest = CType(HttpWebRequest.Create(fcmPath), HttpWebRequest)
                With request
                    .Method = "post"
                    .ContentType = "application/json;charset=UTF-8"
                    .Headers.Add(String.Format("Authorization: key={0}", serverKey))
                    .Headers.Add(String.Format("Sender: id={0}", senderID))
                End With

                Using StreamWriter = New StreamWriter(request.GetRequestStream())
                    Dim webObject As Object
                    If topic.StartsWith("ios") Then ' Assume topic starts with ios
                        webObject = New WebRequestFcmDataIOS
                        With webObject
                            .to = "/topics/" & topic
                            '.priority = 10
                            '.content_available = True
                            '.notification.title = title
                            '.notification.body = body
                            '.notification.sound = "default"
                            .data.title = title
                            .data.body = body
                            .data.content = body
                        End With
                    Else
                        webObject = New WebRequestFcmDataAndroid
                        With webObject
                            .to = "/topics/" & topic
                            '.priority = "high"
                            .data.title = title
                            .data.body = body
                            .data.content = body
                        End With
                    End If

                    Dim json As String = JsonConvert.SerializeObject(webObject)
                    With StreamWriter
                        .Write(json)
                        .Flush()
                        .Close()
                    End With
                End Using
                Dim httpResponse As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using streamReader As New StreamReader(httpResponse.GetResponseStream)
                    Dim result As String = streamReader.ReadToEnd
                    MessageBox.Show(result, "Success")
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error")
            End Try
        End Sub
    End Class
End Namespace
