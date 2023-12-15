Imports AutoMapper
Imports VBProject.Entity

Public Class CategoryDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Category, CategoryDTOResponse)()
    End Sub

End Class
