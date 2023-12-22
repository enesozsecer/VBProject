Imports AutoMapper
Imports VBProject.Entity

Public Class InvoiceDetailDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of InvoiceDetail, InvoiceDetailDTOResponse)()
        CreateMap(Of InvoiceDetailDTOResponse, InvoiceDetail)()
    End Sub
End Class
