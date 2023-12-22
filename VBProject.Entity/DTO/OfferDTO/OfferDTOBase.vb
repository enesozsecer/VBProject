Public Class OfferDTOBase
    Public Property Id As Long
    Public Property RequestId As Long
    Public Property UserId As Long
    Public Property SupplierName As String = Nothing
    Public Property Description As String = Nothing
    Public Property Price As Decimal
    Public Property PriceStatus As String = Nothing
    Public Property Rate As Decimal
    Public Property Status As Integer
    Public Property AddedUser As Long?
    Public Property UpdatedUser As Long?
End Class
