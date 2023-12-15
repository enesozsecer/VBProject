Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.ReportServer.ServiceModel.DataContracts
Imports DevExpress.Utils.Html.Internal
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports Microsoft.EntityFrameworkCore.Metadata.Conventions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net.Http
Imports System.Windows.Media.Animation
Imports UI.BaseFuncs
Imports VBProject.Entity
Public Class CategoryHome
    Public Sub New()
        InitializeComponent()
        GetAll()
    End Sub
    Private Async Sub GetAll()
        categorylist.Items.Clear()
        Dim apiUrl As String = "https://localhost:62437/GetCategories"
        Using client As New HttpClient()
            Dim response = client.GetAsync(apiUrl).Result

            If response.IsSuccessStatusCode Then
                Dim jsonString = response.Content.ReadAsStringAsync().Result
                'Dim jsondata = Await response.Content.ReadAsStringAsync()
                'Dim Kategoriler = JsonConvert.DeserializeObject(Of List(Of CategoryDTOResponse))(jsondata)
                Dim kategoriler As JArray = JArray.Parse(jsonString)

                ' Kategorileri ListView'e ekleyin


                'Dim listViewItem As New ListViewItem()



                'listViewItem.Content = New With {.Id = kategori.Id.ToString()}
                'listViewItem.Content = New With {.IsActive = kategori.IsActive.ToString()}
                'listViewItem.Content = New With {.KategoriAdi = kategori.Name}
                'KategorilerListView.ItemsSource = Kategoriler


                For Each item As JObject In kategoriler
                    Dim name As String = item("name").ToString()
                    Dim id As Integer = (item("id"))
                    Dim value As New CategoryDTOBase With {.Name = name, .Id = id}
                    categorylist.Items.Add(value)
                Next

                'For Each kategori In kategoriler
                '    Dim kategoriAdi As String = kategori("name")
                '    Dim Id As Integer = kategori("id")
                '    Dim IsActive As Boolean = kategori("isActive")
                '    Dim value As New CategoryDTOBase With {.Name = kategoriAdi, .Id = Id}
                '    If IsActive = True Then
                '        categorylist.Items.Add(kategori)
                '    End If
                'Next
            Else
                MessageBox.Show($"API isteği başarısız oldu. HTTP durumu: {response.StatusCode}")
            End If
        End Using
    End Sub
    Private Sub Company_StatusOK(sender As Object, e As EventArgs)
        GetAll()
    End Sub

    Private Sub updatebutton_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim id As Integer = button.Tag
        Dim modalWindow As New UpdateCategory(id)
        AddHandler modalWindow.StatusOK, AddressOf Company_StatusOK
        modalWindow.ShowDialog()
    End Sub

    Private Sub deletebutton_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim Id As Integer = button.Tag
        Dim response = Delete($"RemoveCategory/{Id}")
        GetAll()


    End Sub
    Private Sub ListView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        'Dim deneme As FrameworkElement = sender
        'deneme.
        Dim item As CategoryDTOBase = categorylist.SelectedItem
        MessageBox.Show(item.Id)

        'If categorylist.SelectedItem IsNot Nothing Then

        '    'Seçilen öğenin ID değerini almak için

        '    'Dim selectedKategori As CategoryDTOBase = DirectCast(KategorilerListView.SelectedItems, CategoryDTOBase)

        '    'Dim selectedKategori As ListViewItem = e.AddedItems.Item(index:=1)

        '    'Dim selectedID = selectedKategori

        '    'selectedID 'yi kullanabilirsiniz.
        '    'MessageBox.Show($"Seçilen Kategori ID: {selectedID}")
        'End If
    End Sub
End Class
