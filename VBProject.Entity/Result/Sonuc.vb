Imports System.Net

Public Class Sonuc(Of T)
    Public Sub New(_data As T, _mesaj As String, _statusCode As Integer, _hataBilgisi As HataBilgisi)
        Data = _data
        Mesaj = _mesaj
        StatusCode = _statusCode
        HataBilgisi = _hataBilgisi
    End Sub

    Public Sub New(_mesaj As String, _statusCode As Integer, _hataBilgisi As HataBilgisi)
        Mesaj = _mesaj
        StatusCode = _statusCode
        HataBilgisi = _hataBilgisi
    End Sub

    Public Sub New(_statusCode As Integer, _hataBilgisi As HataBilgisi)
        StatusCode = _statusCode
        HataBilgisi = _hataBilgisi
    End Sub

    Public Property Data As T

    Public Property Mesaj As String

    Public Property StatusCode As Integer

    Public Property HataBilgisi As HataBilgisi

    Public Shared Function [Error](Optional message As String = "Hata Oluştu", Optional statusCode As Integer = CInt(HttpStatusCode.InternalServerError)) As Sonuc(Of T)
        Return New Sonuc(Of T)(message, statusCode, HataBilgisi.Error())
    End Function

    Public Shared Function SuccessWithData(data As T, Optional message As String = "İşlem Başarılı", Optional statusCode As Integer = CInt(HttpStatusCode.OK)) As Sonuc(Of T)
        Return New Sonuc(Of T)(data, message, statusCode, Nothing)
    End Function

    Public Shared Function SuccessWithoutData(Optional message As String = "İşlem Başarılı", Optional statusCode As Integer = CInt(HttpStatusCode.OK)) As Sonuc(Of T)
        Return New Sonuc(Of T)(message, statusCode, Nothing)
    End Function

    Public Shared Function SuccessNoDataFound(Optional message As String = "Sonuç Bulunamadı", Optional statusCode As Integer = CInt(HttpStatusCode.NotFound)) As Sonuc(Of T)
        Return New Sonuc(Of T)(message, statusCode, HataBilgisi.NotFound())
    End Function

    Public Shared Function FieldValidationError(hataBilgisi As HataBilgisi, Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Sonuc(Of T)
        Return New Sonuc(Of T)("Hata Oluştu", statusCode, hataBilgisi)
    End Function

    Public Shared Function TokenNotFound() As Sonuc(Of T)
        Return New Sonuc(Of T)("Hata Oluştu", CInt(HttpStatusCode.Unauthorized), HataBilgisi.TokenNotFoundError())
    End Function

    Public Shared Function ExistingError(Optional message As String = "Hata Oluştu", Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Sonuc(Of T)
        Return New Sonuc(Of T)(message, statusCode, HataBilgisi.Error())
    End Function

    Public Shared Function AlreadyExistError(Optional message As String = "Aynı yetkiden birden fazla eklenemez.", Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Sonuc(Of T)
        Return New Sonuc(Of T)(message, statusCode, HataBilgisi.Error())
    End Function
End Class
