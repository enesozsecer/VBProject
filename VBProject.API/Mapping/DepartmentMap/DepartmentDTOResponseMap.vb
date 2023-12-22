Imports AutoMapper
Imports VBProject.Entity

Public Class DepartmentDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Department, DepartmentDTOResponse)().
            ForMember(
                Function(dest) dest.CompanyName,
                Sub(opt) opt.MapFrom(Function(src) src.Company.Name)
            ).ReverseMap()
    End Sub
End Class
