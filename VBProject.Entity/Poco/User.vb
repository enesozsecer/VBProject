Imports Core

Public Class User
    Inherits BaseEntity
    Public Property Id As Int64
    Public Property DepartmentId As Int32 ' Gelecek
    Public Overridable Property Department As Department = Nothing
    Public Property Name As String = Nothing
    Public Property LastName As String = Nothing
    Public Property Email As String = Nothing
    Public Property Phone As String = Nothing
    Public Property Password As String = Nothing
    Public Property Image As String = Nothing
    Public Overridable Property Requests As ICollection(Of Request) = New List(Of Request)()
    Public Overridable Property AcceptedRequests As ICollection(Of Request) = New List(Of Request)()
    'Public Overridable Property StockDetailDeliverers As ICollection(Of StockDetail) = New List(Of StockDetail)()
    Public Property Offers As ICollection(Of Offer) = New List(Of Offer)()
    'Public Overridable Property StockDetailRecievers As ICollection(Of StockDetail) = New List(Of StockDetail)()
    Public Property UserRoles As ICollection(Of UserRole) = New List(Of UserRole)()
End Class
