Imports System.Net.Http
Imports System.Text
Imports DevExpress.Xpf.Core
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class AddCategory
    Public Event StatusOK As EventHandler
    Public Sub New()
        InitializeComponent()
        'Me.WindowState = WindowState.Maximized
        'Me.WindowStyle = WindowStyle.None

    End Sub
    Private Sub Kategorileri_Ekle(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim apiUrl As String = "https://localhost:62437/AddCategory"

        ' Yeni kategori için örnek veri
        Dim kategoriAdi As String = CategoryInput.Text

        ' API'ye gönderilecek veri oluşturuluyor
        Dim jsonData As String = $"{{""name"": ""{kategoriAdi}""}}"

        ' HttpClient oluşturuluyor
        Using client As New HttpClient()
            ' JSON verisi gönderiliyor
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")

            ' API'ye POST isteği yapılıyor
            Dim response = client.PostAsync(apiUrl, content).Result

            ' Yanıt kontrolü
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Kategori başarıyla eklendi.")
                RaiseEvent StatusOK(Me, EventArgs.Empty)
                Close()
            Else
                MessageBox.Show($"API isteği başarısız oldu. HTTP durumu: {response.StatusCode}")
            End If
        End Using
    End Sub
End Class
