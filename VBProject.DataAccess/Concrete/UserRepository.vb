Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class UserRepository
    Inherits BaseRepository(Of User)
    Implements IUserRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
