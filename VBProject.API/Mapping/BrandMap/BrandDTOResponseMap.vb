Imports AutoMapper
Imports VBProject.Entity

Public Class BrandDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Brand, BrandDTORequest)()
        CreateMap(Of BrandDTORequest, Brand)()
    End Sub

End Class
