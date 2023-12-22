Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class InvoiceDetailRepository
    Inherits BaseRepository(Of InvoiceDetail)
    Implements IInvoiceDetailRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
