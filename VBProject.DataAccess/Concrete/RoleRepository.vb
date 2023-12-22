Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class RoleRepository
    Inherits BaseRepository(Of Role)
    Implements IRoleRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
