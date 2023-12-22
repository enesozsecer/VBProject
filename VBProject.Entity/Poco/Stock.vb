Imports Core

Public Class Stock
    Inherits BaseEntity
    Public Property Id As Long
    Public Property ProductId As Integer
    Public Property Quantity As Integer
    Public Property QuantityUnit As Short
    Public Property Description As String
    Public Property CompanyId As Integer
    Public Overridable Property Company As Company = Nothing
    Public Overridable Property Product As Product = Nothing
    Public Overridable Property StockDetails As ICollection(Of StockDetail) = New List(Of StockDetail)()

End Class
