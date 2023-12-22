Imports AutoMapper
Imports VBProject.Entity

Public Class OfferDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Offer, OfferDTOResponse)().
            ForMember(
                Function(dest) dest.UserName,
                Sub(opt) opt.MapFrom(Function(src) src.User.Name)
            ).ReverseMap()
        'ForMember(
        '    Function(dest) dest.RequestName,
        '    Sub(opt) opt.MapFrom(Function(src) src.Request.Title)
        ')
    End Sub
End Class
