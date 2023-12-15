Imports Core
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class ProductRepository
    Inherits BaseRepository(Of Product)
    Implements IProductRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
