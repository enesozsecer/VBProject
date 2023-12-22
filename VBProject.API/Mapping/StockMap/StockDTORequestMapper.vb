Imports AutoMapper
Imports VBProject.Entity

Public Class StockDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Stock, StockDTORequest)()
        CreateMap(Of StockDTORequest, Stock)()
    End Sub
End Class
