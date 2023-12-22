Public Class ApiResponse(Of T)
    Public Property StatusCode As Integer?
    Public Property Mesaj As String
    Public Property HataBilgisi As HataBilgisi
    Public Property Data As T

    Public Sub New(data As T, statustCode As Integer, hataBilgisi As HataBilgisi, mesaj As String)
        Me.StatusCode = statustCode
        Me.HataBilgisi = hataBilgisi
        Me.Mesaj = mesaj
        Me.Data = data
    End Sub
End Class
