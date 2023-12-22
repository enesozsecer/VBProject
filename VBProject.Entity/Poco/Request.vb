Imports Core

Public Class Request
    Inherits BaseEntity
    Public Property Id As Long
    Public Property UserId As Long
    Public Property AcceptedId As Int64
    Public Property Title As String = Nothing
    Public Property Description As String
    Public Property ProductId As Integer
    Public Property Quantity As Decimal
    Public Property QuantityUnit As Short
    Public Property RequestStatus As Short
    Public Overridable Property Product As Product = Nothing
    Public Overridable Property User As User = Nothing
    Public Overridable Property AcceptedUser As User = Nothing
    Public Overridable Property Offers As ICollection(Of Offer) = New List(Of Offer)()

End Class
