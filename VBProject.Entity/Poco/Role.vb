Imports Core

Public Class Role
    Inherits BaseEntity
    Public Property Id As Integer
    Public Property Name As String = Nothing
    Public Overridable Property UserRoles As ICollection(Of UserRole) = New List(Of UserRole)()
End Class
