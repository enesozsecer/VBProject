Imports AutoMapper
Imports VBProject.Entity

Public Class InvoiceDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Invoice, InvoiceDTOResponse)().ReverseMap()
    End Sub
End Class
