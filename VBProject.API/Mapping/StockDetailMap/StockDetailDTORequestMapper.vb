Imports AutoMapper
Imports VBProject.Entity

Public Class StockDetailDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of StockDetail, StockDetailDTORequest)()
        CreateMap(Of StockDetailDTORequest, StockDetail)()
    End Sub
End Class
