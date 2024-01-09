Imports Newtonsoft.Json.Linq
Imports UI.MainWindow
Imports VBProject.Entity
Imports UI.BaseFuncs
Imports System.Windows.Forms
Imports System.Net.Http
Imports System.Text
Imports DevExpress.Xpf.Bars
Imports System.Windows.Threading

Public Class ProductHome

    Public Sub New()
        InitializeComponent()
        GetAll()
        InstanceProduct = Me
    End Sub
    Public Shared Property InstanceProduct As ProductHome
    'Private Sub productlist_MouseDown(sender As Object, e As MouseButtonEventArgs)
    '    Dim item = productlist.SelectedItem
    '    selectedItemId = item.Id
    'End Sub
    Public Async Sub GetAll()
        Dim values As JArray = UI.BaseFuncs.GetAll(Of ProductModel)("GetProducts")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of ProductModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New ProductModel With {.Name = u.Name, .Id = u.Id, .Description = u.Description, .BrandName = u.BrandName, .CategoryName = u.CategoryName}).ToList()
        productlist.ItemsSource = filteredValues

    End Sub
    Public Sub productlist_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim item = productlist.SelectedItem
        selectedItemId = item.Id
        IdInput.Text = item.Id
        NameInput.Text = item.Name
        DescriptionInput.Text = item.Description
        openWindow.Visibility = Visibility.Visible

    End Sub
    Public Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow.Visibility = Visibility.Collapsed
    End Sub
    'Public Sub GetById(Id As Integer)
    '    Dim category As CategoryDTORequest = GetOne(Of CategoryDTORequest)("GetCategory/" + Id.ToString())
    '    CatId.Text = category.Id
    'End Sub
    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If IdInput.Text = "" Then
            Dim p As ProductDTOBase = New ProductDTOBase()
            p.Name = NameInput.Text
            p.Description = DescriptionInput.Text
            Dim response = Add(Of ProductDTOBase)("AddProduct", p)
            If response Then
                GetAll()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        Else
            Dim p As ProductDTOBase = New ProductDTOBase()
            p.Name = NameInput.Text
            p.Id = Convert.ToUInt32(IdInput.Text)
            p.Description = DescriptionInput.Text
            Dim response = Update(Of ProductDTOBase)(p, "UpdateProduct")
            If response Then
                GetAll()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        End If


    End Sub
    Public Sub Button_Click_Open(sender As Object, e As RoutedEventArgs)
        NameInput.Text = ""
        IdInput.Text = ""
        DescriptionInput.Text = ""
        CategoryInput.Text = ""
        BrandInput.Text = ""
        openWindow.Visibility = Visibility.Visible

    End Sub
    Public Sub CategoryInput_PreviewKeyUp(sender As Object, e As KeyEventArgs)
        Dim filterText As String = CategoryInput.Text.ToLower()

        ' Örnek olarak, kategori listesini bir koleksiyon olarak düşünelim.
        Dim categoryWindow As New CategoryModel()
        Dim categories As List(Of String)
        Dim filteredCategories = categories.Where(Function(cat) cat.ToLower().Contains(filterText)).ToList()

        CategoryListBox.ItemsSource = filteredCategories

        ' Görünürlüğü ayarla
        If filteredCategories.Any() Then
            CategoryListBox.Visibility = Visibility.Visible
        Else
            CategoryListBox.Visibility = Visibility.Collapsed
        End If
    End Sub


End Class
