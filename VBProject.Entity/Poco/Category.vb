Imports Core

Public Class Category
    Inherits BaseEntity

    Sub New()
        Products = New List(Of Product)
    End Sub
    Public Property Id As Integer
    Public Property Name As String
    Public Property Products As ICollection(Of Product) = New List(Of Product)()


End Class
