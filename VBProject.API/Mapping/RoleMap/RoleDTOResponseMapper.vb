Imports AutoMapper
Imports VBProject.Entity

Public Class RoleDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Role, RoleDTOResponse)()
        CreateMap(Of RoleDTOResponse, Role)().
            ForMember(
                Function(dest) dest.UserRoles,
                Sub(opt) opt.MapFrom(Function(src) src.Name)
            )
    End Sub
End Class
