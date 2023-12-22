Public Class HataBilgisi
    Public Property Hata As List(Of String)
    Public Property HataAciklama As String

    Public Shared Function NotFound(Optional hataAciklama As String = "Sonuç Bulunamadı", Optional hata As List(Of String) = Nothing) As HataBilgisi
        Return New HataBilgisi() With {.HataAciklama = hataAciklama, .Hata = hata}
    End Function

    Public Shared Function [Error](Optional hata As List(Of String) = Nothing, Optional hataAciklama As String = "Hata Oluştu") As HataBilgisi
        Return New HataBilgisi() With {.HataAciklama = hataAciklama, .Hata = hata}
    End Function

    Public Shared Function FieldValidationError(Optional hata As List(Of String) = Nothing, Optional hataAciklama As String = "Zorunlu Alanlar Eksik") As HataBilgisi
        Return New HataBilgisi With {.Hata = hata, .HataAciklama = hataAciklama}
    End Function

    Public Shared Function TokenNotFoundError(Optional hata As List(Of String) = Nothing, Optional hataAciklama As String = "Token Bilgisi Gelmedi") As HataBilgisi
        Return New HataBilgisi With {.Hata = hata, .HataAciklama = hataAciklama}
    End Function

    Public Shared Function ForbiddenError(Optional hata As List(Of String) = Nothing, Optional hataAciklama As String = "Yetkisiz Giriş!") As HataBilgisi
        Return New HataBilgisi With {.Hata = hata, .HataAciklama = hataAciklama}
    End Function
End Class
