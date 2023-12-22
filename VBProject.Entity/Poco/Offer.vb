Imports Core

Public Class Offer
    Inherits BaseEntity
    Public Property Id As Long
    Public Property SupplierName As String = Nothing
    Public Property Description As String = Nothing
    Public Property Price As Decimal
    Public Property PriceStatus As String = Nothing
    Public Property Status As Integer
    Public Property Rate As Decimal
    Public Property UserId As Long ' Gelecek
    Public Overridable Property User As User = Nothing
    Public Property RequestId As Long ' Gelecek
    'Public Overridable Property Request As Request = Nothing

End Class
