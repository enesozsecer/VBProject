Imports Core

Public Class UserRole
    Inherits BaseEntity
    Public Property UserRoleId As Long
    Public Property UserId As Long
    Public Property RoleId As Integer
    Public Property User As User
    Public Property Role As Role

End Class
