Imports AutoMapper
Imports VBProject.Entity

Public Class InvoiceDetailDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of InvoiceDetail, InvoiceDetailDTORequest)()
        CreateMap(Of InvoiceDetailDTORequest, InvoiceDetail)()
    End Sub
End Class
