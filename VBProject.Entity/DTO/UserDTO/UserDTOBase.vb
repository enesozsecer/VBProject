Public Class UserDTOBase
    Public Property Id As Long
    Public Property DepartmentId As Integer
    Public Property Name As String = Nothing
    Public Property LastName As String = Nothing
    Public Property Email As String = Nothing
    Public Property Phone As String = Nothing
    Public Property Password As String = Nothing
    Public Property Image As String = Nothing
    Public Property AddedUser As Long?
    Public Property UpdatedUser As Long?
End Class
