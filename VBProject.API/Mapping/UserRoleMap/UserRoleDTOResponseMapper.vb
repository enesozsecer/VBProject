Imports AutoMapper
Imports VBProject.Entity

Public Class UserRoleDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of UserRole, UserRoleDTOResponse)().
            ForMember(
                Function(dest) dest.RoleName,
                Sub(opt) opt.MapFrom(Function(src) src.Role.Name)
            ).
            ForMember(
                Function(dest) dest.UserName,
                Sub(opt) opt.MapFrom(Function(src) src.User.Name)
            ).ReverseMap()
    End Sub
End Class
