Imports Core

Public Class Department
    Inherits BaseEntity
    Public Property Id As Int32
    Public Property Name As String = Nothing
    Public Property CompanyId As Integer
    Public Overridable Property Users As ICollection(Of User) = New List(Of User)()
    Public Overridable Property Company As Company = Nothing
End Class
