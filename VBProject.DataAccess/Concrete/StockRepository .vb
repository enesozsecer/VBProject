Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class StockRepository
    Inherits BaseRepository(Of Stock)
    Implements IStockRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
