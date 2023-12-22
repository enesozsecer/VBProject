Imports DevExpress.Xpf.Grid
Imports Newtonsoft.Json.Linq
Imports UI.MainWindow
Imports VBProject.Entity
Imports DevExpress.wpf.grid.core
Public Class CategoryHome
    Public Event StatusOK As EventHandler
    Public Sub New()
        InitializeComponent()
        GetAll()
    End Sub
    Public Async Sub GetAll()
        Dim values As JArray = UI.BaseFuncs.GetAll(Of CategoryModel)("GetCategories")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of CategoryModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New CategoryModel With {.Name = u.Name, .Id = u.Id, .IsActive = u.IsActive}).ToList()
        categorylist.ItemsSource = filteredValues

    End Sub

    Public Sub Category_StatusOK(sender As Object, e As EventArgs)
        GetAll()
    End Sub

    Private Sub categorylist_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim item As CategoryDTOBase = categorylist.SelectedItem
        selectedItemId = item.Id
        'If e.ChangedButton = MouseButton.Left AndAlso e.ClickCount = 1 Then
        '    Dim gridControl As GridControl = DirectCast(sender, GridControl)
        '    Dim tableView As TableView = TryCast(gridControl.View, TableView)

        '    If tableView IsNot Nothing Then
        '        Dim hitInfo As TableViewHitInfo = tableView.CalcHitInfo(DirectCast(e.OriginalSource, DependencyObject))

        '        If hitInfo.RowHandle <> GridControl.InvalidRowHandle AndAlso hitInfo.Column IsNot Nothing Then
        '            ' Tıklanan hücredeki verilere erişebilirsiniz
        '            Dim columnId As String = hitInfo.Column.FieldName
        '            Dim cellValue As Object = gridControl.GetCellValue(hitInfo.RowHandle, columnId)
        '            ' cellValue'i kullanabilirsiniz
        '        End If
        '    End If
        'End If
    End Sub
End Class
