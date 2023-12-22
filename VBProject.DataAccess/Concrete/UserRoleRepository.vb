Imports Core
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class UserRoleRepository
    Inherits BaseRepository(Of UserRole)
    Implements IUserRoleRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
