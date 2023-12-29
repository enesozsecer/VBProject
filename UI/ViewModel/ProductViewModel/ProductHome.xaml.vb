Imports Newtonsoft.Json.Linq
Imports UI.MainWindow

Public Class ProductHome
    Public Sub New()
        InitializeComponent()
        GetAll()
    End Sub
    Private Sub productlist_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim item = productlist.SelectedItem
        selectedItemId = item.Id
    End Sub
    Public Async Sub GetAll()
        Dim values As JArray = UI.BaseFuncs.GetAll(Of ProductModel)("GetProducts")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of ProductModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New ProductModel With {.Name = u.Name, .Id = u.Id, .Description = u.Description, .BrandName = u.BrandName, .CategoryName = u.CategoryName}).ToList()
        productlist.ItemsSource = filteredValues

    End Sub
End Class
