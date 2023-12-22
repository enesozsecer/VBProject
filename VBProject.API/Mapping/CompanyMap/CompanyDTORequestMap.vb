Imports AutoMapper
Imports VBProject.Entity

Public Class CompanyDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Company, CompanyDTORequest)()
        CreateMap(Of CompanyDTORequest, Company)()
    End Sub
End Class
