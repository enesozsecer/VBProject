Imports Core

Public Class Invoice
    Inherits BaseEntity
    Public Property InvoiceDate As DateTime
    Public Property Id As Long
    Public Property TotalPrice As Decimal
    Public Property SupplierName As String
    Public Property CompanyName As String
    Public Property PriceStatus As String
    Public Property Rate As Decimal
    Public Overridable Property InvoiceDetails As ICollection(Of InvoiceDetail) = New List(Of InvoiceDetail)()
End Class
