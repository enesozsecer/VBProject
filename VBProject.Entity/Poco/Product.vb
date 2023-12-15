Imports Core

Public Class Product
    Inherits BaseEntity

    Public Property Name As String
    Public Property Description As String = Nothing
    Public Property Category As Category
    Public Property Brand As Brand
    Public Property BrandId As Integer
    Public Property CategoryId As Integer

End Class
