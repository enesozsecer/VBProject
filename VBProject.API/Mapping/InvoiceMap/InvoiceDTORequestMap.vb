Imports AutoMapper
Imports VBProject.Entity

Public Class InvoiceDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Invoice, InvoiceDTORequest)()
        CreateMap(Of InvoiceDTORequest, Invoice)()
    End Sub
End Class
