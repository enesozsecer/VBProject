Imports Core

Public Class InvoiceDetail
    Inherits BaseEntity
    Public Property Id As Long
    Public Property Price As Decimal
    Public Property Quantity As Decimal
    Public Property QuantityUnit As Short
    Public Property ProductName As String
    Public Property Invoice As Invoice
    Public Property InvoiceId As Long
    Public Property PriceStatus As String
    Public Property Rate As Decimal
End Class
