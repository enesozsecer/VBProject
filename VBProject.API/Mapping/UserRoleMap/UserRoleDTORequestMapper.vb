Imports AutoMapper
Imports VBProject.Entity

Public Class UserRoleDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of UserRole, UserRoleDTORequest)()
        CreateMap(Of UserRoleDTORequest, UserRole)()
    End Sub
End Class
