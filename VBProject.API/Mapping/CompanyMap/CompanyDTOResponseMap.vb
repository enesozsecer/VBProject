Imports AutoMapper
Imports VBProject.Entity

Public Class CompanyDTOResponseMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Company, CompanyDTOResponse)()
        CreateMap(Of CompanyDTOResponse, Company)()
    End Sub
End Class
