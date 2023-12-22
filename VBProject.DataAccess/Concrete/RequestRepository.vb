Imports Core
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class RequestRepository
    Inherits BaseRepository(Of Request)
    Implements IRequestRepository

    Public Sub New(dbContext As DbContext)
        MyBase.New(dbContext)
    End Sub
End Class
