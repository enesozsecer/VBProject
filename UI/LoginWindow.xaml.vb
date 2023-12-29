Imports System.Net.Http
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc
Imports Newtonsoft.Json
Imports RestSharp
Imports VBProject.Entity

Public Class LoginWindow
    'Private ReadOnly _httpClient As HttpClient
    'Public Sub New(httpClient As HttpClient)

    '    InitializeComponent()
    '    _httpClient = httpClient

    'End Sub


    Public Async Sub LoginButton_Click(sender As Object, e As RoutedEventArgs)
        logInBtn.IsEnabled = False

        If txtUsername.Text Is "" Then
            MessageBox.Show("Kullanıcı Adı Alanını Doldurmak Zorunludur", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            logInBtn.IsEnabled = True

        End If
        If txtPassword.Password Is "" Then
            MessageBox.Show("Sifre Alanını Doldurmak Zorunludur", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            logInBtn.IsEnabled = True

        End If
        Dim p As LoginDTO = New LoginDTO
        p.KullaniciAdi = txtUsername.Text
        p.Sifre = txtPassword.Password
        Dim url As String = "https://localhost:62437/Login"
        Dim client As New RestClient(url)
        Dim request As New RestRequest(url, RestSharp.Method.Post)
        request.AddHeader("Content-Type", "application/json")
        Dim body As String = JsonConvert.SerializeObject(p)
        request.AddBody(body, "application/json")
        Dim response As RestResponse = Await client.ExecuteAsync(request)
        Dim responseObject As ApiResponse(Of LoginDTO) = JsonConvert.DeserializeObject(Of ApiResponse(Of LoginDTO))(response.Content)

        If responseObject.StatusCode = 200 Then
            logInBtn.IsEnabled = False
            Application.Current.Properties("Token") = responseObject.Data.Token
            Application.Current.Properties("Company") = responseObject.Data.CompanyId
            Application.Current.Properties("Department") = responseObject.Data.DepartmentId
            Application.Current.Properties("CompanyName") = responseObject.Data.CompanyName
            Application.Current.Properties("DepartmentName") = responseObject.Data.DepartmentName
            Application.Current.Properties("UserName") = responseObject.Data.AdSoyad
            Application.Current.Properties("User") = responseObject.Data.UserId
            Dim Token As String = CStr(Application.Current.Properties("Token"))
            Dim Company As String = CStr(Application.Current.Properties("Company"))
            Dim Department As String = CStr(Application.Current.Properties("Department"))
            Dim CompanyName As String = CStr(Application.Current.Properties("CompanyName"))
            Dim DepartmentName As String = CStr(Application.Current.Properties("DepartmentName"))
            Dim UserName As String = CStr(Application.Current.Properties("UserName"))
            Dim UserId As String = CStr(Application.Current.Properties("User"))
            'NavigationService.Navigate(New Uri("MainWindow.xaml", UriKind.Relative))
            Dim mainWindow As New MainWindow()
            mainWindow.Show()
            Close()
        Else
            If txtUsername.Text IsNot "" And txtPassword.Password IsNot "" Then
                MessageBox.Show("Hatalı Giriş Yaptınız", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                logInBtn.IsEnabled = True

            End If
        End If
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub MinimizeButton_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub Border_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub
End Class
