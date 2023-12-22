Imports AutoMapper
Imports VBProject.Entity

Public Class StockDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Stock, StockDTOResponse)().
            ForMember(
                Function(dest) dest.CompanyName,
                Sub(opt) opt.MapFrom(Function(src) src.Company.Name)
            ).
            ForMember(
                Function(dest) dest.ProductName,
                Sub(opt) opt.MapFrom(Function(src) src.Product.Name)
            ).ReverseMap()
    End Sub
End Class
