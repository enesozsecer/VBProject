Imports Core
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class CategoryRepository
    Inherits BaseRepository(Of Category)
    Implements ICategoryRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
