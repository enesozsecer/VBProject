Imports VBProject.Entity
Imports UI.BaseFuncs
Class UpdateCategory
    Public Property selectedId As Integer
    Public Event StatusOK As EventHandler
    Public Sub New(Id As Integer)
        selectedId = Id
        InitializeComponent()
        GetById()
    End Sub

    Public Sub GetById()
        Dim category As CategoryDTORequest = GetOne(Of CategoryDTORequest)("GetCategory/" + selectedId.ToString())
        Id.Text = category.Id
        CategoryInput.Text = category.Name
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim p As CategoryDTORequest = New CategoryDTORequest()
        p.Id = Convert.ToUInt32(Id.Text)
        p.Name = CategoryInput.Text
        Dim response = Update(Of CategoryDTORequest)(p, "UpdateCategory")
        If response Then
            MessageBox.Show("Güncellendi.")
            Me.Close()
            RaiseEvent StatusOK(Me, EventArgs.Empty)
        Else
            MessageBox.Show("Bir sorun oluştu...")

        End If
    End Sub
End Class
