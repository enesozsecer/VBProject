Imports AutoMapper
Imports VBProject.Entity

Public Class RoleDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Role, RoleDTORequest)()
        CreateMap(Of RoleDTORequest, Role)()
    End Sub
End Class
