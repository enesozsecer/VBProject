Imports AutoMapper
Imports VBProject.Entity

Public Class DepartmentDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Department, DepartmentDTORequest)()
        CreateMap(Of DepartmentDTORequest, Department)()
    End Sub
End Class
