Imports AutoMapper
Imports VBProject.Entity

Public Class RequestDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of Request, RequestDTORequest)()
        CreateMap(Of RequestDTORequest, Request)()
    End Sub
End Class
