Imports AutoMapper
Imports Core
Imports VBProject.Entity

Public Class CategoryDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Category, CategoryDTORequest)()
        CreateMap(Of CategoryDTORequest, Category)()
        CreateMap(Of CategoryDTORequest, BaseEntity)()
        CreateMap(Of BaseEntity, CategoryDTORequest)()
    End Sub


End Class
