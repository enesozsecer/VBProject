Imports Newtonsoft.Json.Linq
Imports UI.BaseFuncs
Imports UI.MainWindow
Imports VBProject.Entity

Public Class BrandHome
    Public Sub New()
        InitializeComponent()
        GetAllAsync()
        InstanceBrand = Me
    End Sub
    Public Property selectedId As Integer
    Public Shared Property InstanceBrand As BrandHome
    Public Async Function GetAllAsync() As Task(Of List(Of String))
        Dim values As JArray = GetAll(Of BrandModel)("GetBrands")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of BrandModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New BrandModel With {.Name = u.Name, .Id = u.Id}).ToList()
        brandlist.ItemsSource = filteredValues
    End Function
    Public Sub GetById(Id As Integer)
        Dim brand As BrandDTORequest = GetOne(Of BrandDTORequest)("GetBrand/" + Id.ToString())
        BrandId.Text = brand.Id
    End Sub
    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If BrandId.Text = "" Then
            Dim p As BrandDTOBase = New BrandDTOBase()
            p.Name = BrandInput.Text
            Dim response = Add("AddBrand", p)
            If response Then
                Await GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        Else
            Dim p As BrandDTOBase = New BrandDTOBase()
            p.Name = BrandInput.Text
            p.Id = Convert.ToUInt32(BrandId.Text)
            Dim response = Update(p, "UpdateBrand")
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        End If
    End Sub
    Public Sub brandlist_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim item = brandlist.SelectedItem
        If item Is Nothing Then
            MessageBox.Show("Seçim yapmadınız veya veri yok!!!")
            Return
        End If
        selectedItemId = item.Id
        BrandId.Text = item.Id
        BrandInput.Text = item.Name
        openWindow.Visibility = Visibility.Visible
    End Sub
    Public Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow.Visibility = Visibility.Collapsed
    End Sub

    Public Sub Button_Click_Open(sender As Object, e As RoutedEventArgs)
        BrandInput.Text = ""
        BrandId.Text = ""
        openWindow.Visibility = Visibility.Visible
    End Sub
End Class
