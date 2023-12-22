Imports Core

Public Class Company
    Inherits BaseEntity
    Public Property Id As Integer
    Public Property Name As String = Nothing
    Public Property Image As String = Nothing
    Public Overridable Property Departments As ICollection(Of Department) = New List(Of Department)()
    Public Overridable Property Stocks As ICollection(Of Stock) = New List(Of Stock)()
End Class
