Public Class RequestDTOBase
    Public Property Id As Long
    Public Property UserId As Long
    Public Property AcceptedId As Long?
    Public Property ProductId As Integer
    Public Property Title As String = Nothing
    Public Property Description As String
    Public Property Quantity As Decimal
    Public Property QuantityUnit As Decimal
    Public Property RequestStatus As Integer
    Public Property AddedUser As Long?
    Public Property UpdatedUser As Long?
End Class
