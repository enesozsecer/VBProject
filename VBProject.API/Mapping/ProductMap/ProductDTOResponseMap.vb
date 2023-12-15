Imports AutoMapper
Imports VBProject.Entity

Public Class ProductDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Product, ProductDTOResponse)().
            ForMember(Function(dest) dest.CategoryName,
                      Sub(opt)
                          opt.MapFrom(Function(src) src.Category.Name)
                      End Sub).ReverseMap()
    End Sub

End Class
