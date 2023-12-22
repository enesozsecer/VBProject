Imports AutoMapper
Imports VBProject.Entity

Public Class UserDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of User, UserDTOResponse)().
            ForMember(
                Function(dest) dest.DepartmentName,
                Sub(opt) opt.MapFrom(Function(src) src.Department.Name)
            ).
            ForMember(
                Function(dest) dest.RoleName,
                Sub(opt) opt.MapFrom(Function(src) src.UserRoles.Select(Function(x) x.Role.Name).ToList())
            ).
            ForMember(
                Function(dest) dest.CompanyId,
                Sub(opt) opt.MapFrom(Function(src) src.Department.CompanyId)
            ).
            ForMember(
                Function(dest) dest.CompanyName,
                Sub(opt) opt.MapFrom(Function(src) src.Department.Company.Name)
            ).ReverseMap()
    End Sub
End Class
