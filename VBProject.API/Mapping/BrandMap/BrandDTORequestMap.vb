Imports AutoMapper
Imports VBProject.Entity

Public Class BrandDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Brand, BrandDTOResponse)()
        CreateMap(Of BrandDTOResponse, Brand)()
    End Sub

End Class
