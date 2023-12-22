Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class InvoiceRepository
    Inherits BaseRepository(Of Invoice)
    Implements IInvoiceRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
