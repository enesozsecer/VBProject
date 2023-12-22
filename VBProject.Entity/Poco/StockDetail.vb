Imports Core

Public Class StockDetail
    Inherits BaseEntity
    Public Property Id As Long
    Public Property StockId As Long
    Public Property Quantity As Integer
    Public Property RecieverName As String
    Public Property DelivererName As String
    Public Property RecieverId As Long
    Public Property DelivererId As Long
    Public Property User As User
    Public Overridable Property Stock As Stock = Nothing

End Class
