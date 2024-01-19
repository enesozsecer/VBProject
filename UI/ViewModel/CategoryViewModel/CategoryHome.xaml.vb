Imports System.Text
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq
Imports UI.BaseFuncs
Imports UI.MainWindow
Imports VBProject.Entity
Public Class CategoryHome
    Dim myUserControl As New UserControl()
    Public Sub New()
        InitializeComponent()
        GetAllAsync()
        InstanceCategory = Me
    End Sub
    Public Property selectedId As Integer
    Public Shared Property InstanceCategory As CategoryHome
    Public Async Function GetAllAsync() As Task(Of List(Of String))
        Dim values As JArray = GetAll(Of CategoryModel)("GetCategories")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of CategoryModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New CategoryModel With {.Name = u.Name, .Id = u.Id, .IsActive = u.IsActive}).ToList()
        categorylist.ItemsSource = filteredValues
    End Function

    Public Sub GetById(Id As Integer)
        Dim category As CategoryDTORequest = GetOne(Of CategoryDTORequest)("GetCategory/" + Id.ToString())
        CatId.Text = category.Id
    End Sub
    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If CatId.Text = "" Then
            Dim p As CategoryDTOBase = New CategoryDTOBase()
            p.Name = CategoryInput.Text
            Dim response = Add(Of CategoryDTOBase)("AddCategory", p)
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        Else
            Dim p As CategoryDTOBase = New CategoryDTOBase()
            p.Name = CategoryInput.Text
            p.Id = Convert.ToUInt32(CatId.Text)
            Dim response = Update(Of CategoryDTOBase)(p, "UpdateCategory")
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        End If
    End Sub
    Public Sub categorylist_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim item = categorylist.SelectedItem
        If item Is Nothing Then
            MessageBox.Show("Seçim yapmadınız veya veri yok!!!")
            Return
        End If
        selectedItemId = item.Id
        CatId.Text = item.Id
        CategoryInput.Text = item.Name
        openWindow.Visibility = Visibility.Visible

    End Sub
    Public Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow.Visibility = Visibility.Collapsed
    End Sub

    Public Sub Button_Click_Open(sender As Object, e As RoutedEventArgs)
        CategoryInput.Text = ""
        CatId.Text = ""
        openWindow.Visibility = Visibility.Visible

    End Sub

End Class
