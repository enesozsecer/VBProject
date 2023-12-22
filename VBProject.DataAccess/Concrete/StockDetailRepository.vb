Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class StockDetailRepository
    Inherits BaseRepository(Of StockDetail)
    Implements IStockDetailRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
