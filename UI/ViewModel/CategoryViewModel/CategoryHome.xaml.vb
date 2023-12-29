Imports Newtonsoft.Json.Linq
Imports UI.MainWindow
Imports VBProject.Entity
Imports UI.BaseFuncs
Public Class CategoryHome
    Public Event StatusOK As EventHandler
    Public Property selectedId As Integer
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

    Public Sub categorylist_MouseDown(sender As Object, e As RoutedEventArgs)


        Dim item = categorylist.SelectedItem
        selectedItemId = item.Id
        CatId.Text = item.Id
        CategoryInput.Text = item.Name
        openWindow.Visibility = Visibility.Visible
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

    Private Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow.Visibility = Visibility.Collapsed
    End Sub

    Public Sub GetById(Id As Integer)
        Dim category As CategoryDTORequest = GetOne(Of CategoryDTORequest)("GetCategory/" + Id.ToString())
        CatId.Text = category.Id
    End Sub
    Public Property CategoryNameText As String
    Public Property CategoryIdText As String
    Private Sub TextBox_TextChanged(sender As Object, e As TextChangedEventArgs)
        ' TextBox'ın içindeki yeni metni al
        Dim newText As String = (DirectCast(sender, TextBox)).Text

        ' Yapmak istediğiniz işlemleri burada gerçekleştirin
        ' Örneğin, newText'i bir etikete (Label) yazdırabilirsiniz
        CategoryInput.Text = newText
        Dim p As CategoryDTOBase = New CategoryDTOBase()
        Name = newText
    End Sub

    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        AddHandler CategoryInput.TextChanged, AddressOf TextBox_TextChanged
        Dim p As CategoryDTOBase = New CategoryDTOBase()
        p.Name = CategoryInput.Text
        p.Id = Convert.ToUInt32(CatId.Text)
        Dim response = Update(Of CategoryDTOBase)(p, "UpdateCategory")
        If response Then
            RaiseEvent StatusOK(Me, EventArgs.Empty)
            Dim hedefSayfaMain As New MainWindow()

        Else
            MessageBox.Show("Bir sorun oluştu...")
        End If

    End Sub
End Class
