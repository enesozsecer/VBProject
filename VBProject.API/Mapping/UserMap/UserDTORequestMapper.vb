Imports AutoMapper
Imports VBProject.Entity

Public Class UserDTORequestMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of User, UserDTORequest)()
        CreateMap(Of UserDTORequest, User)()
    End Sub
End Class
