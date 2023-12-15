Imports Core

Public Class Brand
    Inherits BaseEntity

    Public Property Name As String
    Public Property Products As ICollection(Of Product) = New List(Of Product)()

End Class
