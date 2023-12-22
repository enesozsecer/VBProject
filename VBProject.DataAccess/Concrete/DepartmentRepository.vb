Imports Core
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class DepartmentRepository
    Inherits BaseRepository(Of Department)
    Implements IDepartmentRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class