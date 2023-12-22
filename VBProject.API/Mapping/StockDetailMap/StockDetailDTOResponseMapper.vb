Imports AutoMapper
Imports VBProject.Entity

Public Class StockDetailDTOResponseMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of StockDetail, StockDetailDTOResponse)().
            ForMember(
                Function(dest) dest.ProductName,
                Sub(opt) opt.MapFrom(Function(src) src.Stock.Product.Name)
            ).ReverseMap()
    End Sub
End Class
