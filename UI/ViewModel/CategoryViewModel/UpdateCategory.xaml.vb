'Imports UI.BaseFuncs
'Imports VBProject.Entity
'Imports UI.MainWindow
'Public Class UpdateCategory
'    Public Property selectedId As Integer
'    Public Event StatusOK As EventHandler

'    Public Sub New(Id As Integer)
'        selectedId = Id
'        InitializeComponent()
'        GetById()
'    End Sub

'    Public Sub GetById()
'        Dim category As CategoryDTORequest = GetOne(Of CategoryDTORequest)("GetCategory/" + selectedId.ToString())
'        Id.Text = category.Id
'        CategoryInput.Text = category.Name
'    End Sub

'    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
'        Dim p As CategoryDTOBase = New CategoryDTOBase()
'        p.Id = Convert.ToUInt32(Id.Text)
'        p.Name = CategoryInput.Text
'        Dim response = Update(Of CategoryDTOBase)(p, "UpdateCategory")
'        If response Then
'            Close()
'            RaiseEvent StatusOK(Me, EventArgs.Empty)
'            Dim hedefSayfaMain As New MainWindow()

'        Else
'            MessageBox.Show("Bir sorun oluştu...")
'        End If
'    End Sub

'End Class
