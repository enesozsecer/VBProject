Imports System.Text
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq
Imports UI.BaseFuncs
Imports UI.MainWindow
Imports VBProject.Entity

Public Class ProductHome

    Public Sub New()
        InitializeComponent()
        GetAllAsync()
        InstanceProduct = Me
    End Sub
    Public Shared Property InstanceProduct As ProductHome
    'Private Sub productlist_MouseDown(sender As Object, e As MouseButtonEventArgs)
    '    Dim item = productlist.SelectedItem
    '    selectedItemId = item.Id
    'End Sub
    Public Async Function GetAllAsync() As Task(Of List(Of String))
        Dim values As JArray = UI.BaseFuncs.GetAll(Of ProductModel)("GetProducts")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of ProductModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New ProductModel With {.Name = u.Name, .Id = u.Id, .Description = u.Description, .BrandName = u.BrandName, .CategoryName = u.CategoryName}).ToList()
        productlist.ItemsSource = filteredValues

    End Function
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
            p.CategoryId = Convert.ToInt32(CategoryTextId.Text)
            p.CategoryId = Convert.ToInt32(CategoryTextId.Text)
            p.BrandId = Convert.ToInt32(BrandTextId.Text)
            Dim response = Add(Of ProductDTOBase)("AddProduct", p)
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        Else
            Dim p As ProductDTOBase = New ProductDTOBase()
            p.Name = NameInput.Text
            p.Id = Convert.ToUInt32(IdInput.Text)
            p.Description = DescriptionInput.Text
            p.CategoryId = Convert.ToInt32(CategoryTextId.Text)
            p.BrandId = Convert.ToInt32(BrandTextId.Text)
            Dim response = Update(Of ProductDTOBase)(p, "UpdateProduct")
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        End If


    End Sub
    Public Sub Button_Click_Open(sender As Object, e As RoutedEventArgs)
        NameInput.Text = ""
        IdInput.Text = ""
        DescriptionInput.Text = ""
        CategoryTextInput.Text = ""
        BrandTextInput.Text = ""
        openWindow.Visibility = Visibility.Visible

    End Sub

    Private Sub PreviewKeyUpCategory(sender As Object, e As Input.KeyEventArgs)
        Dim filterTextCategory As String = CategoryTextInput.Text.ToLower()

        Dim categories As JArray = GetAll(Of CategoryModel)("GetCategories")
        Dim filteredCategories = categories.Where(Function(cat) cat("name").ToString().ToLower().Contains(filterTextCategory)).ToList()
        CategoryListBox.ItemsSource = filteredCategories
        If categories.Any() Then
            CategoryListBox.Visibility = Visibility.Visible
        Else
            CategoryListBox.Visibility = Visibility.Collapsed
        End If
    End Sub
    Private Sub CategoryListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedCategory As JObject = TryCast(CategoryListBox.SelectedItem, JObject)

        If selectedCategory IsNot Nothing Then
            Dim categoryId As String = selectedCategory("id")
            Dim categoryName As String = selectedCategory("name")

            If categoryName IsNot Nothing Then
                CategoryTextId.Text = categoryId
                CategoryTextInput.Text = categoryName
                CategoryListBox.Visibility = Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub PreviewKeyUpBrand(sender As Object, e As Input.KeyEventArgs)
        Dim filterTextBrand As String = BrandTextInput.Text.ToLower()

        Dim brands As JArray = GetAll(Of BrandModel)("GetBrands")
        Dim filteredBrands = brands.Where(Function(br) br("name").ToString().ToLower().Contains(filterTextBrand)).ToList()
        BrandListBox.ItemsSource = filteredBrands
        If brands.Any() Then
            BrandListBox.Visibility = Visibility.Visible
        Else
            BrandListBox.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub BrandListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedBrand As JObject = TryCast(BrandListBox.SelectedItem, JObject)

        If selectedBrand IsNot Nothing Then
            Dim brandId As String = selectedBrand("id")
            Dim brandName As String = selectedBrand("name")

            If brandName IsNot Nothing Then
                BrandTextId.Text = brandId
                BrandTextInput.Text = brandName
                BrandListBox.Visibility = Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub BrandTextInput_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        CategoryListBox.Visibility = Visibility.Hidden
    End Sub
End Class
