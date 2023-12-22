Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class BrandRepository
    Inherits BaseRepository(Of Brand)
    Implements IBrandRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
