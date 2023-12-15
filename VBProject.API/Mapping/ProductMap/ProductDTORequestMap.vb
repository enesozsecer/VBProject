Imports AutoMapper
Imports VBProject.Entity

Public Class ProductDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Product, ProductDTORequest)()
        CreateMap(Of ProductDTORequest, Product)()
    End Sub


End Class
