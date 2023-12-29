Imports AutoMapper
Imports VBProject.Entity

Public Class RequestDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Request, RequestDTOResponse)().
            ForMember(Function(dest) dest.UserName, Sub(opt)
                                                        opt.MapFrom(Function(src) src.User.Name & " " & src.User.LastName)
                                                    End Sub).
            ForMember(Function(dest) dest.ProductName, Sub(opt)
                                                           opt.MapFrom(Function(src) src.Product.Name)
                                                       End Sub)
        'ForMember(Function(dest) dest.AcceptedName, Sub(opt)
        '                                                opt.MapFrom(Function(src) src.AcceptedUser.Name & " " & src.AcceptedUser.LastName)
        '                                            End Sub).ReverseMap()

    End Sub
End Class
