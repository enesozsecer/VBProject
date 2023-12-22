Imports Newtonsoft.Json.Linq
Imports VBProject.Entity

Class ProductHome
    Public Sub New()
        InitializeComponent()
        GetAll()
    End Sub
    Private Sub productlist_MouseDown(sender As Object, e As MouseButtonEventArgs)

    End Sub
    Public Async Sub GetAll()
        Dim values As JArray = UI.BaseFuncs.GetAll(Of ProductModel)("GetProducts")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of ProductModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New ProductModel With {.Name = u.Name, .Id = u.Id, .Description = u.Description, .BrandId = u.BrandId, .CategoryId = u.CategoryId}).ToList()
        productlist.ItemsSource = filteredValues

    End Sub
End Class
